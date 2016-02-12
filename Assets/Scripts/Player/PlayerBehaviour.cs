using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    private Rigidbody2D playerRigidBody;
    // public Vector2 playerPos = Vector2.zero;
    private CharacterController controller;

    [SerializeField] private GameObject player;
    [SerializeField] private int playerState; // 0 = idle/grounded, 1 = aerial, 2 = walk, 3 = falling, 4 = hanging 999 = dood 
    [SerializeField] private int playerDirection = 2; // 1 = Left, 2 = Right
    [SerializeField] private int currentDirection = 2; // 1 = Left, 2 = Right;
    [SerializeField] private float runSpeed;
    [SerializeField] private float speedIncrease;
    [SerializeField] private float forceReset;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool grounded;


    // Use this for initialization
    void Awake() {
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        controller = player.GetComponent<CharacterController>();
        playerState = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Walk();
        
    }

    private void Walk() {
        //Walk Left
        if (Input.GetKey(KeyCode.A))
        {
                playerDirection = 1;
                TurnPlayer();
                runSpeed += speedIncrease;
                playerRigidBody.AddForce(-transform.right * runSpeed);
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            runSpeed = 0;
        }
        //Walk Right
        if (Input.GetKey(KeyCode.D))
        {
                playerDirection = 2;
                TurnPlayer();
                runSpeed += speedIncrease;
                playerRigidBody.AddForce(transform.right * runSpeed);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            runSpeed = 0;
           // playerRigidBody.AddForce();
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            // W
            if (controller.isGrounded) { }
            else {
                playerState = 1;
                playerRigidBody.AddForce(transform.up * jumpForce);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // W
            if (controller.isGrounded) { }
            else {
                playerState = 1;
                playerRigidBody.AddForce(transform.up * jumpForce);
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
