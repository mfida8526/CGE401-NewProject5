using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required for UI Text

public class Spawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // List of all animal prefabs
    public Transform[] spawnPoints; // Array of potential spawn locations

    public float initialSpawnTime = 2f;
    public float minSpawnTime = 0.5f; // Cap the minimum spawn time
    public float maxSpawnTime = 5f; // Cap the maximum spawn time
    public Text timerText; // UI Text to display remaining time

    private float timeBetweenSpawns;
    private float gameTimer = 60f; // Total game time in seconds
    public ShootWithRaycasts shootWithRaycasts;

    void Start()
    {
        timeBetweenSpawns = initialSpawnTime;
        StartCoroutine(SpawnRoutine());
    }

    public void Update()
    {
        // Decrease game timer

        gameTimer -= Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.FloorToInt(gameTimer).ToString();
        }

        if (gameTimer <= 0)
        {
            // Handle game over condition (stop spawning, etc.)
            StopCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (gameTimer > 0) // Only spawn while game is active
        {
            // Wait for the current spawn time
            yield return new WaitForSeconds(timeBetweenSpawns);

            // Spawn an animal
            SpawnAnimal();
        }
    }

    void SpawnAnimal()
    {
        // Choose a random prefab and a random spawn point
        int prefabIndex = Random.Range(0, animalPrefabs.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(animalPrefabs[prefabIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    // Public method to be called by the PlayerShooting script
    public void AdjustSpawnRate(float adjustment)
    {
        timeBetweenSpawns += adjustment;
        // Clamp the spawn time between min and max values
        timeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns, minSpawnTime, maxSpawnTime);
    }
    public void DeductTime(float penalty)
    {
        gameTimer -= penalty;
        if (gameTimer <= 0)
        {
            gameTimer = 0;
            // Additional game over logic can be triggered here too
 
        }
    }
}


