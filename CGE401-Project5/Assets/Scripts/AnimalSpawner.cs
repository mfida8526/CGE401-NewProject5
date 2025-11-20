using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] normalAnimalPrefabs;
    public GameObject[] infectedAnimalPrefabs;
    public float initialSpawnRate = 2f; // Time in seconds between spawns
    public float minSpawnRate = 0.5f; // Minimum spawn rate limit
    public float maxSpawnRate = 5f; // Maximum spawn rate limit
    private float currentSpawnRate;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnRate);
            SpawnRandomAnimal();
        }
    }

    IEnumerator SpawnRandomAnimal()
    {
        // Simple random position within bounds (adjust as needed)
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

        // Randomly decide between normal and infected animal
        bool spawnInfected = Random.Range(0f, 1f) < gameManager.InfectedSpawnChance;
        GameObject animalPrefab;

        if (spawnInfected && infectedAnimalPrefabs.Length > 0)
        {
            animalPrefab = infectedAnimalPrefabs[Random.Range(0, infectedAnimalPrefabs.Length)];
        }
        else if (normalAnimalPrefabs.Length > 0)
        {
            animalPrefab = normalAnimalPrefabs[Random.Range(0, normalAnimalPrefabs.Length)];
        }
        else
        {
            Debug.LogError("No animal prefabs assigned!");
            yield break;
        }

        Instantiate(animalPrefab, spawnPosition, Quaternion.identity);
    }

    public void AdjustSpawnRate(float amount)
    {
        currentSpawnRate = Mathf.Clamp(currentSpawnRate + amount, minSpawnRate, maxSpawnRate);
    }
}

