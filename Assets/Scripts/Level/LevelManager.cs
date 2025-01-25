using UnityEngine;
using UnityEngine.Events;

public enum LevelState { InProgress, NoLevel }
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public UnityEvent<Level> RestartLevelEvent;

    [SerializeField] Level testLevel;

    private Level currentLevel;

    LevelState currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentState = LevelState.InProgress;
        Instance = this;

        currentLevel = testLevel;
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

    public void TryRestartLevel()
    {
        if (currentState == LevelState.InProgress)
        {
            Debug.Log("RESTARTING LEVEL");

            RestartLevelEvent.Invoke(currentLevel);
        }
        else
        {
            Debug.Log("NO LEVEL 2 RESTART");
        }
    }
}
