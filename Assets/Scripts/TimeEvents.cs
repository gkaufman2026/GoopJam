using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TimeEvents 
{
   public enum TimerStatus
    {
        START,
        PAUSE,
        STOP
    };
    
    public static UnityAction<TimerStatus> timerNotifactions;
    public static UnityAction<Level, float> dataNotif;
    public static UnityAction<float> reciveTime;
    //this event is scuffed
    public static UnityAction <DataManager> dataRequest;
}
