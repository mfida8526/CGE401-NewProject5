using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UninfectedAnimal : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 0.5f;           // Walking speed
    public float rotationSpeed = 1f;         // How fast the animal turns
    public float wanderRadius = 3f;          // Max distance from spawn point
    public float changeDirectionTime = 3f;   // Time between picking new directions

    [Header("Pause Settings")]
    public float minPauseTime = 1f;          // Minimum idle duration
    public float maxPauseTime = 3f;          // Maximum idle duration
    [Range(0f, 1f)]
    public float pauseChance = 0.4f;         // Chance to pause when changing direction

    private Rigidbody rb;
    private Vector3 spawnPosition;
    private Vector3 targetDirection;
    private float timer = 0f;
    private bool isPaused = false;
    private float pauseTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;   // Prevent physics from interfering
        spawnPosition = transform.position;
        PickNewDirection();
    }

    void Update()
    {
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0f)
            {
                isPaused = false;
                PickNewDirection();
            }
            return;
        }

        timer += Time.deltaTime;

        if (timer >= changeDirectionTime)
        {
            timer = 0f;
            if (Random.value < pauseChance)
            {
                isPaused = true;
                pauseTimer = Random.Range(minPauseTime, maxPauseTime);
                return;
            }
            else
            {
                PickNewDirection();
            }
        }

        // Smooth rotation
        Vector3 direction = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void FixedUpdate()
    {
        // Move forward slowly using Rigidbody
        if (!isPaused)
        {
            rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void PickNewDirection()
    {
        // Pick a random point within a circle around the spawn point
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        Vector3 randomTarget = spawnPosition + new Vector3(randomCircle.x, 0, randomCircle.y);

        // Compute the direction to face
        targetDirection = (randomTarget - transform.position).normalized;
    }
}
