using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
* Maile Fidale
* Project 5
* introductory text for game, sets the narrative
*/

public class IntroductionText : MonoBehaviour
{
    public GameObject introductionPanel;  // The tutorial panel
    public Text introductionText;          // The text component for dialogue
    public string[] dialogue;              // Lines of tutorial text
    public float wordSpeed = 0.05f;       // Speed of typing effect

    private int index = 0;
    private bool lineFinished = false;

    // Static set to track which scenes already showed the tutorial
    private static HashSet<string> completedScenes = new HashSet<string>();

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (completedScenes.Contains(sceneName))
        {
            // Tutorial already shown for this scene → skip
            introductionPanel.SetActive(false);
            return;
        }

        // Mark as shown
        completedScenes.Add(sceneName);

        // Show tutorial
        introductionPanel.SetActive(true);
        StartCoroutine(Typing());
        Time.timeScale = 0f; // Pause game
    }

    void Update()
    {
        // Advance to next line when Space is pressed
        if (lineFinished && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }

        // Skip tutorial entirely with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndTutorial();
        }
    }

    private IEnumerator Typing()
    {
        lineFinished = false;
        introductionText.text = "";

        foreach (char letter in dialogue[index])
        {
            introductionText.text += letter;
            yield return new WaitForSecondsRealtime(wordSpeed);
        }

        lineFinished = true;
    }

    private void NextLine()
    {
        if (!lineFinished) return;

        if (index < dialogue.Length - 1)
        {
            index++;
            StartCoroutine(Typing());
        }
        else
        {
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        introductionPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}

