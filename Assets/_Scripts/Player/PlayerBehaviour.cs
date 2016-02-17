using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    private Rigidbody2D playerRigidBody;
    // public Vector2 playerPos = Vector2.zero;

    [SerializeField] private GameObject player;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private int playerState; // 0 = idle/grounded, 1 = aerial, 2 = walk, 3 = falling, 4 = hanging 999 = dood 
    [SerializeField] private int playerDirection = 2; // 1 = Left, 2 = Right
    [SerializeField] private int currentDirection = 2; // 1 = Left, 2 = Right;
    [SerializeField] private float runSpeed;
    [SerializeField] private float speedIncrease;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float forceReset;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool grounded;


    [SerializeField] private BoxCollider2D chainCollider;
    [SerializeField] private SpriteRenderer chainSprite;
    [SerializeField] private bool isClickedOnce;
    [SerializeField] private float DCTimer;
    [SerializeField] private Vector2 fwd;
    [SerializeField] private RaycastHit2D hit;


    // Use this for initialization
    void Awake() {
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerState = 0;
        //chainCollider = GameObject.Find("chain").GetComponent<BoxCollider2D>();
        chainSprite = GameObject.Find("chain").GetComponent<SpriteRenderer>();
        chainSprite.enabled = false;
        //chainCollider.enabled = false;
        maxSpeed = 25;
        fwd = transform.TransformDirection(Vector3.forward);
    }

    void Update() {
        Attack();
        Walk();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }

    private void Walk() {
        //Walk Left
        if (Input.GetKey(KeyCode.A))
        {
                playerDirection = 1;
                TurnPlayer();
            if (runSpeed <= maxSpeed)
            {
                runSpeed += speedIncrease;
                playerRigidBody.AddForce(-transform.right * runSpeed);
            }
            else {
                runSpeed = maxSpeed;
            }
                
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            runSpeed = 0;
        }
        //Walk Right
        if (Input.GetKey(KeyCode.D))
        {
                playerDirection = 2;
                TurnPlayer();
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
            if (grounded) {
                grounded = false;
                playerState = 1;
                playerRigidBody.AddForce(transform.up * jumpForce);
            }
            else {
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

    void Attack() {
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(ChainAttack(.75f));

        }
    }

    IEnumerator ChainAttack(float waitTime) {
        StartCoroutine(DoubleClick(.2f));
       // chainCollider.enabled = true;
        if (Physics2D.Raycast(player.transform.position, transform.forward, 20)) {
            print("chain shot");
            Debug.DrawLine(player.transform.position, hit.point, Color.red);
            print("There is something in front of the object!");
            
        }
        yield return new WaitForSeconds(waitTime);
        //chainCollider.enabled = false;
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
        if (isClickedOnce) {
            if ((Time.time - DCTimer) > waitTime)
            {
                isClickedOnce = false;
            }
        }

        yield return new WaitForSeconds(waitTime);
    }

    int CheckDirection(int currentDirection)
    {
        if (playerDirection == 1) { return 1; }
        else if (playerDirection == 2) { return 2; }
        else { Debug.Log("Something is wrong, playerDirection is other than 1 or 2"); return 0;}
    }
    //Laat de speler sprite omdraaien
    private void TurnPlayer()
    {
        if (playerDirection == 1)
        {
            playerDirection = 2;
            //Player is facing left
            //Turn player sprite right
        }
        if (playerDirection == 2)
        {
            playerDirection = 1;
            //Player is facing right
            //Turn Sprite Left
        }
        else { Debug.Log("Something is wrong, playerDirection is other than 1 or 2");}

    }
}
