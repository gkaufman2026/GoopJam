using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float timer;
    bool isActive;
    Level levelData;
    
    void Start()
    {
        TimeEvents.timerNotifactions += HandleTimerNotif;
        LevelManager.Instance.StartLevelEvent.AddListener(HandelLevelStart);
        LevelManager.Instance.CompleteLevelEvent.AddListener(HandelLevelComplete);
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive) 
        {
            timer += Time.deltaTime;
        }
    }
    void HandelLevelComplete(Level level)
    {
        TimeEvents.timerNotifactions?.Invoke(TimeEvents.TimerStatus.STOP);
    }
    void HandelLevelStart(Level level)
    {
        levelData = level;
        TimeEvents.timerNotifactions?.Invoke(TimeEvents.TimerStatus.START);

    }
    void HandleTimerNotif(TimeEvents.TimerStatus status)
    {
        switch (status) 
        {
            case TimeEvents.TimerStatus.START:
                //start timer
                startTimer();
                break;
            case TimeEvents.TimerStatus.STOP:
                //stop timer
                stopTimer();
                break;
            case TimeEvents.TimerStatus.PAUSE:
                //pause timer
                pauseTimer();
                break;
        }
    }
    void startTimer()
    {
        Debug.Log("StartingTimer");
        isActive = true;
    }
    void stopTimer() 
    {
        Debug.Log("StopingTimer");
        //set the timer to flase, read time, and save
        isActive = false; 
        //send some timer event
        //save using the data mng

        timer = 0;
    }
    void pauseTimer()
    {
        Debug.Log("Pauseing Timer");
        if (isActive) 
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
    }

}
