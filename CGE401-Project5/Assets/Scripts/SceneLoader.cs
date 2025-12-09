using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
* Mimi Davis
* SceneLoader
* Project5
* Loads the player into the next wave
*/
public class SceneLoader : MonoBehaviour
{
    public string sceneToLoadName; // Assign this in the Inspector
    public GameObject winPanel;

    public void LoadTargetScene()
    {
        // Check if the scene name is not empty
        if (!string.IsNullOrEmpty(sceneToLoadName) && winPanel != null)
        {
            SceneManager.LoadScene(sceneToLoadName);
        }
        else
        {
            Debug.LogWarning("Scene name to load is empty. Please assign a scene name in the Inspector.");
        }
    }

    public void RestartCurrentScene()
    {
        Time.timeScale = 1f; // unpause

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int lastIndex = SceneManager.sceneCountInBuildSettings - 1;

        // If we're on the last scene, restart from first scene
        if (currentIndex == lastIndex)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            // Otherwise restart the same scene
            SceneManager.LoadScene(currentIndex);
        }
    }

    // Load the next scene automatically
    public void LoadNextScene()
    {
        Time.timeScale = 1f; // unpause
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        // If last scene → wrap back to first scene
        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 0;
        }

        SceneManager.LoadScene(nextIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && winPanel != null)
        {
            LoadNextScene();   
        }

        if (Input.GetKeyDown(KeyCode.C) && winPanel != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
        }
    }

    // Example of calling the method (e.g., on a key press)
    /* void Update()
     {
         if (Input.GetKeyDown(KeyCode.C))
         {
             LoadTargetScene();
         }

         if (Input.GetKeyDown(KeyCode.R))
         {
             LoadTargetScene();
         }
     }*/
}

