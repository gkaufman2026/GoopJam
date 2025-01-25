using UnityEngine;

public class IngameMenuManager : MonoBehaviour
{
    InputCollector input;
    LevelManager levelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelManager = LevelManager.Instance;
        input = GetComponent<InputCollector>();

        input.playerActions.Refresh.started += ctx => RestartLevel();
    }

    void RestartLevel()
    {
        levelManager.TryRestartLevel();
    }
}
