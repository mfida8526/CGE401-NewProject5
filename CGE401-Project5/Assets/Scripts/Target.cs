using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public GameObject infectedAnimal;
    public GameObject curedAnimal;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            CureAnimal();
        }
    }

    void CureAnimal()
    {
        infectedAnimal.SetActive(false);
        curedAnimal.SetActive(true);
        GameManager.Instance.AdjustDifficulty(0.05f);
    }

    /*    void Die()
        {
            Destroy(gameObject);
        }
    */
}
