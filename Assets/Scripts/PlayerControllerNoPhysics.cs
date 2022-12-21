using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;


public class PlayerControllerNoPhysics : MonoBehaviour
{
    [Header("PlayerStuff")]
    [SerializeField] public LayerMask jumpableGround;
    public PhysicsMaterial2D bounceMat, normalMat;
    private Rigidbody2D rb;
    private BoxCollider2D colli;
    SpriteRenderer sprite;
    public TextMeshProUGUI text;
    [SerializeField] private float maxYvelocity;
    public bool canMove = true;
    public Animation animKnockDown;
    public Animator m4a4animator;
    public Vector3 position;
    public Slider sliderJumpValue;
    public Gradient gradient;
    public Image fill;

    [Header("Movement")]
    // 280 For more horizontal jump
    // 220 For more vertical jump
    public float walkSpeep = 280f;
    private float moveInput;
    // 5 for slow fall
    public float fallMultiplier = 5;
     [SerializeField]
    private float jumpValue = 0.0f;
    public float chargeJump = 1500f;
    public float maxJump = 800f;
    public Transform frontCheck;

    [Header("Movement_Bool")]
    public bool isInTheGrass;
    public bool isInTheCloud;
    public bool isInTheRocket;
    private bool facingRight = true;
    public bool isTouchingFront;
    public bool isTouchingFrontFunction;

    [Header("Animation")]
    private Animator animator;

    [Header("Sounds")]
    // Grass
    [SerializeField] private AudioSource grassJumpSoundEffect;
    [SerializeField] private AudioSource grassJumpLandSoundEffect;
    [SerializeField] private AudioSource grassRunEffect;
    [SerializeField] private AudioSource grassHardFloorEffect;

    // Clouds
    [SerializeField] private AudioSource cloudJumpSoundEffect;
    [SerializeField] private AudioSource cloudJumpLandSoundEffect;
    [SerializeField] private AudioSource cloudRunEffect;
    [SerializeField] private AudioSource cloudHardFloorEffect;

    // Rocket
    [SerializeField] private AudioSource rocketJumpSoundEffect;
    [SerializeField] private AudioSource rocketJumpLandSoundEffect;
    [SerializeField] private AudioSource rocketRunEffect;
    [SerializeField] private AudioSource rocketHardFloorEffect;
    public static PlayerControllerNoPhysics instance;
    public AudioMixer audioMixer;
    private void Awake()
    {
        instance = this;
    }
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
        animKnockDown = GetComponent<Animation>();
        QualitySettings.vSyncCount = 1;
        rb = gameObject.GetComponent<Rigidbody2D>();
        colli = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        if (SaveManager.instance.hasLoaded)
        {
            // Position
            position = SaveManager.instance.activeSave.position;
            rb.transform.position = position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set charge jump slider value
        sliderJumpValue.SetValueWithoutNotify(jumpValue);
        fill.color = gradient.Evaluate(sliderJumpValue.normalizedValue);


        //SAVE
        if (isInTheGroundAll())
        {
            position = rb.transform.position;
        }

        SaveManager.instance.activeSave.position = position;
        SaveManager.instance.activeSave.spears = SpearCounter.instance.spears;
        if (!GameObject.Find("SpearKaladinV1"))
        {
            Debug.Log("Saved 1 Spear");
            SaveManager.instance.activeSave.lanza1 = "SpearKaladinV1";
        }
        if (!GameObject.Find("SpearKaladinV2"))
        {
            Debug.Log("Saved 2 Spear");
            SaveManager.instance.activeSave.lanza2 = "SpearKaladinV2";
        }
        // Save Volume
        float volumeMusicValue;
        bool resultMusic = audioMixer.GetFloat("MusicVolume", out volumeMusicValue);
        float volumeEffectValue;
        bool resultEffect = audioMixer.GetFloat("EffectVolume", out volumeEffectValue);
        SaveManager.instance.activeSave.musicVolume = volumeMusicValue;
        SaveManager.instance.activeSave.effectVolume = volumeEffectValue;

        SaveManager.instance.Save();




        // isInTheCorner();
        isTouchingFrontFunction = frontCheckerFunction();

        moveInput = Input.GetAxis("Horizontal");

        #region Animations
        // Is in the ground variable
        if (isInTheGroundAll())
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
            animator.SetFloat("Horizontal", 0);
        }
        else
        {
            // Math Round because it goes to velocity like 10e-14
            if (Mathf.Round(rb.velocity.x) != 0)
            {
                animator.SetFloat("Horizontal", 1);
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
            }
        }

        // Vertical variable
        if (Mathf.Round(rb.velocity.y) > 0)
        {
            animator.SetFloat("Vertical", 1);
        }
        if (Mathf.Round(rb.velocity.y) < 0)
        {
            animator.SetFloat("Vertical", -1);
        }

        // Animation charge jump
        if (!PauseMenu.instance.GameIsPaused && isInTheGroundAll() && (Mathf.Round(rb.velocity.x) == 0) && Input.GetKey("space"))
        {
            // Animation jumpChargin true
            animator.SetBool("isCharginJump", true);
        }
        else
        {
            // Animation jumpChargin false
            animator.SetBool("isCharginJump", false);
        }

        //            END ANIMATION
        #endregion

        // Flip the Player facing right and left
        if (moveInput > 0 && facingRight == false && canMove)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight == true && canMove)
        {
            Flip();
        }

        // Bounce of the wall
        // isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 0.3f, jumpableGround);
        // NO SE COMO ARREGLAR QUE NO REBOTE EN LAS ESQUINAS
        // if (isTouchingFrontFunction && isInTheCorner())
        // {
        //     Debug.Log("no rebota 1");
        //     rb.sharedMaterial = normalMat;
        // }

        if (isTouchingFrontFunction && !isInTheGroundAll())
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }


        if (isInTheGroundAll() && Input.GetKeyUp("space") && jumpValue < maxJump && canMove && !PauseMenu.instance.GameIsPaused)
        {
            float tempx = moveInput * walkSpeep;
            float tempy = jumpValue;

            rb.velocity = new Vector2(tempx, tempy);

            Debug.Log("sonido saltar");
            if (isInTheGrass)
            {
                grassJumpSoundEffect.Play();
            }
            else if (isInTheCloud)
            {
                cloudJumpSoundEffect.Play();
            }
            else if (isInTheRocket)
            {
                rocketJumpSoundEffect.Play();
            }
            Invoke("resetJump", 0.2f);
        }

        // if the jumpValue if more than the max, make jump the player
        if (jumpValue >= maxJump && isInTheGroundAll() && canMove && !PauseMenu.instance.GameIsPaused)
        {
            // Debug.Log("salto mayor que el maximo");
            float tempx = moveInput * walkSpeep;
            float tempy = jumpValue;

            rb.velocity = new Vector2(tempx, tempy);

            Debug.Log("sonido saltar 2");
            if (isInTheGrass)
            {
                grassJumpSoundEffect.Play();
            }
            else if (isInTheCloud)
            {
                cloudJumpSoundEffect.Play();
            }
            else if (isInTheRocket)
            {
                rocketJumpSoundEffect.Play();
            }

            Invoke("resetJump", 0.2f);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        //Sound run
        if (rb.velocity.x != 0 && isInTheGrass && !PauseMenu.instance.GameIsPaused && ((Input.GetKey("right") || Input.GetKey("left")) && !Input.GetKey("space")))
        {
            Debug.Log("sonido andar");
            grassRunEffect.enabled = true;
        }
        else
        {
            grassRunEffect.enabled = false;
        }
        if (rb.velocity.x != 0 && isInTheCloud && !PauseMenu.instance.GameIsPaused && ((Input.GetKey("right") || Input.GetKey("left")) && !Input.GetKey("space")))
        {
            Debug.Log("sonido andar");
            cloudRunEffect.enabled = true;
        }
        else
        {
            cloudRunEffect.enabled = false;
        }
        if (rb.velocity.x != 0 && isInTheRocket && !PauseMenu.instance.GameIsPaused && ((Input.GetKey("right") || Input.GetKey("left")) && !Input.GetKey("space")))
        {
            Debug.Log("sonido andar");
            rocketRunEffect.enabled = true;
        }
        else
        {
            rocketRunEffect.enabled = false;
        }


        // Only can move if you are grounded
        if (isInTheGroundAll() && jumpValue <= 0 && canMove && !PauseMenu.instance.GameIsPaused)
        {
            rb.velocity = new Vector2(moveInput * walkSpeep, rb.velocity.y);
            if (!PauseMenu.instance.GameIsPaused && (rb.velocity.x != 0f && ((!Input.GetKey("right") && !Input.GetKey("left")) || Input.GetKey("space"))))
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }

        if (isInTheGroundAll())
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

        // Save the time charge of the jump force
        if (!PauseMenu.instance.GameIsPaused && Input.GetKey("space") && isInTheGroundAll())
        {
            Debug.Log("cargando");
            jumpValue += chargeJump * Time.deltaTime;
        }


        if (isInTheGroundAll())
        {
            if (maxYvelocity <= -1200f)
            {
                if (isInTheGrass)
                {
                    grassHardFloorEffect.Play();
                    animator.SetTrigger("kb");
                    maxYvelocity = 0;
                }
                if (isInTheCloud)
                {
                    cloudHardFloorEffect.Play();
                    animator.SetTrigger("kb");
                    maxYvelocity = 0;
                }
                if (isInTheRocket)
                {
                    rocketHardFloorEffect.Play();
                    animator.SetTrigger("kb");
                    maxYvelocity = 0;
                }

            }
        }

        if (!isInTheGroundAll())
        {
            if (rb.velocity.y < maxYvelocity)
            {
                maxYvelocity = rb.velocity.y;
            }
        }

        if (m4a4animator.GetCurrentAnimatorStateInfo(0).IsName("Knockdown") || m4a4animator.GetCurrentAnimatorStateInfo(0).IsName("Knockdown 0"))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    public bool isInTheGroundAll()
    {
        if (isInTheGrass || isInTheCloud || isInTheRocket)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grass")
        {
            grassJumpLandSoundEffect.Play();
            isInTheGrass = true;
        }
        if (other.gameObject.tag == "Cloud")
        {
            cloudJumpLandSoundEffect.Play();
            isInTheCloud = true;
        }
        if (other.gameObject.tag == "Rocket")
        {
            rocketJumpLandSoundEffect.Play();
            isInTheRocket = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grass")
        {
            isInTheGrass = false;
        }
        if (other.gameObject.tag == "Cloud")
        {
            isInTheCloud = false;
        }
        if (other.gameObject.tag == "Rocket")
        {
            isInTheRocket = false;
        }
    }
    public bool frontCheckerFunction()
    {
        // Right
        RaycastHit2D raycastWallRight = Physics2D.Raycast(colli.bounds.center, Vector2.right, colli.bounds.extents.x + 5f, jumpableGround);
        RaycastHit2D raycastWallRightUpper = Physics2D.Raycast(new Vector3(colli.bounds.max.x, colli.bounds.max.y, 0f), Vector2.right, colli.bounds.extents.x, jumpableGround);
        RaycastHit2D raycastWallRightBottom = Physics2D.Raycast(new Vector3(colli.bounds.max.x, colli.bounds.min.y, 0f), Vector2.right, colli.bounds.extents.x, jumpableGround);

        // Left
        RaycastHit2D raycastWallLeft = Physics2D.Raycast(colli.bounds.center, Vector2.left, colli.bounds.extents.x + 5f, jumpableGround);
        RaycastHit2D raycastWallLeftUpper = Physics2D.Raycast(new Vector3(colli.bounds.min.x, colli.bounds.max.y, 0f), Vector2.left, colli.bounds.extents.x, jumpableGround);
        RaycastHit2D raycastWallLeftBottom = Physics2D.Raycast(new Vector3(colli.bounds.min.x, colli.bounds.min.y, 0f), Vector2.left, colli.bounds.extents.x, jumpableGround);

        // Right
        Color rayColorRight;
        Color rayColorRightUpper;
        Color rayColorRightBottom;

        // Left
        Color rayColorLeft;
        Color rayColorLeftUpper;
        Color rayColorLeftBottom;

        // Right
        if (raycastWallRight.collider != null)
        {
            rayColorRight = Color.green;
        }
        else
        {
            rayColorRight = Color.red;
        }
        if (raycastWallRightUpper.collider != null)
        {
            rayColorRightUpper = Color.green;
        }
        else
        {
            rayColorRightUpper = Color.red;
        }
        if (raycastWallRightBottom.collider != null)
        {
            rayColorRightBottom = Color.green;
        }
        else
        {
            rayColorRightBottom = Color.red;
        }

        // Left
        if (raycastWallLeft.collider != null)
        {
            rayColorLeft = Color.green;
        }
        else
        {
            rayColorLeft = Color.red;
        }
        if (raycastWallLeftUpper.collider != null)
        {
            rayColorLeftUpper = Color.green;
        }
        else
        {
            rayColorLeftUpper = Color.red;
        }
        if (raycastWallLeftBottom.collider != null)
        {
            rayColorLeftBottom = Color.green;
        }
        else
        {
            rayColorLeftBottom = Color.red;
        }

        // Right
        Debug.DrawRay(colli.bounds.center, Vector2.right * (colli.bounds.extents.x + 5f), rayColorRight);
        Debug.DrawRay(new Vector3(colli.bounds.max.x, colli.bounds.max.y, 0f), Vector2.right * (colli.bounds.extents.x), rayColorRightUpper);
        Debug.DrawRay(new Vector3(colli.bounds.max.x, colli.bounds.min.y, 0f), Vector2.right * (colli.bounds.extents.x), rayColorRightBottom);

        // Left
        Debug.DrawRay(colli.bounds.center, Vector2.left * (colli.bounds.extents.x + 5f), rayColorLeft);
        Debug.DrawRay(new Vector3(colli.bounds.min.x, colli.bounds.min.y, 0f), Vector2.left * (colli.bounds.extents.x), rayColorLeftBottom);
        Debug.DrawRay(new Vector3(colli.bounds.min.x, colli.bounds.max.y, 0f), Vector2.left * (colli.bounds.extents.x), rayColorLeftUpper);

        return raycastWallRight.collider != null || raycastWallLeft.collider != null || raycastWallRightUpper.collider != null || raycastWallRightBottom.collider != null || raycastWallLeftBottom.collider != null || raycastWallLeftUpper.collider != null;
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
        // Debug.DrawRay(colli.bounds.center * new Vector2(1.025f, 1f), Vector2.down * (colli.bounds.extents.y + 9f), rayColorRight);
        return raycastHitRight.collider != null;
    }
}
