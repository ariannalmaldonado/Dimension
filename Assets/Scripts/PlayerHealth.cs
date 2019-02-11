using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
     
        currentHealth = numOfHearts;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X)) {
            DealDamage(1);

        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            pickedUpHeart();

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("OUCH");
            DealDamage(1);
        }
        if (collision.gameObject.tag == "Heart")
        {
            Destroy(collision.gameObject);
            pickedUpHeart();
        }
        if (collision.gameObject.tag == "Respawn")
        {
            playerDeath();
        }
        if (collision.gameObject.tag == "Lava")
        {
            playerDeath();
        }

    }

    void DealDamage(int damageValue)
    {
        currentHealth -= damageValue;

        bool changed = false;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i].sprite == fullHeart && !changed)
            {
                hearts[i].sprite = emptyHeart;
                changed = true;
            }
        }

        if (currentHealth <= 0)
        {
            playerDeath();
        }
    }

    void pickedUpHeart()
    {
        if(currentHealth == 3)
        {
            return;
        }

        bool changed = false;
        for (int i = hearts.Length -1 ; i >= 0; i--)
        {
            if (hearts[i].sprite == emptyHeart && !changed)
            {
                hearts[i].sprite = fullHeart;
                changed = true;
            }
        }

        currentHealth += 1;
    }

    void playerDeath()
    {

        Debug.Log("YOU DIED.");
        transform.position = new Vector3(-45.6f, 14.36f, -52.89f);
        currentHealth = 3;
        for (int i = hearts.Length - 1; i >= 0; i--)
        {
            if (hearts[i].sprite == emptyHeart)
            {
                hearts[i].sprite = fullHeart;
            }
        }
    }
}
