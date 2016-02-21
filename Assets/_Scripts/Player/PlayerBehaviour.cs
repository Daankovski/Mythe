using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    [SerializeField]   private GameObject player;
    [SerializeField]   private BoxCollider2D playerCollider;
    [SerializeField]   private int playerState; // 0 = idle/grounded, 1 = aerial, 2 = walkLeft, 3 = walkRight 4 = falling, 5 = hanging 999 = dood 
    [SerializeField]   private int playerDirection; // 1 = Left, 2 = Right
    [SerializeField]   private float runSpeed;
    [SerializeField]   private float speedIncrease;
    [SerializeField]   private float maxSpeed;
    [SerializeField]   private float forceReset;
    [SerializeField]   private float jumpForce;
    [SerializeField]   private bool isGrounded = false;
    [SerializeField]   private bool isFacingRight;
    [SerializeField]   private SpriteRenderer playerSpriteRenderer;
    private GameObject playerSprite; //Temporary until Animations are ready;
    private Transform groundCheck;
    float movementForce = 20f;
    GameObject chainCrosshair;
    public LayerMask mask;



    // Use this for initialization
    void Awake()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerSprite = GameObject.Find("PlayerSprite");
        playerSpriteRenderer = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        groundCheck = GameObject.Find("groundCheck").transform;
        chainCrosshair = GameObject.Find("chainCrosshair");        
        
    }

    void Start()
    {
        isFacingRight = true;
        maxSpeed = 7;
        jumpForce = 600f;
    }

    void Update()
    { 
        GroundCheck();
    }

    void FixedUpdate()

    {
        PlayerControls();
    }

    public float MaxSpeed {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }


    void GroundCheck() { // Check if there is a collision with the ground via Raycast.
        //Debug.DrawLine(this.transform.position, groundCheck.position, Color.green);
        isGrounded = Physics2D.Linecast(this.transform.position, groundCheck.position, mask);

    }


    private void PlayerControls()
    {
        float direction = Input.GetAxis("Horizontal");

        if (direction * playerRigidBody.velocity.x < maxSpeed)
        {
            playerRigidBody.AddForce(Vector2.right * direction * movementForce);
        }
        if (Mathf.Abs(playerRigidBody.velocity.x) > maxSpeed) {
            playerRigidBody.velocity = new Vector2(Mathf.Sign(playerRigidBody.velocity.x) * maxSpeed, playerRigidBody.velocity.y);
        }

        if (direction > 0 && !isFacingRight)
        {
            TurnPlayer();
        }
        else if (direction < 0 && isFacingRight) {
            TurnPlayer();
        }



        //Crouch
        if (Input.GetKey(KeyCode.C))
        {
            //Animatie trigger
            playerCollider.offset = new Vector2(0f, -0.64f);
            playerCollider.size = new Vector2(1f, 1.25f);
            playerSprite.transform.localScale = new Vector2(1f,0.5f);
            playerSprite.transform.localPosition = new Vector2(0f, -.64f);
        }
        else {
            playerCollider.offset = new Vector2(0f, 0f);
            playerCollider.size = new Vector2(1f, 2.5f);
            playerSprite.transform.localScale = new Vector2(1f, 1f);
            playerSprite.transform.localPosition = new Vector2(0f, 0f);
        }
        //Dodge
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //  Dodge();
        }
        //Jumps
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // W
            if (isGrounded)
            {
                isGrounded = false;
                playerState = 1;
                playerRigidBody.AddForce(Vector2.up * jumpForce);
            }
            else
            {
                isGrounded = true;
            }
        }
        //Interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Interaction
        }

        //Weapon activation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 1
            //Activate Weapon 1(Sword)
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 2
            //Activate Weapon 2(Chain)
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // 3
            //Activate Weapon 3(Javelin)
        }
    }

    void TurnPlayer() // Turns the players Sprite to the direction it walks.
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}