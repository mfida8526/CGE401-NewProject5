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
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;

    void Start()
    {
        introductionPanel.SetActive(true);
        StartCoroutine(Typing());  // Start the typing animation
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (introductionText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        introductionText.text = "";
        index = 0;
        introductionPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            introductionText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {

        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            introductionText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
            introductionPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

