using UnityEngine;
using UnityEngine.Events;

public class IngameMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuObject;

    [SerializeField] UnityEvent<bool> MenuOpenEvent;

    InputCollector input;
    LevelManager levelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelManager = LevelManager.Instance;
        input = GetComponent<InputCollector>();

        input.playerActions.Refresh.started += ctx => RestartLevel();

        input.playerActions.Escape.started += ctx => OpenClosePause();

        pauseMenuObject.SetActive(false);
    }

    void RestartLevel()
    {
        levelManager.TryRestartLevel();
    }

    public void OpenClosePause()
    {
        if (pauseMenuObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            pauseMenuObject.SetActive(false);
            MenuOpenEvent.Invoke(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            pauseMenuObject.SetActive(true);
            MenuOpenEvent.Invoke(true);
        }
        
    }
}
