﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;
    public bool isDead = false;
    public Image EndScreen;
    public TextMeshProUGUI endText;
    private bool triggered = false;


    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        EndScreen.enabled = false;
        endText.enabled = false;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {

        
        if (isDead == true)
        {
            if (other.gameObject.tag == "Avatar")
            {
                if (SceneManager.GetActiveScene().buildIndex == 3 && !triggered) 
                {
                    triggered = true;
                    Debug.Log("You Completed ALL Levels");
                    StartCoroutine(ExampleCoroutine());
                    
                    //Show Win Screen or Somethin.
                }
                else
                {
                    Debug.Log("You moved to the next level");
                    //Move to next level
                    SceneManager.LoadScene(nextSceneLoad);
                    if(PlayerPrefs.GetInt("Coins") >= 24)
                    {
                        PlayerPrefs.SetInt("Coins", 24);
                    }

                    if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                    {
                        PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                    }
                }
            }
        }
    }
    IEnumerator ExampleCoroutine()
    {
        SoundManager.PlaySound("Roundabout");
        EndScreen.enabled = true;
        endText.enabled = true;
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}
