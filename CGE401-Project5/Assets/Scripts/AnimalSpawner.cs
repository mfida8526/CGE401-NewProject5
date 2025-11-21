using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public float initialSpawnRate = 2f; // Time in seconds between spawns
    public float minSpawnRate = 0.5f; // Minimum spawn rate limit
    public float maxSpawnRate = 5f; // Maximum spawn rate limit
    private float currentSpawnRate;
    private GameManager gameManager;

    private float leftBound = -5;
    private float rightBound = 5;
    private float spawnPosZ = 20;

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

    void SpawnRandomAnimal()
    {
        // Instantiate a random animal from the list
        int prefabIndex = Random.Range(0, animalPrefabs.Length);

        Vector3 spawnPos = new Vector3(Random.Range(leftBound, rightBound), 0, spawnPosZ);

        Instantiate(animalPrefabs[prefabIndex], spawnPos, animalPrefabs[prefabIndex].transform.rotation);
    }

    public void AdjustSpawnRate(float amount)
    {
        currentSpawnRate = Mathf.Clamp(currentSpawnRate + amount, minSpawnRate, maxSpawnRate);
    }
}

