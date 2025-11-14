using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public HealthSystem healthSystem;
    public float hitCooldown = 1f;
    private float lastHitTime = -999f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            if (Time.time - lastHitTime > hitCooldown)
            {
                lastHitTime = Time.time;
                healthSystem.TakeDamage();
                Debug.Log("Player hit by enemy!");
            }
        }
    }
}
