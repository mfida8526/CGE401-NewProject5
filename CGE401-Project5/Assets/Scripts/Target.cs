using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
