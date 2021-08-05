using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Toggle toggle;
    public void PlayGame()
    {
        DontDestroyOnLoad(toggle);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.instance.StopPlaying("MainMenuMusic");
        AudioManager.instance.Play("GameMusic");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
