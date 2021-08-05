using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class ResetValues : MonoBehaviour
{
    public static bool isOn=true;
    [SerializeField] private Toggle toggle;
    [SerializeField] TextMeshProUGUI score;

    private void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex > 0)
            score.text = " SCORE: " + ScoreManager.scoreValue.ToString();


        toggle.isOn = isOn;
        if (isOn == false)
            AudioManager.instance.StopPlaying("MainMenuMusic");
        else
            AudioManager.instance.Play("MainMenuMusic");

        AudioManager.instance.StopPlaying("GameMusic");

    }

    void Update()
    {
        ScoreManager.scoreValue = 0;
        if (IsHardMode.hardMode == true)
        { 
            Countdown.timeStart = 180;
            
        }
        else if (IsHardMode.hardMode == false)
        {
            Countdown.timeStart = 60; 
        }
    }

    public void Mute(bool toggled)
    {
        isOn = toggled;
        AudioListener.pause = !toggled;
        Debug.Log(AudioListener.pause);
        if (isOn == false)
            AudioManager.instance.StopPlaying("MainMenuMusic");
        else
            AudioManager.instance.Play("MainMenuMusic");
    }
}
