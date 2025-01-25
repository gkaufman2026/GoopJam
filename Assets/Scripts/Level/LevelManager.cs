using UnityEngine;

public enum LevelState { InProgress, NoLevel }
public class LevelManager : MonoBehaviour
{
    LevelState currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = LevelState.InProgress;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelComplete()
    {
        if (currentState == LevelState.NoLevel)
            return;

        currentState = LevelState.NoLevel;
    }
}
