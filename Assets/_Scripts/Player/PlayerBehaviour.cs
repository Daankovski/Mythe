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
    float movementForce = 15f;
    GameObject chainCrosshair;
    public LayerMask mask;

    void Awake()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        chainCrosshair = GameObject.Find("chainCrosshair");
        playerAnims = GameObject.Find("Player").GetComponent<Animator>();       
        
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
        PlayerControls();
    }

    public float MaxSpeed {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }


    void GroundCheck() { // Check if there is a collision with the ground via Raycast.
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
            playerRigidBody.AddForce(new Vector2(1,0) * direction * movementForce);
        }

        if (playerRigidBody.velocity.x > maxSpeed)
        {
            playerRigidBody.velocity = playerRigidBody.velocity.normalized * maxSpeed;
        }

        if (playerRigidBody.velocity.x < -maxSpeed)
        {
            playerRigidBody.velocity = -playerRigidBody.velocity.normalized * -maxSpeed;
        }

        if (direction > 0 && !isFacingRight)
        {
            TurnPlayer();
        }
        else if (direction < 0 && isFacingRight) {
            TurnPlayer();
        }

        //Jumps
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                isGrounded = false;
                playerRigidBody.AddForce(Vector2.up * jumpForce);
                playerAnims.SetTrigger("isJump");
            }
            else
            {
                isGrounded = true;
            }
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