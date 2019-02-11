using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    //if you have any questions about how this works, ask me. Right now, this script only allows for one enemy in the world. You can easily add more GameObjects
    //to this script called enemy1, enemy2, etc. You'd have to keep track of which enemy is right in front of the player, probably by using a different layer
    //and mask per enemy (for the raycasts). the bool 'beenPunched' only applies to one enemy. If you want to add more enemies, you need more booleans. 
    //so you'd check if an enemy is in front of the player when the attack button is pressed (multiple raycasts and 'if' statements, one per enemy/layer/mask)
    //and inside each if statement, you'd check the boolean that goes with the enemy in front of the player to see if its been attacked already.
      
    //you have to create two identical blocks for the fists. do not make them rigidbodies. disable the box collider. make them children of the player.
    //attach this script to one of the fists

    //these are the blocks inside the player. they are PUBLIC so that, in Unity, you can drag them to the script (you'll see it)
    GameObject punchRight;
    GameObject punchLeft;
    GameObject kick;

    public Rigidbody enemy1;
    public Rigidbody enemy2;
    public Rigidbody enemy3;
    public Rigidbody enemy4;
    public Rigidbody enemy5;
    public Rigidbody enemy6;
    public Rigidbody enemy7;
    public Rigidbody enemy8;
    public Rigidbody enemy9;
    public Rigidbody enemy10;
    public Rigidbody enemy11;
    public Rigidbody enemy12;

    public GameObject enemy1Object;
    public GameObject enemy2Object;
    public GameObject enemy3Object;
    public GameObject enemy4Object;
    public GameObject enemy5Object;
    public GameObject enemy6Object;
    public GameObject enemy7Object;
    public GameObject enemy8Object;
    public GameObject enemy9Object;
    public GameObject enemy10Object;
    public GameObject enemy11Object;
    public GameObject enemy12Object;

    LayerMask enemyMask1;
    LayerMask enemyMask2;
    LayerMask enemyMask3;
    LayerMask enemyMask4;
    LayerMask enemyMask5;
    LayerMask enemyMask6;
    LayerMask enemyMask7;
    LayerMask enemyMask8;
    LayerMask enemyMask9;
    LayerMask enemyMask11;
    LayerMask enemyMask12;
    LayerMask enemyMask10;

    GameObject player;

    //counter to decide which fist/block (left or right) punches
    int i = 0;

    //indicates if the enemy has been punched
    bool beenPunched1 = false;
    bool beenPunched2 = false;
    bool beenPunched3 = false;
    bool beenPunched4 = false;
    bool beenPunched5 = false;
    bool beenPunched6 = false;
    bool beenPunched7 = false;
    bool beenPunched8 = false;
    bool beenPunched9 = false;
    bool beenPunched10 = false;
    bool beenPunched11 = false;
    bool beenPunched12 = false;


    // Start is called before the first frame update
    void Start()
    {
        punchLeft = GameObject.Find("PunchLeft");
        punchRight = GameObject.Find("PunchRight");
        kick = GameObject.Find("Kick");
        player = GameObject.Find("Player");
        enemy1 = enemy1.GetComponent<Rigidbody>();
        enemy2 = enemy2.GetComponent<Rigidbody>();
        enemy3 = enemy3.GetComponent<Rigidbody>();
        enemy4 = enemy4.GetComponent<Rigidbody>();
        enemy5 = enemy5.GetComponent<Rigidbody>();
        enemy6 = enemy6.GetComponent<Rigidbody>();
        enemy7 = enemy7.GetComponent<Rigidbody>();
        enemy8 = enemy8.GetComponent<Rigidbody>();
        enemy9 = enemy9.GetComponent<Rigidbody>();
        enemy10 = enemy10.GetComponent<Rigidbody>();
        enemy11 = enemy11.GetComponent<Rigidbody>();
        enemy12 = enemy12.GetComponent<Rigidbody>();

        enemyMask1 = LayerMask.GetMask("enemy1");
        enemyMask2 = LayerMask.GetMask("enemy2");
        enemyMask3 = LayerMask.GetMask("enemy3");
        enemyMask4 = LayerMask.GetMask("enemy4");
        enemyMask5 = LayerMask.GetMask("enemy5");
        enemyMask6 = LayerMask.GetMask("enemy6");
        enemyMask7 = LayerMask.GetMask("enemy7");
        enemyMask8 = LayerMask.GetMask("enemy8");
        enemyMask9 = LayerMask.GetMask("enemy9");
        enemyMask10 = LayerMask.GetMask("enemy10");
        enemyMask11 = LayerMask.GetMask("enemy11");
        enemyMask12 = LayerMask.GetMask("enemy12");



    }

    // Update is called once per frame
    void Update()
    {
        //space is punch
        if (Input.GetKeyDown("mouse 1"))
        {

            //if the enemy is right in front of the player
            //player.transform.position is the position of the player
            //player.transform.forward is the direction the player is facing
            //1f is the raycast distance. this might need to be shorter
            //enemyMask is the mask defined above
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask1))
            {
                //if the enemy has already been punched, "delete" him. Idk how to actually remove something from here yet, but this works
                if (beenPunched1)
                {
                    //moves enemy far away below the map
                    Destroy(enemy1Object);

                }
                //else if enemy has not been punched, change beenPunched to true (enemy has two hits until death) and knock back the enemy
                else
                {
                    enemy1.AddForce(player.transform.forward * (100) + new Vector3(0, 200, 0));
                    beenPunched1 = true;
                }
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask2))
            {
                //if the enemy has already been punched, "delete" him. Idk how to actually remove something from here yet, but this works
                if (beenPunched2)
                {
                    //moves enemy far away below the map
                    Destroy(enemy2Object);
                }
                //else if enemy has not been punched, change beenPunched to true (enemy has two hits until death) and knock back the enemy
                else
                {
                    enemy2.AddForce(player.transform.forward * (100) + new Vector3(0, 200, 0));
                    beenPunched2 = true;
                }
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask3))
            {
                //if the enemy has already been punched, "delete" him. Idk how to actually remove something from here yet, but this works
                if (beenPunched3)
                {
                    //moves enemy far away below the map
                    Destroy(enemy3Object);
                }
                //else if enemy has not been punched, change beenPunched to true (enemy has two hits until death) and knock back the enemy
                else
                {
                    enemy3.AddForce(player.transform.forward * (100) + new Vector3(0, 200, 0));
                    beenPunched3 = true;
                }
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask4))
            {
                //if the enemy has already been punched, "delete" him. Idk how to actually remove something from here yet, but this works
                if (beenPunched4)
                {
                    //moves enemy far away below the map
                    Destroy(enemy4Object);

                }
                //else if enemy has not been punched, change beenPunched to true (enemy has two hits until death) and knock back the enemy
                else
                {
                    enemy4.AddForce(player.transform.forward * (100) + new Vector3(0, 200, 0));
                    beenPunched4 = true;
                }
            }
  
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask5))
            {
                //if the enemy has already been punched, "delete" him. Idk how to actually remove something from here yet, but this works
                if (beenPunched5)
                {
                    //moves enemy far away below the map
                    Destroy(enemy5Object);

                }
                //else if enemy has not been punched, change beenPunched to true (enemy has two hits until death) and knock back the enemy
                else
                {
                    enemy5.AddForce(player.transform.forward * (100) + new Vector3(0, 200, 0));
                    beenPunched5 = true;
                }
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask6))
            {
                //if the enemy has already been punched, "delete" him. Idk how to actually remove something from here yet, but this works
                if (beenPunched6)
                {
                    //moves enemy far away below the map
                    Destroy(enemy6Object);

                }
                //else if enemy has not been punched, change beenPunched to true (enemy has two hits until death) and knock back the enemy
                else
                {
                    enemy6.AddForce(player.transform.forward * (100) + new Vector3(0, 200, 0));
                    beenPunched6 = true;
                }
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask7))
            {
                 Destroy(enemy7Object);
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask8))
            {
               
                    Destroy(enemy8Object);

               
            }

            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask9))
            {

                    Destroy(enemy9Object);

               
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask10))
            {
                //if the enemy has already been punched, "delete" him. Idk how to actually remove something from here yet, but this works
                if (beenPunched10)
                {
                    //moves enemy far away below the map
                    Destroy(enemy10Object);

                }
                //else if enemy has not been punched, change beenPunched to true (enemy has two hits until death) and knock back the enemy
                else
                {
                    enemy10.AddForce(player.transform.forward * (100) + new Vector3(0, 200, 0));
                    beenPunched10 = true;
                }
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask11))
            {
                //if the enemy has already been punched, "delete" him. Idk how to actually remove something from here yet, but this works
                if (beenPunched11)
                {
                    //moves enemy far away below the map
                    Destroy(enemy11Object);

                }
                //else if enemy has not been punched, change beenPunched to true (enemy has two hits until death) and knock back the enemy
                else
                {
                    enemy11.AddForce(player.transform.forward * (100) + new Vector3(0, 200, 0));
                    beenPunched11 = true;
                }
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask12))
            {
                //if the enemy has already been punched, "delete" him. Idk how to actually remove something from here yet, but this works
                if (beenPunched12)
                {
                    //moves enemy far away below the map
                    Destroy(enemy12Object);

                }
                //else if enemy has not been punched, change beenPunched to true (enemy has two hits until death) and knock back the enemy
                else
                {
                    enemy12.AddForce(player.transform.forward * (100) + new Vector3(0, 200, 0));
                    beenPunched12 = true;
                }
            }

            //this alternates left and right punches. I 
            if (i % 2 == 0)
            {
                StartCoroutine(PunchLeftAnim());
            }
            else
            {
                StartCoroutine(PunchRightAnim());
            }
            i++;
        }
        //kick
        if (Input.GetKeyDown("space"))
        {

            //if the enemy is right in front of the player
            //player.transform.position is the position of the player
            //player.transform.forward is the direction the player is facing
            //1f is the raycast distance. this might need to be shorter
            //enemyMask is the mask defined above
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask1))
            {
                enemy1.AddForce(player.transform.forward * (500) + new Vector3(0, 750, 0));

            }

            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask2))
            {
                enemy2.AddForce(player.transform.forward * (500) + new Vector3(0, 750, 0));

            }

            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask3))
            {
                enemy3.AddForce(player.transform.forward * (500) + new Vector3(0, 750, 0));
          
            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask4))
            {
                enemy4.AddForce(player.transform.forward * (500) + new Vector3(0, 750, 0));

            }

            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask5))
            {
                enemy5.AddForce(player.transform.forward * (500) + new Vector3(0, 750, 0));

            }

            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask6))
            {
                enemy6.AddForce(player.transform.forward * (500) + new Vector3(0, 750, 0));

            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask7))
            {
                Destroy(enemy7Object);

            }

            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask8))
            {
                Destroy(enemy8Object);

            }

            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask9))
            {
                Destroy(enemy9Object);

            }
            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask10))
            {
                enemy10.AddForce(player.transform.forward * (500) + new Vector3(0, 750, 0));

            }

            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask11))
            {
                enemy11.AddForce(player.transform.forward * (500) + new Vector3(0, 750, 0));

            }

            if (Physics.Raycast(player.transform.position, player.transform.forward, 1f, enemyMask12))
            {
                enemy12.AddForce(player.transform.forward * (500) + new Vector3(0, 750, 0));

            }
            StartCoroutine(KickAnim());

        }
    }

    IEnumerator KickAnim()
    {
        kick.transform.Translate(0, 0, 1f);
        yield return new WaitForSeconds(0.1f);
        kick.transform.Translate(0, 0, -1f);
    }

    //move the block inside the player to simulate punch
    IEnumerator PunchLeftAnim()
    {
        punchLeft.transform.Translate(0, 0, 0.5f);

        yield return new WaitForSeconds(0.1f);

        punchLeft.transform.Translate(0, 0, -0.5f);

    }

    IEnumerator PunchRightAnim()
    {

        punchRight.transform.Translate(0, 0, 0.5f);

        yield return new WaitForSeconds(0.1f);

        punchRight.transform.Translate(0, 0, -0.5f);

    }
}
