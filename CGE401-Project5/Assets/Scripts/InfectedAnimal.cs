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
    public Transform player;
    public float attackRange = 2f;

    [Header("Damage")]
    public float hitCooldown = 1f;
    private float lastHitTime = -999f;

    [Header("Chase Behavior")]
    public float targetUpdateDistance = 0.5f;  // Distance before updating player target
    public float refreshDelay = 0.2f;          // Delay to give player dodge time

    private NavMeshAgent agent;
    private Vector3 lastKnownPlayerPos;
    private bool updatingTarget = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent missing on InfectedAnimal!");
        }

        // Rotate manually
        agent.updateRotation = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    private void Start()
    {
        if (player != null)
        {
            lastKnownPlayerPos = player.position;
            agent.SetDestination(lastKnownPlayerPos);
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // --- ATTACK BEHAVIOR ---
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

        // If target is out of attack range, resume moving
        if (agent.isStopped)
            agent.isStopped = false;

        // --- ALWAYS CHASE / DELAYED TRACKING ---
        if (!updatingTarget && !agent.pathPending && agent.remainingDistance <= targetUpdateDistance)
        {
            StartCoroutine(UpdatePlayerDestination());
        }

        // Rotate based on movement
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion lookRot = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 5f * Time.deltaTime);
        }
    }

    private System.Collections.IEnumerator UpdatePlayerDestination()
    {
        updatingTarget = true;

        // Small delay gives player space to dodge
        yield return new WaitForSeconds(refreshDelay);

        // Update to player’s new position
        lastKnownPlayerPos = player.position;
        agent.SetDestination(lastKnownPlayerPos);

        updatingTarget = false;
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

            Destroy(gameObject);
        }
    }
}
