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
    public GameObject introductionPanel;
    public Text introductionText;
    public Text titleText;
    public Text timerText;
    public string[] dialogue;

    private int index;
    public GameObject contButton;
    public float wordSpeed;

    private bool isTyping = false;
    private bool lineFinished = false;

    private string tutorialKey; // unique key per scene

    void Start()
    {
        // Create a unique key for this level
        tutorialKey = "TutorialCompleted_" + SceneManager.GetActiveScene().name;

        // Check if tutorial was already completed in this level
        if (PlayerPrefs.GetInt(tutorialKey, 0) == 1)
        {
            SkipTutorialCompletely();
            return;
        }

        // First time → run tutorial
        introductionPanel.SetActive(true);
        StartCoroutine(Typing());
        Time.timeScale = 0f;  // Pause game
    }

    void Update()
    {
        contButton.SetActive(lineFinished);

        if (lineFinished && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipTutorialCompletely();
        }
    }

    IEnumerator Typing()
    {
        isTyping = true;
        lineFinished = false;
        introductionText.text = "";

        foreach (char letter in dialogue[index])
        {
            introductionText.text += letter;
            yield return new WaitForSecondsRealtime(wordSpeed);
        }

        isTyping = false;
        lineFinished = true;
    }

    public void NextLine()
    {
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

        // Save completion for this specific level
        PlayerPrefs.SetInt(tutorialKey, 1);
        PlayerPrefs.Save();
    }

    private void SkipTutorialCompletely()
    {
        introductionPanel.SetActive(false);
        Time.timeScale = 1f;

        // Mark tutorial as completed for this level
        PlayerPrefs.SetInt(tutorialKey, 1);
        PlayerPrefs.Save();
    }
}

