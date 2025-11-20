using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    // UI References (assign in Inspector)
    public Text timerText;
    public Slider progressBar; // Progress bar for 10 units
    public float gameTimer = 140f; // Example total game time
    private float timeRemaining;
    private const float TIME_PENALTY_SECONDS = 30f;

    // Game State Variables
    private int score = 0; // Or whatever "progress" means
    private int requiredProgress = 10;
    public float InfectedSpawnChance = 0.2f; // Base chance

    // References to other managers
    private AnimalSpawner animalSpawner;

    void Start()
    {
        animalSpawner = FindObjectOfType<AnimalSpawner>();
        timeRemaining = gameTimer;
        progressBar.maxValue = requiredProgress;
        progressBar.value = 0;
    }

    void Update()
    {
        // Handle Timer

        if (timeRemaining > 0)
        {
            gameTimer -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            timeRemaining = 0;
            GameOver();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(gameTimer / 60);
        int seconds = Mathf.FloorToInt(gameTimer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateProgressUI()
    {
        progressBar.value = score;
        if (score >= requiredProgress)
        {
            GameWon();
        }
    }

    public void HitInfectedAnimal()
    {
        // Increase spawn rate (decrease time between spawns, e.g., by -0.1f)
        animalSpawner.AdjustSpawnRate(-0.1f);
        score++;
        UpdateProgressUI();
        Debug.Log("Hit infected! Score: " + score + ", Spawn Rate adjusted.");
    }

    public void HitNormalAnimal()
    {
        // Decrease timer (e.g., lose 5 seconds)
        gameTimer -= TIME_PENALTY_SECONDS;
        // Decrease spawn rate slightly (increase time between spawns, e.g., by +0.05f)
        animalSpawner.AdjustSpawnRate(0.05f);
        Debug.Log("Hit normal! Time penalty, Spawn Rate adjusted.");

    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        // Implement game over logic here (stop spawning, show UI, etc.)
        Time.timeScale = 0; // Stop the game
    }

    void GameWon()
    {
        Debug.Log("You Won!");
        // Implement win logic here
        Time.timeScale = 0; // Stop the game
    }
}
