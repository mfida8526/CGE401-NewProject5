using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
* Mimi Davis
*AnimalSpawner
* Project5
* Spawns random amount of animals 
*/

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public float initialSpawnRate = 2f; // Time in seconds between spawns
    public float minSpawnRate = 0.5f; // Minimum spawn rate limit
    public float maxSpawnRate = 5f; // Maximum spawn rate limit
    public float currentSpawnRate;
    private GameManager gameManager;

    public float leftBound = -5;
    public float rightBound = 5;
    public float spawnPosZ = 20;
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

        Vector3 spawnPos = new Vector3(
            Random.Range(leftBound, rightBound),
            0.5f, // make sure it’s near the NavMesh
            spawnPosZ
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPos, out hit, 5f, NavMesh.AllAreas))
        {
            // Instantiate and store a reference to the new animal
            GameObject newAnimal = Instantiate(
                animalPrefabs[prefabIndex],
                hit.position,
                animalPrefabs[prefabIndex].transform.rotation
            );

            // <-- NEW CODE: assign player reference if it has InfectedAnimal script
            InfectedAnimal ia = newAnimal.GetComponent<InfectedAnimal>();
            if (ia != null)
            {
                ia.player = GameObject.FindGameObjectWithTag("Player")?.transform;
            }
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

