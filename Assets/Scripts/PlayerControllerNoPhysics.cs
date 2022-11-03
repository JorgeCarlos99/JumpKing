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

    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        Debug.Log("SUELO trigger: " + isInTheGround);

        // Flip the Player facing right and left
        // if (moveInput > 0 && facingRight == false)
        // {
        //     Flip();
        // }
        // else if (moveInput < 0 && facingRight == true)
        // {
        //     Flip();
        // }

        // Bounce of the wall
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 0.5f, jumpableGround);
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
            Debug.Log("SALTO MENOR QUE EL MAXIMO");
            float tempx = moveInput * walkSpeep;
            float tempy = jumpValue;

            rb.velocity = new Vector2(tempx, tempy);
            Invoke("resetJump", 0.2f);
        }

        // if the jumpValue if more than the max, make jump the player
        if (jumpValue >= maxJump && isInTheGround)
        {
            // estaEnelSuelo = false;
            Debug.Log("if the jumpValue if more than the max, make jump the player");
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

    private bool estaEnelSueloFunciton()
    {
        float extraheight = .5f;
        Color rayColorCenter;
        Color rayColorRight;

        RaycastHit2D raycastHitCenter = Physics2D.Raycast(colli.bounds.center, Vector2.down, colli.bounds.extents.y + extraheight, jumpableGround);

        RaycastHit2D raycastHitRight = Physics2D.Raycast(transform.position + new Vector3(11.8f, 0f), Vector2.down, colli.bounds.extents.y + 1f, jumpableGround);

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


        Debug.DrawRay(colli.bounds.center, Vector2.down * (colli.bounds.extents.y + extraheight), rayColorCenter);

        Debug.DrawRay(transform.position + new Vector3(11.8f, 0f), Vector2.down * (colli.bounds.extents.y + 1f), rayColorRight);


        return raycastHitCenter.collider != null || raycastHitRight.collider != null;
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.transform.tag == "Ground")
    //     {
    //         estaEnelSuelo = true;
    //     }
    // }

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

    private bool isWallRight()
    {
        RaycastHit2D raycastWallRight = Physics2D.Raycast(capsule.bounds.center, Vector2.right, capsule.bounds.extents.x + 0.5f, jumpableGround);
        Color rayColor;
        if (raycastWallRight.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(capsule.bounds.center, Vector2.right * (capsule.bounds.extents.x + 0.5f), rayColor);

        return raycastWallRight.collider != null;
    }

    private bool isWallLeft()
    {
        RaycastHit2D raycastWallLeft = Physics2D.Raycast(colli.bounds.center, Vector2.left, colli.bounds.extents.x + 0.5f, jumpableGround);
        Color rayColor;
        if (raycastWallLeft.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(capsule.bounds.center, Vector2.left * (capsule.bounds.extents.x + 0.5f), rayColor);

        return raycastWallLeft.collider != null;
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
