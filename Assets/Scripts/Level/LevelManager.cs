using UnityEngine;
using UnityEngine.Events;

public enum LevelState { InProgress, NoLevel }
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public UnityEvent<Level> RestartLevelEvent;
    public UnityEvent<Level> StartLevelEvent;

    [SerializeField] Level testLevel;

    private Level currentLevel;

    LevelState currentState;

    bool gamerunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        currentState = LevelState.NoLevel;
    }

    // Update is called once per frame
    void Update()
    {
        // makeshift late start
        // TODO: Actual level management
        if (!gamerunning)
        {
            StartLevel(testLevel);
            gamerunning = true;
        }
    }

    void StartLevel(Level newLevel)
    {
        if (currentState == LevelState.InProgress)
        {
            Debug.LogError("TRYING TO START LEVEL WHILE ANOTHER IS IN PROGRESS!!!");
            return;
        }

        currentState = LevelState.InProgress;

        currentLevel = newLevel;
        StartLevelEvent.Invoke(currentLevel);

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
