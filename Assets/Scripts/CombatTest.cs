using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public class CombatTest : MonoBehaviour
//{
//    public AudioSource coinAudioSource;
//    public float walkSpeed = 8f;
//    public float jumpSpeed = 7f;
//    public float turnSpeed = 300f;

//    // access the HUD
//    public HudManager hud;

//    //to keep our rigid body
//    Rigidbody rb;

//    //to keep the collider object
//    Collider coll;

//    //flag to keep track of whether a jump started
//    bool pressedJump = false;

//    // Use this for initialization
//    void Start()
//    {
//        //get the rigid body component for later use
//        rb = GetComponent<Rigidbody>();

//        //get the player collider
//        coll = GetComponent<Collider>();

//        //refresh the HUD
//        hud.Refresh();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Handle player walking
//        WalkHandler();

//        //Handle player jumping
//        JumpHandler();
//    }

//    // Make the player walk according to user input
//    void WalkHandler()
//    {
//        // Set x and z velocities to zero
//        rb.velocity = new Vector3(0, rb.velocity.y, 0);

//        // Distance ( speed = distance / time --> distance = speed * time)
//        float distance = walkSpeed * Time.deltaTime;

//        // Input on x ("Horizontal")
//        float hAxis = Input.GetAxis("Horizontal");

//        // Input on z ("Vertical")
//        float vAxis = Input.GetAxis("Vertical");

//        // Movement vector
//        Vector3 movement = new Vector3(hAxis * distance, 0f, vAxis * distance);

//        // Current position
//        Vector3 currPosition = transform.position;

//        // New position
//        Vector3 newPosition = currPosition + movement;

//        // Move the rigid body
//        rb.MovePosition(newPosition);
//    }

//    // Check whether the player can jump and make it jump
//    void JumpHandler()
//    {
//        // Jump axis
//        float jAxis = Input.GetAxis("Jump");

//        // Is grounded
//        bool isGrounded = CheckGrounded();

//        // Check if the player is pressing the jump key
//        if (jAxis > 0f)
//        {
//            // Make sure we've not already jumped on this key press
//            if (!pressedJump && isGrounded)
//            {
//                // We are jumping on the current key press
//                pressedJump = true;

//                // Jumping vector
//                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);

//                // Make the player jump by adding velocity
//                rb.velocity = rb.velocity + jumpVector;
//            }
//        }
//        else
//        {
//            // Update flag so it can jump again if we press the jump key
//            pressedJump = false;
//        }
//    }

//    // Check if the object is grounded
//    bool CheckGrounded()
//    {
//        // Object size in x
//        float sizeX = coll.bounds.size.x;
//        float sizeZ = coll.bounds.size.z;
//        float sizeY = coll.bounds.size.y;

//        // Position of the 4 bottom corners of the game object
//        // We add 0.01 in Y so that there is some distance between the point and the floor
//        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
//        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
//        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
//        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);

//        // Send a short ray down the cube on all 4 corners to detect ground
//        bool grounded1 = Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.01f);
//        bool grounded2 = Physics.Raycast(corner2, new Vector3(0, -1, 0), 0.01f);
//        bool grounded3 = Physics.Raycast(corner3, new Vector3(0, -1, 0), 0.01f);
//        bool grounded4 = Physics.Raycast(corner4, new Vector3(0, -1, 0), 0.01f);

//        // If any corner is grounded, the object is grounded
//        return (grounded1 || grounded2 || grounded3 || grounded4);
//    }

//    void OnTriggerEnter(Collider collider)
//    {
//        // Check if we ran into a coin
//        if (collider.gameObject.tag == "Coin")
//        {
//            print("Grabbing coin..");

//            // Increase score
//            GameManager.instance.IncreaseScore(1);

//            //refresh the HUD
//            hud.Refresh();

//            // Play coin collection sound
//            coinAudioSource.Play();

//            // Destroy coin
//            Destroy(collider.gameObject);
//        }
//        else if (collider.gameObject.tag == "Enemy")
//        {
//            // Game over
//            print("game over");

//            SceneManager.LoadScene("Game Over");
//        }
//        else if (collider.gameObject.tag == "Goal")
//        {
//            print("goal reached");

//            // Increase level
//            GameManager.instance.IncreaseLevel();
//        }

//    }
//}

[RequireComponent(typeof(CharacterController))]
public class CombatTest : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float sneakSpeed = 2.5f;
    public float runSpeed = 8.0f;
    public float crouchWalkSpeed = 3.5f;
    public float crouchRunSpeed = 6.5f;
    public float crouchSneakSpeed = 1f;
    public float jumpSpeed = 6.0f;

    public bool limitDiagonalSpeed = true;
    public bool toggleRun = false;
    public bool toggleSneak = false;
    public bool airControl = true; // strafing / b-hop
    public bool firstPerson = false;
    public bool crouching = false;

    public float gravity = 10.0f;
    public float fallingDamageLimit = 10.0f;

    private Vector3 moveDirection;

    private bool grounded;
    private CharacterController controller;
    private Transform myTransform;
    private float speed;
    private RaycastHit hit;
    private float fallStartLevel;
    private bool falling;
    private bool punching;
    private bool playerControl;
    private Animator anim;


    // Use this for initialization
    void Start()
    {
        moveDirection = Vector3.zero;
        grounded = false;
        playerControl = false;
        controller = GetComponent<CharacterController>();
        myTransform = transform;
        speed = walkSpeed;
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f && limitDiagonalSpeed) ? 0.6701f : 1.0f;

        anim.SetFloat("BlendX", (inputX * 2));
        anim.SetFloat("BlendY", (inputY * 2));
        anim.SetBool("Walking", (anim.GetFloat("BlendX") != 0 || anim.GetFloat("BlendY") != 0));

        anim.SetBool("Punching", Input.GetButton("Fire1"));

        if (grounded)
        {

            if (falling)
            {
                falling = false;
                if (myTransform.position.y < (fallStartLevel - fallingDamageLimit))
                {
                    FallingDamageAlert(fallStartLevel - myTransform.position.y);
                }
            }

            if (!toggleRun)
            {
                bool running = Input.GetButton("Run");
                speed = running ? runSpeed : walkSpeed;
                anim.SetBool("Running", running);
            }

            else
            {
                anim.SetBool("Running", true);
            }

            if (!toggleSneak)
            {
                bool sneaking = Input.GetButton("Sneak");
                speed = sneaking ? sneakSpeed : speed;
                anim.SetBool("Sneaking", sneaking);
            }

            if (crouching)
            {
                speed = Input.GetButton("Run") ? crouchRunSpeed : crouchWalkSpeed;
                speed = Input.GetButton("Sneak") ? crouchSneakSpeed : speed;


            }

            //print(speed);
            moveDirection = new Vector3(inputX * inputModifyFactor, 0, inputY * inputModifyFactor);
            moveDirection = myTransform.TransformDirection(moveDirection) * speed;

            if (!Input.GetButton("Jump"))
            {
                anim.SetBool("Jump", false);
            }
            else
            {
                moveDirection.y = jumpSpeed;

                anim.SetBool("Jump", true);
            }
        }
        else
        {
            if (!falling)
            {
                falling = true;
                fallStartLevel = myTransform.position.y;
            }

            if (airControl && playerControl)
            {
                moveDirection.x = inputX * speed * inputModifyFactor;
                moveDirection.z = inputY * speed * inputModifyFactor;
                moveDirection = myTransform.TransformDirection(moveDirection);
            }
        }

        grounded = (controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
        moveDirection.y -= gravity * Time.deltaTime;


    }

    void Update()
    {
        if (toggleRun && grounded && Input.GetButtonDown("Run"))
            speed = (speed == walkSpeed ? runSpeed : walkSpeed);

        if (Input.GetButtonUp("Crouch"))
        {
            crouching = !crouching;
            anim.SetBool("Crouch", crouching);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit.point);
    }

    void FallingDamageAlert(float fallDistance)
    {
        print("Ouch! Fell " + fallDistance + " units!");
    }
}
