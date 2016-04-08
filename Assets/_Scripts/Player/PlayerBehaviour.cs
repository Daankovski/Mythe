using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    [SerializeField]   private GameObject player;
    [SerializeField]   private BoxCollider2D playerCollider;
    [SerializeField]   private float maxSpeed;
    [SerializeField]   private float forceReset;
    [SerializeField]   private float jumpForce;
    [SerializeField]   private bool isGrounded = false;
    [SerializeField]   private bool isFacingRight;
    [SerializeField]   private Animator playerAnims;
    [SerializeField]   private Transform groundCheck;
    [SerializeField]   private float maxJumpAmplifier;
    private float jumpAmplifier;
    [SerializeField]   private float attackDamage;
    float movementForce = 15f;
    GameObject chainCrosshair;
    public LayerMask mask;
    BoxCollider2D bladeCollider;



    // Use this for initialization
    void Awake()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        chainCrosshair = GameObject.Find("chainCrosshair");
        playerAnims = GameObject.Find("Player").GetComponent<Animator>();
        bladeCollider = GameObject.Find("bladeCollider").GetComponent<BoxCollider2D>();

    }

    void Start()
    {
        isFacingRight = true;
        maxSpeed = 7;
        jumpForce = 600f;
        maxJumpAmplifier = 3;
        jumpAmplifier = 1;
        bladeCollider.enabled = false;
    }

    void Update()
    { 
        GroundCheck();
        PlayerControls();
        Debug.Log(jumpAmplifier);
    }

    public float MaxSpeed {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }


    void GroundCheck() { // Check if there is a collision with the ground via Raycast.
       // groundCheck = GameObject.Find("groundCheck").transform;
        Debug.DrawLine(transform.position, groundCheck.position, Color.green);
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, mask);
        playerAnims.SetBool("isGrounded",isGrounded);

    }


    private void PlayerControls()
    {
        float direction = Input.GetAxis("Horizontal");
        playerAnims.SetFloat("direction", direction);

        if (direction * playerRigidBody.velocity.x < maxSpeed || direction * playerRigidBody.velocity.x > maxSpeed)
        {
            playerRigidBody.AddForce(Vector2.right * direction * movementForce);
        }

        if (playerRigidBody.velocity.x > maxSpeed)
        {
            playerRigidBody.velocity = playerRigidBody.velocity.normalized * maxSpeed;
        }

        if (direction > 0 && !isFacingRight)
        {
            TurnPlayer();
        }
        else if (direction < 0 && isFacingRight) {
            TurnPlayer();
        }

        //Jumps
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpAmplifier != maxJumpAmplifier)
            {
                jumpAmplifier++;
            }
     
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isGrounded)
            {
                isGrounded = false;
                playerRigidBody.AddForce(Vector2.up * (jumpForce * jumpAmplifier));
                playerAnims.SetTrigger("isJump");
                jumpAmplifier = 1;

            }
            else
            {
                isGrounded = true;
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            playerAnims.SetTrigger("isBladeAttack");
            
            bladeCollider.enabled = true;
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