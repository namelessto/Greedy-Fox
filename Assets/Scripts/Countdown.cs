using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Countdown : MonoBehaviour
{
    [SerializeField] public static float timeStart;
    [SerializeField] TextMeshProUGUI Counter;

    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject pauseButton;

    // Use this for initialization
    private void Awake()
    {
        if (!IsHardMode.hardMode)
            timeStart = 60;
    }
    void Start()
    {
        Counter.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        Counter.text = Mathf.Round(timeStart).ToString();

        if (timeStart <= 0)
        {
            loseMenu.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0f;
            PlayerControl.health = 3;
        }

    }
}
