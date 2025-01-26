using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class StateFadeManager : MonoBehaviour
{
    public UnityEvent<float> SetFadePercent;

    [SerializeField] float fadeDuration = 2;

    bool fading;
    float fadeTimeStart;

    GravityState lastState;

    private void Start()
    {
        //ReceiveStateSwap(GravityState.Down);
        SetFadePercent.Invoke(0);
    }

    private void Update()
    {
        if (fading)
        {
            float fadeTime = Time.time - fadeTimeStart;
            Debug.Log("FADING! FADETIME IS " + fadeTime);

            if (fadeTime >= fadeDuration)
            {
                fading = false;
                SetFadePercent.Invoke(0);
            }
            else
            {
                SetFadePercent.Invoke((1 - (fadeTime / fadeDuration)));
            }
                
        }
    }

    public void ReceiveStateSwap(GravityState state)
    {
        if (state == GravityState.Down)
        {
            Debug.Log("SETTING STATE DOWN! LAST STATE WAS " + lastState);
            if (lastState == GravityState.Up)
            {
                fading = true;
                fadeTimeStart = Time.time;
            }
        }
        else if (state == GravityState.Up)
        {
            // TODO: Perform the swap
            SetFadePercent.Invoke(1);
        }

        lastState = state;
    }
}
