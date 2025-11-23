using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

