using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Mimi Davis
* Target
* Project5
* Target health for animals being spawned and adds to progress bar
*/
public class Target : MonoBehaviour
{
    public float health = 50f;
    public bool isInfected;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f && isInfected)
        {
            Die();
            gameManager.HitInfectedAnimal();
        }
        else if (health <= 0f && !isInfected)
        {
            Die();
            gameManager.HitNormalAnimal();
        }
    }

    void Die()
        {
            Destroy(gameObject);
        }
}
