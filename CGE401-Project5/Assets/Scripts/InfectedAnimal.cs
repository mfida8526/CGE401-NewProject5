using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    * Maile Fidale
    * Project 5
    * Infected animal movement and player damage
*/

public class InfectedAnimal : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;            // Assigned when spawned
    public float detectionRange = 15f;  // Distance at which enemy starts chasing
    public float attackRange = 2f;      // Distance at which enemy stops and attacks

    [Header("Damage")]
    public float hitCooldown = 1f;
    private float lastHitTime = -999f;

    private NavMeshAgent agent;
    private Vector3 lastTargetPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent missing on InfectedAnimal!");
        }

        // Keep rotation controlled by script, not NavMeshAgent's auto-rotation
        agent.updateRotation = false;

        // Ensure Rigidbody is optional & kinematic (not used for movement)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false; // NavMesh handles ground snapping
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Too far? Stop chasing.
        if (distance > detectionRange)
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }
            return;
        }

        // In attack range
        if (distance <= attackRange)
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }

            // Face player
            Vector3 lookDir = (player.position - transform.position);
            lookDir.y = 0;
            if (lookDir.sqrMagnitude > 0.01f)
            {
                Quaternion lookRot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 8f * Time.deltaTime);
            }

            return;
        }

        // Chase behavior
        if (agent.isStopped)
            agent.isStopped = false;

        // Only update destination when needed
        if ((player.position - lastTargetPos).sqrMagnitude > 0.5f)
        {
            agent.SetDestination(player.position);
            lastTargetPos = player.position;
        }

        // Rotate toward movement
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion lookRot = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 5f * Time.deltaTime);
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
