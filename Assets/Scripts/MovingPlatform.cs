using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Rigidbody q;
    public Rigidbody player;
    public GameObject pl;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        q = GetComponent<Rigidbody>();
        player = player.GetComponent<Rigidbody>();
        InvokeRepeating("ChangeSpeed", 1f, 5f);
    }

    void ChangeSpeed()
    {
        Vector3 mov;
        if (i % 2 == 0)
        {
            mov = new Vector3(0f, 0f, 2f);
        }
        else
        {
            mov = new Vector3(0f, 0f, -2f);

        }
        i++;
        q.velocity = mov;
    }

    // Update is called once per frame
    void Update()
    {
        //same as the one in the player controller. if player is on it, change velocity
        if (Physics.Raycast(transform.position, new Vector3(0, 1, 0), 0.5f))
        {
            player.velocity = new Vector3(0f, player.velocity.y, 0f);
            player.velocity += q.velocity;
        }


    }
}
