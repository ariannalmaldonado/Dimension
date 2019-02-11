using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    int j = 0;
    public Rigidbody PlatVert;

    // Start is called before the first frame update
    void Start()
    {
        PlatVert = GetComponent<Rigidbody>();

        InvokeRepeating("ChangeSpeedVert", 1f, 5f);
    }

    void ChangeSpeedVert()
    {
        Vector3 mov;
        if (j % 2 == 1)
        {
            mov = new Vector3(0f, 2f, 0f);
        }
        else
        {
            mov = new Vector3(0f, -2f, 0f);

        }
        j++;
        PlatVert.velocity = mov;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
