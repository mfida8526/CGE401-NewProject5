using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private bool isTyping = false;   // Prevents skipping while typing
    private bool lineFinished = false;

    void Start()
    {
        introductionPanel.SetActive(true);
        StartCoroutine(Typing());  // Start the typing animation
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        // Show continue button when finished
        contButton.SetActive(lineFinished);

        // Player presses space to continue
        if (lineFinished && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
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
            introductionPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

