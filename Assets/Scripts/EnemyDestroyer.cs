using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    public GameObject enemy;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            Object.Destroy(enemy);
        }
        if (collision.gameObject.tag == "Lava")
        {
            Object.Destroy(enemy);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
