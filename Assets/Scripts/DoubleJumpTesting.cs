using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoubleJumpTesting : MonoBehaviour
{
    public AudioSource coinAudioSource;
    public float walkSpeed = 8f;
    public float jumpSpeed = 10f;

    bool touchingWallLeft = false;
    bool touchingWallRight = false;
    bool jumpedRight = false;
    bool jumpedLeft = false;
    float wallTouchRadius = 0.6f;
    public GameObject MovingPlatform;
    public Rigidbody MovPlat;
    float thrust = 20f;



    // access the HUD
    public HudManager hud;

    //to keep our rigid body
    Rigidbody rb;

    //to keep the collider object
    Collider coll;

    //flag to keep track of whether a jump started
    bool pressedJump = false;

    // Use this for initialization
    void Start()
    {

        //rigid body of moving platform. drag and drop in unity
        MovPlat = MovPlat.GetComponent<Rigidbody>();

        //get the rigid body component for later use
        rb = GetComponent<Rigidbody>();

        //get the player collider
        coll = GetComponent<Collider>();

        //refresh the HUD
        hud.Refresh();
    }


    // Update is called once per frame
    void Update()
    {
        //checks if player is on top of trampoline

        float sizeX = coll.bounds.size.x;
        float sizeZ = coll.bounds.size.z;
        float sizeY = coll.bounds.size.y;

        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.1f, sizeZ / 2);

        //mask is used for the raycast. You can make your own layers, i just used default layers
        LayerMask trampolineMask = LayerMask.GetMask("UI");
        if (Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.3f, trampolineMask))
        {
            /*
            float temp = rb.velocity.y / 4;

            if (temp < 0f)
            {
                temp = temp * (-1);
            }
            if (temp > 10f)
            {
                temp = 10f;
            }
            thrust += temp;
            */
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(0, thrust, 0, ForceMode.Impulse);
        }

        // Handle player walking
        WalkHandler();

        //Handle player jumping
        JumpHandler();


    }

   



    // Make the player walk according to user input
    void WalkHandler()
    {
        //this first section checks to see if the player is on the moving platform
        float sizeX = coll.bounds.size.x;
        float sizeZ = coll.bounds.size.z;
        float sizeY = coll.bounds.size.y;

        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.1f, sizeZ / 2);

        //mask is used for the raycast. You can make your own layers, i just used default layers
        LayerMask movingPlatformMask = LayerMask.GetMask("Water");

        if (Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.5f, movingPlatformMask))
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            rb.velocity += MovPlat.velocity;
        }

        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }


        // Distance ( speed = distance / time --> distance = speed * time)
        float distance = walkSpeed * Time.deltaTime;

        // Input on x ("Horizontal")
        float hAxis = Input.GetAxis("Horizontal");

        // Input on z ("Vertical")
        float vAxis = Input.GetAxis("Vertical");

        // Movement vector
        Vector3 movement = new Vector3(hAxis * distance, 0f, vAxis * distance);

        // Current position
        Vector3 currPosition = transform.position;

        // New position
        Vector3 newPosition = currPosition + movement;

        // Move the rigid body
        rb.MovePosition(newPosition);
    }

    // Check whether the player can jump and make it jump
    void JumpHandler()
    {
        // Jump axis
        float jAxis = Input.GetAxis("Jump");

        // Is grounded
        bool isGrounded = CheckGrounded();


        //Is touching a wall
        //TODO add masks for the walls. check all directions of player, not just left and right
        touchingWallLeft = (Physics.Raycast(transform.position, Vector3.left, wallTouchRadius));
        touchingWallRight = (Physics.Raycast(transform.position, Vector3.right, wallTouchRadius));


        // Check if the player is pressing the jump key
        if (jAxis > 0f)
        {
            // Make sure we've not already jumped on this key press
            //this makes sure that, if we are touching a wall, we didnt just jump from that wall
            if (!pressedJump && (isGrounded || (touchingWallLeft && !jumpedLeft) || (touchingWallRight && !jumpedRight)))
            {
                // We are jumping on the current key press
                pressedJump = true;

                //if the player just wall jumped, the player cannot jump on that same wall
                if (touchingWallLeft)
                {
                    jumpedLeft = true;
                    jumpedRight = false;
                }
                if (touchingWallRight)
                {
                    jumpedRight = true;
                    jumpedLeft = false;
                }
                if (isGrounded)
                {
                    jumpedRight = false;
                    jumpedLeft = false;
                }

                // Jumping vector
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);

                // Make the player jump by adding velocity
                rb.velocity = jumpVector;
            }
        }
        else
        {
            // Update flag so it can jump again if we press the jump key
            pressedJump = false;
        }
    }

    // Check if the object is grounded
    bool CheckGrounded()
    {
        // Object size in x
        float sizeX = coll.bounds.size.x;
        float sizeZ = coll.bounds.size.z;
        float sizeY = coll.bounds.size.y;

        // Position of the 4 bottom corners of the game object
        // We add 0.01 in Y so that there is some distance between the point and the floor
        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);

        // Send a short ray down the cube on all 4 corners to detect ground
        bool grounded1 = Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.1f);
        bool grounded2 = Physics.Raycast(corner2, new Vector3(0, -1, 0), 0.1f);
        bool grounded3 = Physics.Raycast(corner3, new Vector3(0, -1, 0), 0.1f);
        bool grounded4 = Physics.Raycast(corner4, new Vector3(0, -1, 0), 0.1f);

        // If the bottom is touching the floor
        return (grounded1);
    }




    /*
    void OnTriggerEnter(Collider collider)
    {
        // Check if we ran into a coin
        if (collider.gameObject.tag == "Coin")
        {
            print("Grabbing coin..");

            // Increase score
            GameManager.instance.IncreaseScore(1);

            //refresh the HUD
            hud.Refresh();

            // Play coin collection sound
            coinAudioSource.Play();

            // Destroy coin
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "Enemy")
        {
            // Game over
            print("game over");

            SceneManager.LoadScene("Game Over");
        }
        else if (collider.gameObject.tag == "Goal")
        {
            print("goal reached");

            // Increase level
            GameManager.instance.IncreaseLevel();
        }


    }*/
}