using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        text.text = PlayerPrefs.GetInt("Coins").ToString();
        score = PlayerPrefs.GetInt("Coins");
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = score.ToString();
        PlayerPrefs.SetInt("Coins", score);
        //text.text = PlayerPrefsManager.coins.ToString();
    }
    public void ChangeHp(int healthValue)
    {
        playercontroller.addHp(healthValue);
    }

}
