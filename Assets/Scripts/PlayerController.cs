using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeep;
    public float moveInput;
    private Rigidbody2D rb;
    public float jumpValue = 0.0f;
    private BoxCollider2D colli;
    private bool canJump = true;
    public PhysicsMaterial2D bounceMat, normalMat;
    [SerializeField] private LayerMask jumpableGround;
    private float chargeJump = 550f;
    private float maxJump = 400;

    private bool estaEnelSuelo;

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
        rb = gameObject.GetComponent<Rigidbody2D>();
        colli = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        Debug.Log("SUELO???" + estaEnelSuelo);
        // Only can move if you are grounded
        if (estaEnelSuelo && jumpValue <= 0)
        {
            Debug.Log("se puede mover");
            rb.velocity = new Vector2(moveInput * walkSpeep, rb.velocity.y);
        }

        // Can't move if u are pressing the space
        if (Input.GetKeyDown("space") && estaEnelSuelo)
        {
            Debug.Log("Can't move if u are pressing the space");
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        // Bounce of the wall
        //wall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, jumpableGround);
        if ((isWallLeft() || isWallRight()) && !estaEnelSuelo)
        {
            Debug.Log("pared");
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }

        // Save the time charge of the jump force
        if (Input.GetKey("space") && estaEnelSuelo && canJump)
        {
            jumpValue += chargeJump * Time.deltaTime;
        }

        // if the jumpValue if more than the max, make jump the player
        if (jumpValue >= maxJump && estaEnelSuelo)
        {
            estaEnelSuelo = false;
            Debug.Log("if the jumpValue if more than the max, make jump the player");
            float tempx = moveInput * walkSpeep;
            float tempy = jumpValue;

            rb.velocity = new Vector2(tempx, tempy);

            Invoke("resetJump", 0.2f);
        }

        // If the player lift the space before the max jump and if lower than x
        //if (Input.GetKeyUp("space") && estaEnelSuelo && jumpValue <= 120f)

        if (Input.GetKeyUp("space") && estaEnelSuelo && jumpValue <= 120f)
        {
            if (estaEnelSuelo)
            {
                estaEnelSuelo = false;
                Debug.Log("120f");
                jumpValue = 120f;
                rb.velocity = new Vector2(moveInput * walkSpeep, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }
        else if (Input.GetKeyUp("space") && estaEnelSuelo)
        {
            if (estaEnelSuelo)
            {
                estaEnelSuelo = false;
                Debug.Log("If the player lift the space before the max jump");
                rb.velocity = new Vector2(moveInput * walkSpeep, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }

        // Bounce while one the wall, vaya guarrada pero soy un genio
        if (estaEnelSuelo && isWallRight())
        {
            Debug.Log("pared y muro RIGHT");
            if (Input.GetKeyUp("space") || jumpValue >= maxJump)
            {
                estaEnelSuelo = false;
                Debug.Log("soltaste el espacio o max jump RIGHT");
                rb.position = new Vector2(rb.position.x - 0.01f, rb.position.y);
            }
        }
        if (estaEnelSuelo && isWallLeft())
        {
            Debug.Log("pared y muro LEFT");
            if (Input.GetKeyUp("space") || jumpValue >= maxJump)
            {
                estaEnelSuelo = false;
                Debug.Log("soltaste el espacio o max jump LEFT");
                rb.position = new Vector2(rb.position.x + 0.01f, rb.position.y);
            }
        }

        // Si no toca ninguna tecla y puede saltar y esta en la tierra DA MUCHOS PROBLEMAS
        // if (!Input.GetKey("space") && !Input.GetKey("right") && !Input.GetKey("left") && canJump && isGrounded())
        // {
        //     rb.velocity = new Vector2(0.0f, rb.velocity.y);
        // }
    }

    private bool isGrounded()
    {
        // float extraheight = .06f;
        // Color rayColorCenter;
        // Color rayColorRight;
        // Color rayColorLeft;
        // Vector3 rayCastRight = new Vector3(rb.position.x + 13.90f, rb.position.y - 0.6f, 0f);
        // Vector3 rayCastLeft = new Vector3(rb.position.x - 9.85f, rb.position.y - 0.6f, 0f);

        // RaycastHit2D raycastHitCenter = Physics2D.Raycast(colli.bounds.center, Vector2.down, colli.bounds.extents.y + extraheight, jumpableGround);
        // RaycastHit2D raycastHitRight = Physics2D.Raycast(rayCastRight, Vector2.down * (colli.bounds.extents.y + extraheight), jumpableGround);
        // RaycastHit2D raycastHitLeft = Physics2D.Raycast(rayCastLeft, Vector2.down * (colli.bounds.extents.y + extraheight), jumpableGround);


        // if (raycastHitCenter.collider != null)
        // {
        //     rayColorCenter = Color.green;
        // }
        // else
        // {
        //     rayColorCenter = Color.red;
        // }

        // if (raycastHitRight.collider != null)
        // {
        //     rayColorRight = Color.green;
        // }
        // else
        // {
        //     rayColorRight = Color.red;
        // }

        // if (raycastHitLeft.collider != null)
        // {
        //     rayColorLeft = Color.green;
        // }
        // else
        // {
        //     rayColorLeft = Color.red;
        // }

        // Debug.DrawRay(colli.bounds.center, Vector2.down * (colli.bounds.extents.y + extraheight), rayColorCenter);
        // Debug.DrawRay(rayCastRight, Vector2.down * (colli.bounds.extents.y), rayColorRight);
        // Debug.DrawRay(rayCastLeft, Vector2.down * (colli.bounds.extents.y + extraheight), rayColorLeft);
        
        // Funciona raro con los rebotes
        Debug.Log(colli.bounds.size);
        RaycastHit2D box = Physics2D.BoxCast(colli.bounds.center, new Vector3(21.50f, 26.93f), 0f, Vector2.down, 0.5f, jumpableGround);

        return box.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Ground") {
            estaEnelSuelo = true;
        }
    }

    private bool isWallRight()
    {
        RaycastHit2D raycastWallRight = Physics2D.Raycast(colli.bounds.center, Vector2.right, colli.bounds.extents.x + 0.1f, jumpableGround);
        Color rayColor;
        if (raycastWallRight.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(colli.bounds.center, Vector2.right * (colli.bounds.extents.x + 0.1f), rayColor);

        return raycastWallRight.collider != null;
    }

    private bool isWallLeft()
    {
        RaycastHit2D raycastWallLeft = Physics2D.Raycast(colli.bounds.center, Vector2.left, colli.bounds.extents.x + 0.1f, jumpableGround);
        Color rayColor;
        if (raycastWallLeft.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(colli.bounds.center, Vector2.left * (colli.bounds.extents.x + 0.1f), rayColor);

        return raycastWallLeft.collider != null;
    }

    private void resetJump()
    {
        jumpValue = 0;
    }
}
