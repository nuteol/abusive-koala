using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentence;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject textPanel;
    public GameObject background;

    private void Start()
    {
        continueButton.SetActive(false);
        textPanel.SetActive(false);
        background.SetActive(false);
    }

    private void Update()
    {

    }

    public void BeginDialogue()
    {
        textPanel.SetActive(true);
        background.SetActive(true);
        index = 0;
        textDisplay.text = "";
        StartCoroutine(Type());
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentence.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            background.SetActive(false);
            //Dialogue finihed
        }
    }

    public IEnumerator Type()
    {
        //Start talking animation
        foreach (char letter in sentence[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        //End talking animation
        continueButton.SetActive(true);
        if(textPanel.active == false)
        {
            continueButton.SetActive(false);
        }
    }

    public void QuitDialogue()
    {
        continueButton.SetActive(false);
        textPanel.SetActive(false);
        background.SetActive(false);
    }
}
