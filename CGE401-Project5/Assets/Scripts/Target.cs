using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isInfected = false;
    public float health = 50f;

    public GameObject infectedAnimal;
    public GameObject curedAnimal;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0 && isInfected)
        {
            CureAnimal();
            gameManager.HitInfectedAnimal();
        }
         
    }

    void CureAnimal()
    {
        infectedAnimal.SetActive(false);
        curedAnimal.SetActive(true);
    }

    /*    void Die()
        {
            Destroy(gameObject);
        }
    */
}
