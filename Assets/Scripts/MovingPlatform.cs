using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.collider.transform.SetParent(null);
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(-2f, 16f, Mathf.PingPong(Time.time, 3));
    }
}
