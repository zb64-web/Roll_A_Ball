using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;
    public Text TimerTxt;
    public Text overText;

    void Start()
    {
        TimerOn = true;
        SetTimerText(); 
        overText.text = "";
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
                overText.text = "Game Over";
            }
        }
     
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void SetTimerText() // Fixed the method name
    {
        TimerTxt.text = "00:00";
    }

}


