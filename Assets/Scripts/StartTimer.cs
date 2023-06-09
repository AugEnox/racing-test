using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    public float initialTime;
    float clockedTime;
    private int lap;
    public Text timerText;
    public Text lapText;
    public Text bestlapText;
    public Text lastlapText;
    bool timerStarted;

    public List<float> savedTime = new List<float>();

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "FinishDetect" && Checkpoints.validLap)
        {
            StartTime(true);

            UpdateTime();
            UpdateLap();
            initialTime = Time.time;
        }
    }

    void UpdateTime()
    {
        if(lap>0)
        {
            lastlapText.text = ConvertFloat2TimeString(clockedTime);
            savedTime.Add(clockedTime);
            float bestLap = savedTime.Min();
            bestlapText.text = ConvertFloat2TimeString(bestLap);
        }
    }

    void UpdateLap()
    {
        lap++;
        lapText.text = lap.ToString();
    }

    bool StartTime(bool state)
    {
        timerStarted = state;
        return state;
    }

    string ConvertFloat2TimeString(float time)
    {
        int clockedmilisec = (int)(time % 1 * 100);
        //if (clockedmilisec == 100) clockedmilisec = 0;
        int clockedsec = (int)time % 60;
        int clockedmin = (int)time / 60;
        string convertedTime = FormatNum(clockedmin) + ":" + FormatNum(clockedsec) + ":" + FormatNum(clockedmilisec);
        return convertedTime;
    }

    string FormatNum(int num)
    {
        string formatted = num.ToString("D2");
        return formatted;
    }

    void UpdateTimer()
    {
        if (timerStarted == true)
        {
            clockedTime = Time.time - initialTime;
            //double clockedsec = Math.Round(clockedTime, 2) % 60;
            //int clockedmin = (int)clockedTime / 60;
            //timerText.text = clockedmin + " : " + clockedsec;
            timerText.text = ConvertFloat2TimeString(clockedTime);
        }
    }

    void Start()
    {
        clockedTime = 0;
        StartTime(false);
        lap = 0;
        
    }

    void Update()
    {
        
        UpdateTimer();
    }
}
