using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject pauseButton;

    [SerializeField] int gemCount;
    int scoreCounter;
    public static int scoreValue;
    bool hasWon = false;

    void Start()
    {
        score.text = " Score: " + scoreValue.ToString();
        if (instance == null)
            instance = this;
        winMenu.SetActive(false);
        scoreCounter = 0;
    }


    public void ChangeScore(int value)
    {

        scoreValue += value;
        score.text = " Score: " + scoreValue.ToString();
        if (PlayerControl.enemyHurt == false)
            scoreCounter++;
    }

    void Update()
    {
        if (PlayerControl.enemyHurt)
        {
            ScoreManager.scoreValue += 1000;
            score.text = " Score: " + scoreValue.ToString();
        }

        if (scoreCounter == gemCount && hasWon == false)
        {
            for (float i = 0; i < Countdown.timeStart; i++)
            {
                ScoreManager.scoreValue += 50;
                score.text = " Score: " + scoreValue.ToString();
            }
            hasWon = true;
            winMenu.SetActive(true);
            pauseButton.SetActive(false);
            //Time.timeScale = 0.8f;
            Invoke("Wait", 0.29f);

            //Debug.Log("WON");
        }
    }

    void Wait()
    {
        Time.timeScale = 0f;
    }
    void OnWinScoreTimer()
    {
        for (int i = 0; i < 1000; i++)
        {
            if (i % 100 == 0)
            {
                ScoreManager.scoreValue += 50;
                score.text = " Score: " + scoreValue.ToString();
            }
        }
    }
}
