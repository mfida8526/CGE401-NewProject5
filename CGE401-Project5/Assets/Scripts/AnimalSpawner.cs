using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public float initialSpawnRate = 2f; // Time in seconds between spawns
    public float minSpawnRate = 0.5f; // Minimum spawn rate limit
    public float maxSpawnRate = 5f; // Maximum spawn rate limit
    public float currentSpawnRate;
    private GameManager gameManager;

    private float leftBound = -5;
    private float rightBound = 5;
    private float spawnPosZ = 20;
    public HealthSystem healthSystem;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnRoutine());
        healthSystem = GameObject.FindGameObjectWithTag("HealthSystem").GetComponent<HealthSystem>();
    }

    IEnumerator SpawnRoutine()
    {
        while (!healthSystem.gameOver)
        {
            SpawnRandomAnimal();

            float randomDelay = Random.Range(1.5f, 2.0f);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    void SpawnRandomAnimal()
    {
        int prefabIndex = Random.Range(0, animalPrefabs.Length);

        // Start a bit above the expected surface
        Vector3 spawnPos = new Vector3(
            Random.Range(leftBound, rightBound),
            10f,
            spawnPosZ
        );

        // Sample the NavMesh to find a valid spawn position
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPos, out hit, 5f, NavMesh.AllAreas))
        {
            Instantiate(animalPrefabs[prefabIndex], hit.position, animalPrefabs[prefabIndex].transform.rotation);
        }
        else
        {
            Debug.LogWarning("No valid NavMesh position found for spawning animal.");
        }
    }

    public void AdjustSpawnRate(float amount)
    {
        currentSpawnRate = Mathf.Clamp(currentSpawnRate + amount, minSpawnRate, maxSpawnRate);
    }
}

