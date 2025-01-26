using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum LevelState { InProgress, NoLevel }
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public UnityEvent<Level> RestartLevelEvent;
    public UnityEvent<Level> StartLevelEvent;
    public UnityEvent<Level> CompleteLevelEvent;

    [SerializeField] List<Level> levelList;

    //[SerializeField] Level testLevelPrefab;

    [SerializeField] GameObject StartTransitionAnchor;

    private Level currentLevel;
    private Level queuedLevel;

    LevelState currentState;

    int currentLevelIndex = 0;

    //bool gamerunning = false;
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

        CompleteLevelEvent.Invoke(currentLevel);
      
        GetNextLevel();
    }

    public void GetNextLevel()
    {
        // TODO: Add a sequence of levels
        currentLevelIndex++;
        if (currentLevelIndex >= levelList.Count)
        {
            Debug.LogWarning("WE ARE OUT OF NEW LEVELS! GOING BACK TO FIRST LEVEL.");
            currentLevelIndex = 0;
        }

        // Keep an eye on how this works if there are rotation changes
        // It should work but I'm not 100% sure
        Level nextLevel = Instantiate(levelList[currentLevelIndex], currentLevel.transitionAnchor.transform.position, currentLevel.transitionAnchor.transform.rotation);
        queuedLevel = nextLevel;
    }

    public void StartNextLevel()
    {
        StartLevel(queuedLevel);

    }

    public void StartGame()
    {
        Vector3 pos = StartTransitionAnchor.gameObject.transform.position;
        if (currentLevel != null)
        {
            pos = currentLevel.transitionAnchor.transform.position;
        }

        //  testLevelPrefab

        Level nextLevel = Instantiate(levelList[currentLevelIndex], pos, Quaternion.identity);
        StartLevel(nextLevel);
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
