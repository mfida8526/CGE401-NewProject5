using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required for UI Text

public class Spawner : MonoBehaviour
{
    public GameObject[] animalPrefabs; // Array of animal prefabs (friendly and infected)
    public float initialSpawnDelay = 2f; // Initial time between spawns
    public float minSpawnDelay = 0.5f; // Minimum possible spawn delay
    public float maxSpawnDelay = 5f; // Maximum spawn time
    public float spawnRateIncreaseAmount = 0.1f; // Amount to decrease the delay by
    public float gameTimerDuration = 60f; // Total game time in seconds
    public Text timerText; // Optional: UI Text element to display the timer

    private float currentSpawnDelay;
    private float gameTimer;
    public HealthSystem healthSystem;

    private float leftBound = -5;
    private float rightBound = 5;
    private float spawnPosZ = 20;
    void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        gameTimer = gameTimerDuration;
        StartCoroutine(SpawnAnimalsRoutine());
    }

    void Update()
    {
        // Update the game timer
        gameTimer -= Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(gameTimer).ToString();
        }

        if (gameTimer <= 0)
        {
            // Game over logic
            Debug.Log("Game Over!");
            StopCoroutine(SpawnAnimalsRoutine());
            // Add game over scene load or other logic here
        }
    }

    IEnumerator SpawnAnimalsRoutine()
    {
        while (gameTimer > 0 && !healthSystem.gameOver)
        {
            yield return new WaitForSeconds(currentSpawnDelay);
            float randomDelay = Random.Range(1.5f, 2.0f);
            SpawnAnimal();
        }
    }

    void SpawnAnimal()
    {
        // Instantiate a random animal from the list
       int prefabIndex = Random.Range(0, animalPrefabs.Length); 
            
            Vector3 spawnPos = new Vector3(Random.Range(leftBound, rightBound), 0, spawnPosZ);

            Instantiate(animalPrefabs[prefabIndex], spawnPos, animalPrefabs[prefabIndex].transform.rotation);
    }

    // Public method to be called by the PlayerShooting script
    public void IncreaseSpawnRate()
    {
        // Decrease the delay (increase the rate), ensuring it doesn't go below the minimum
        currentSpawnDelay = Mathf.Max(minSpawnDelay, currentSpawnDelay - spawnRateIncreaseAmount);
        Debug.Log("Spawn rate increased! New delay: " + currentSpawnDelay);
    }
    public void DecreaseSpawnRate()
    {
        currentSpawnDelay = Mathf.Min(maxSpawnDelay, currentSpawnDelay + spawnRateIncreaseAmount);
        Debug.Log("Spawn rate decreased! New time between spawns: " + currentSpawnDelay);
    }

    // Public method to be called by the PlayerShooting script
    public void DecreaseGameTimer(float amount)
    {
        gameTimer -= amount;
        Debug.Log("Friendly hit! Time decreased by: " + amount + "s. Remaining time: " + gameTimer);
    }
}



