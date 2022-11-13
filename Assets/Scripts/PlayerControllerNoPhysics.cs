using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerNoPhysics : MonoBehaviour
{
    public float walkSpeep;
    public float moveInput;
    private Rigidbody2D rb;
    public float jumpValue = 0.0f;
    private BoxCollider2D colli;
    private CapsuleCollider2D capsule;
    public PhysicsMaterial2D bounceMat, normalMat;
    [SerializeField] private LayerMask jumpableGround;
    private float chargeJump = 1500f;
    private float maxJump = 1000f;
    private bool isInTheGround;
    bool facingRight = true;
    bool isTouchingFront;
    public Transform frontCheck;

    [Header("Animation")]
    private Animator animator;


    /*
    *   Cosas para que no se me olviden
    *   1.- Para add una fuerza
    *       gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0.5f));
    *   2.- Return de antes de isGrounded
    *       return Physics2D.BoxCast(colli.bounds.center, colli.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    */

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        rb = gameObject.GetComponent<Rigidbody2D>();
        colli = GetComponent<BoxCollider2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        
        //ANIMATIONS
        // Animation running if velocity is positive
        if (isTouchingFront)
        {
            animator.SetFloat("Horizontal", 0);
        }
        else
        {
            animator.SetFloat("Horizontal", Mathf.Abs(rb.velocity.x));

        }

        // Animation charge jump
        if (isInTheGround && (Mathf.Abs(rb.velocity.x) == 0) && Input.GetKey("space"))
        {
            // Animation jumpChargin true
            animator.SetBool("isCharginJump", true);
        }
        else
        {
            // Animation jumpChargin false
            animator.SetBool("isCharginJump", false);
        }
        //END ANIMATION


        // Flip the Player facing right and left
        if (moveInput > 0 && facingRight == false)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight == true)
        {
            Flip();
        }

        // Bounce of the wall
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 0.3f, jumpableGround);
        if (isTouchingFront && !isInTheGround)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }

        if (isInTheGround && Input.GetKeyUp("space") && jumpValue < maxJump)
        {
            Debug.Log("salto menor que el maximo");
            float tempx = moveInput * walkSpeep;
            float tempy = jumpValue;

            rb.velocity = new Vector2(tempx, tempy);
            Invoke("resetJump", 0.2f);
        }

        Debug.Log("Esta en el suelo?: " + isInTheGround);

        // if the jumpValue if more than the max, make jump the player
        if (jumpValue >= maxJump && isInTheGround)
        {
            // estaEnelSuelo = false;
            Debug.Log("salto mayor que el maximo");
            float tempx = moveInput * walkSpeep;
            float tempy = jumpValue;

            rb.velocity = new Vector2(tempx, tempy);

            Invoke("resetJump", 0.2f);
        }

    }

    void FixedUpdate()
    {
        // Only can move if you are grounded
        if (isInTheGround && jumpValue <= 0)
        {
            rb.velocity = new Vector2(moveInput * walkSpeep, rb.velocity.y);
            if (rb.velocity.x != 0f && ((!Input.GetKey("right") && !Input.GetKey("left")) || Input.GetKey("space")))
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }

        // Save the time charge of the jump force
        if (Input.GetKey("space") && isInTheGround)
        {
            jumpValue += chargeJump * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isInTheGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isInTheGround = false;
        }
    }

    private void resetJump()
    {
        jumpValue = 0;
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
}
