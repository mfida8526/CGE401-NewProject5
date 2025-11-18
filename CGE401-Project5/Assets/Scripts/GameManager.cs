using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pattern for easy access
    public float gameTimer = 300f; // Initial game time (e.g., 5 minutes)
    public Text timerText; // UI text to display the timer
    public float friendlyHitPenalty = 15f; // Time penalty for hitting a civilian
    
    // Difficulty parameters (can be adjusted dynamically)
    public float zombieSpawnRate = 2f; // Time between zombie spawns
    public float zombieSpeed = 3f;
    public int zombieDamage = 10;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0)
        {
            gameTimer = 0;
            // Handle Game Over logic here
            Debug.Log("Game Over!");
        }
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        // Format the timer display (minutes:seconds)
        int minutes = Mathf.FloorToInt(gameTimer / 60F);
        int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void HitFriendly()
    {
        gameTimer -= friendlyHitPenalty;
        Debug.Log("Friendly hit! " + friendlyHitPenalty + " seconds deducted.");
        // Implement further penalties or difficulty changes here
        AdjustDifficulty(-0.1f); // Example: slightly reduce difficulty/increase penalty
    }

    public void AdjustDifficulty(float adjustment)
    {
        // Example logic to dynamically adjust spawn rate or enemy stats
        zombieSpawnRate = Mathf.Clamp(zombieSpawnRate + adjustment, 0.5f, 5f); // Keep spawn rate within a reasonable range
        // Other stats can be adjusted similarly (speed, health, etc.)
    }
}

