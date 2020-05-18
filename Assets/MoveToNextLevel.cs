using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;
    public bool isDead = false;


    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead == true)
        {
            if (other.gameObject.tag == "Avatar")
            {
                if (SceneManager.GetActiveScene().buildIndex == 3) /* < Change this int value to whatever your
                                                                   last level build index is on your
                                                                   build settings */
                {
                    Debug.Log("You Completed ALL Levels");

                    //Show Win Screen or Somethin.
                }
                else
                {
                    Debug.Log("You moved to the next level");
                    //Move to next level
                    SceneManager.LoadScene(nextSceneLoad);

                    if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                    {
                        PlayerPrefs.SetInt("levelAt", nextSceneLoad);
                    }
                }
            }
        }
    }
}
