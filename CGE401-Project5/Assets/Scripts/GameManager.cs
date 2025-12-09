using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using TMPro;
/*
* Mimi Davis
* GameManager
* Project5
* Handles the timer, spawn rate, and progress bar
*/
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public TextMeshProUGUI timerText;
    public Slider progressBar; 
    public float gameTimer = 180f; 
    private const float TIME_PENALTY_SECONDS = 30f;
    public GameObject losePanel;
    public GameObject winPanel;


    
    private int score = 0; 
    private int requiredProgress = 10;


    
    private AnimalSpawner animalSpawner;

    void Start()
    {
        animalSpawner = FindObjectOfType<AnimalSpawner>();

        progressBar.maxValue = requiredProgress;
        progressBar.value = 0;
    }

    void Update()
    {
        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer < 0) gameTimer = 0;
            UpdateTimerUI();
        }
        else
        {
            GameOver();
        }

        if (winPanel != null)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
               SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }


        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(gameTimer / 60);
        int seconds = Mathf.FloorToInt(gameTimer % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
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
        StartCoroutine(FlashTimerColor(Color.red, 0.15f));

        // Decrease timer (e.g., lose 15 seconds)
        gameTimer -= TIME_PENALTY_SECONDS;
        // Decrease spawn rate slightly (increase time between spawns, e.g., by +0.05f)
        animalSpawner.AdjustSpawnRate(0.05f);
        Debug.Log("Hit normal! Time penalty, Spawn Rate adjusted.");

    }

    private IEnumerator FlashTimerColor(Color flashColor, float duration)
    {
        Color originalColor = timerText.color;   // timerText is your TMP Text
        timerText.color = flashColor;

        yield return new WaitForSeconds(duration);

        timerText.color = originalColor;
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        
        losePanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameWon()
    {
        Debug.Log("You Won!");
        Time.timeScale = 0f;
        winPanel.gameObject.SetActive(true);
    }

}
