using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerControllerNoPhysics : MonoBehaviour
{
    [Header("PlayerStuff")]
    [SerializeField] public LayerMask jumpableGround;
    public PhysicsMaterial2D bounceMat, normalMat;
    private Rigidbody2D rb;
    private BoxCollider2D colli;
    SpriteRenderer sprite;
    public TextMeshProUGUI text;

    [Header("Movement")]
    // 280 For more horizontal jump
    // 220 For more vertical jump
    public float walkSpeep = 280f;
    private float moveInput;
    // 5 for slow fall
    public float fallMultiplier = 5;
    private float jumpValue = 0.0f;
    public float chargeJump = 1500f;
    public float maxJump = 800f;
    public Transform frontCheck;

    [Header("Movement_Bool")]
    public bool isInTheGround;
    private bool facingRight = true;
    public bool isTouchingFront;
    public bool isTouchingFrontFunction;

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
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isInTheCorner();
        isTouchingFrontFunction = frontCheckerFunction();
        text.text = "Velocidad = " + Mathf.Round(rb.velocity.x);

        moveInput = Input.GetAxis("Horizontal");

        //            ANIMATIONS
        // Animation running if velocity is positive 
        if (Time.timeScale != 69420)
        {
            // Is in the ground variable
            if (isInTheGround)
            {
                animator.SetBool("isInTheGround", true);
            }
            else
            {
                animator.SetBool("isInTheGround", false);
            }

            // Horizontal varibale
            if (isTouchingFrontFunction)
            {
                Debug.Log("aqui");
                animator.SetFloat("Horizontal", 0);
            }
            else
            {
                // Math Round because it goes to velocity like 10e-14
                animator.SetFloat("Horizontal", Mathf.Round(rb.velocity.x));
            }

            // Animation charge jump
            if (isInTheGround && (Mathf.Round(rb.velocity.x) == 0) && Input.GetKey("space"))
            {
                // Animation jumpChargin true
                animator.SetBool("isCharginJump", true);
            }
            else
            {
                // Animation jumpChargin false
                animator.SetBool("isCharginJump", false);
            }
        }
        //            END ANIMATION


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
        // NO SE COMO ARREGLAR QUE NO REBOTE EN LAS ESQUINAS
        // if (isTouchingFrontFunction && isInTheCorner())
        // {
        //     Debug.Log("no rebota 1");
        //     rb.sharedMaterial = normalMat;
        // }

        if (isTouchingFrontFunction && !isInTheGround)
        {
            Debug.Log("rebota");
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            Debug.Log("no rebota");
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
            Debug.Log("salto mayor que el maximo");
            float tempx = moveInput * walkSpeep;
            float tempy = jumpValue;

            rb.velocity = new Vector2(tempx, tempy);
            Invoke("resetJump", 0.2f);
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
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

    public bool frontCheckerFunction()
    {
        RaycastHit2D raycastWallRight = Physics2D.Raycast(colli.bounds.center, Vector2.right, colli.bounds.extents.x + 5f, jumpableGround);
        RaycastHit2D raycastWallLeft = Physics2D.Raycast(colli.bounds.center, Vector2.left, colli.bounds.extents.x + 5f, jumpableGround);

        Color rayColor;
        if (raycastWallRight.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Color rayColor2;
        if (raycastWallLeft.collider != null)
        {
            rayColor2 = Color.green;
        }
        else
        {
            rayColor2 = Color.red;
        }

        Debug.DrawRay(colli.bounds.center, Vector2.right * (colli.bounds.extents.x + 5f), rayColor);
        Debug.DrawRay(colli.bounds.center, Vector2.left * (colli.bounds.extents.x + 5f), rayColor2);

        return raycastWallRight.collider != null || raycastWallLeft.collider != null;
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
    public bool isInTheCorner()
    {
        float extraheight = .06f;
        Color rayColorCenter;
        Color rayColorRight;

        RaycastHit2D raycastHitCenter = Physics2D.Raycast(colli.bounds.center, Vector2.down, colli.bounds.extents.y + extraheight, jumpableGround);
        // RaycastHit2D raycastHitRight = Physics2D.Raycast(transform.position +  new Vector3(10f, 0f), Vector2.down * 13f, jumpableGround);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(colli.bounds.center * new Vector2(1.025f, 1f), Vector2.down, colli.bounds.extents.y + 9f, jumpableGround);

        if (raycastHitCenter.collider != null)
        {
            rayColorCenter = Color.green;
        }
        else
        {
            rayColorCenter = Color.red;
        }

        if (raycastHitRight.collider != null)
        {
            rayColorRight = Color.green;
        }
        else
        {
            rayColorRight = Color.red;
        }

        // Debug.DrawRay(colli.bounds.center, Vector2.down * (colli.bounds.extents.y + extraheight), rayColorCenter);
        Debug.DrawRay(colli.bounds.center * new Vector2(1.025f, 1f), Vector2.down * (colli.bounds.extents.y + 9f), rayColorRight);
        return raycastHitRight.collider != null;
    }
}
