using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    // public Vector2 playerPos = Vector2.zero;

    [SerializeField]   private GameObject player;
    [SerializeField]   private BoxCollider2D playerCollider;
    [SerializeField]   private int playerState; // 0 = idle/grounded, 1 = aerial, 2 = walkLeft, 3 = walkRight 4 = falling, 5 = hanging 999 = dood 
    [SerializeField]   private int playerDirection; // 1 = Left, 2 = Right
    [SerializeField]   private float runSpeed;
    [SerializeField]   private float speedIncrease;
    [SerializeField]   private float maxSpeed;
    [SerializeField]   private float forceReset;
    [SerializeField]   private float jumpForce;
    [SerializeField]   private bool grounded = false;
    [SerializeField]   private bool isFacingRight;
    [SerializeField]   private SpriteRenderer playerSprite;
    [SerializeField]   private BoxCollider2D chainCollider;
    [SerializeField]   private LineRenderer chainLineRenderer;
    [SerializeField]   private float maxChainLength;
                       private bool isClickedOnce;
                       private float DCTimer;
                       private Vector2 currentOffset;
                       private Vector2 currentEndPos;
                       private Vector2 endPoint;
                       private Transform chainStart, chainEnd, groundCheck;
                       private bool isAttacking = false;
    private RaycastHit2D hit;



    // Use this for initialization
    void Awake()
    {
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        chainCollider = GameObject.Find("chain").GetComponent<BoxCollider2D>();
        playerSprite = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        chainLineRenderer = GameObject.Find("chain").GetComponent<LineRenderer>();
        groundCheck = GameObject.Find("groundCheck").transform;
        chainStart = GameObject.Find("chainStart").transform;
        chainEnd = GameObject.Find("chainEnd").transform;
        
    }

    void Start()
    {
        isFacingRight = true;
        chainCollider.enabled = false;
        chainLineRenderer.enabled = false;
        maxSpeed = 25;
        endPoint = new Vector2(chainCollider.transform.position.x + 8, .3f);
        currentOffset = chainCollider.offset;
        currentEndPos = endPoint;

    }

    void Update()
    {
        Attack();
        Walk();
        Raycasting();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }


    void Raycasting() {
        Debug.DrawLine(this.transform.position, groundCheck.position, Color.green);
        grounded = Physics2D.Linecast(this.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetMouseButtonDown(0)) {
            Debug.DrawLine(chainStart.position, chainEnd.position, Color.red);
            if (Physics2D.Linecast(chainStart.position, chainEnd.position, 1 << LayerMask.NameToLayer("Enemy")))
            {
                hit = Physics2D.Linecast(chainStart.position, chainEnd.position, 1 << LayerMask.NameToLayer("Enemy"));
            }
        
        }

    }


    private void Walk()
    {
        //Walk Left
        if (Input.GetKey(KeyCode.A))
        {
            playerDirection = 1;
            StartCoroutine(TurnPlayer(playerDirection));
            if (runSpeed <= maxSpeed)
            {
                runSpeed += speedIncrease;
                playerRigidBody.AddForce(-transform.right * runSpeed);
            }
            else
            {
                runSpeed = maxSpeed;
            }

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            runSpeed = 0;
        }
        //Walk Right
        if (Input.GetKey(KeyCode.D))
        {
            playerDirection = 2;
            StartCoroutine(TurnPlayer(playerDirection));
            if (runSpeed <= maxSpeed)
            {
                runSpeed += speedIncrease;
                playerRigidBody.AddForce(transform.right * runSpeed);
            }
            else
            {
                runSpeed = maxSpeed;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            runSpeed = 0;
        }
        //Crouch
        if (Input.GetKey(KeyCode.C))
        {
            // Crouch();
        }
        //Dodge
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //  Dodge();
        }
        //Jumps
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            // W
            if (grounded)
            {
                grounded = false;
                playerState = 1;
                playerRigidBody.AddForce(Vector2.up * jumpForce);
            }
            else
            {
                grounded = true;
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

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(ChainAttack(.75f));
            StartCoroutine(ChainAttack(.75f));

        }
        else {
            StopCoroutine(ChainAttack(0f));
            chainLineRenderer.SetPosition(0, transform.position);
        }
    }

    IEnumerator ChainAttack(float waitTime)
    {
        StartCoroutine(DoubleClick(.15f));
        chainCollider.enabled = true;
        chainLineRenderer.enabled = true;
        maxChainLength = 8;
        chainLineRenderer.SetPosition(1, endPoint);
        yield return new WaitForSeconds(waitTime);
        chainCollider.enabled = false;
        chainLineRenderer.enabled = false;
    }

    IEnumerator DoubleClick(float waitTime)
    {
        if (!isClickedOnce)
        {
            isClickedOnce = true;
            DCTimer = Time.time;
            print("clicked once");
            yield return new WaitForSeconds(waitTime);
            isClickedOnce = false;
        }
        else
        {
            isClickedOnce = false;
            print("clicked twice");
            // Do Double Click things
            // Change shape of BoxCollider
        }
        if (isClickedOnce)
        {
            if ((Time.time - DCTimer) > waitTime)
            {
                isClickedOnce = false;
            }
        }

        yield return new WaitForSeconds(waitTime);
    }

    IEnumerator TurnPlayer(int playerDirection) {
        yield return playerDirection;

        if (playerDirection == 1)
        {
            playerSprite.color = Color.black;
            playerState = 2;
            isFacingRight = false;
        }
        else if (playerDirection == 2)
        {
            playerSprite.color = Color.white;
            playerState = 3;
            isFacingRight = true;
        }

        switch (isFacingRight)
        {
            case true:
                chainCollider.offset = -currentOffset * -1;
                endPoint.x = -currentEndPos.x * -1;
                break;
            case false:
                chainCollider.offset = currentOffset * -1;
                endPoint.x = currentEndPos.x * -1;
                break;
        }
    }

}