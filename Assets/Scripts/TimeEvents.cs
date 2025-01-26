using System;
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
}
