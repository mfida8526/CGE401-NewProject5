using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthSystem healthSystem;

    public float hitCooldown = 1f;
    private float lastHitTime = -999f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the HealthSystem component from the player
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();

            if (playerHealth != null && Time.time - lastHitTime > hitCooldown)
            {
                lastHitTime = Time.time; // reset hit timer
                playerHealth.TakeDamage(); // call damage method
            }
        }
    }
}
