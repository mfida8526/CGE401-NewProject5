using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedAnimal : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;        // Assigned when spawned
    public float speed = 2f;        // Walking speed
    public float detectionRange = 15f; // Distance at which enemy starts chasing
    public float attackRange = 2f;     // Distance at which enemy stops moving toward player

    [Header("Damage")]
    public float hitCooldown = 1f;
    private float lastHitTime = -999f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true; // Ensure physics doesn’t affect movement
    }

    private void Update()
    {
        if (player == null) return;

        // Distance to player
        Vector3 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;

        // Only move if player is within detection range
        if (distance <= detectionRange)
        {
            // Move toward player if outside attack range
            if (distance > attackRange)
            {
                Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);

                // Smooth movement using MoveTowards
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

                // Rotate to face player horizontally
                Vector3 lookDir = (targetPos - transform.position).normalized;
                if (lookDir != Vector3.zero)
                {
                    Quaternion lookRot = Quaternion.LookRotation(lookDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 5f * Time.deltaTime);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null && Time.time - lastHitTime > hitCooldown)
            {
                lastHitTime = Time.time;
                playerHealth.TakeDamage();
                Debug.Log("Enemy hit player!");
            }
        }
    }
}
