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

    public void LoadTargetScene()
    {
        // Check if the scene name is not empty
        if (!string.IsNullOrEmpty(sceneToLoadName))
        {
            SceneManager.LoadScene(sceneToLoadName);
        }
        else
        {
            Debug.LogWarning("Scene name to load is empty. Please assign a scene name in the Inspector.");
        }
    }

    // Example of calling the method (e.g., on a key press)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadTargetScene();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadTargetScene();
        }
    }
}

