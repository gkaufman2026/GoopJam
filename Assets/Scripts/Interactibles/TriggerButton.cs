using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{
    public UnityEvent triggerEvent;

    public bool isPressed = false;

    [SerializeField] Material pressedMaterial;
    [SerializeField] Material notPressedMaterial;

    private void Start()
    {
        LevelManager.Instance.RestartLevelEvent.AddListener(OnRestart);
        LevelManager.Instance.StartLevelEvent.AddListener(OnRestart);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            triggerEvent.Invoke();
            UpdateMaterial();
        }
    }

    void OnRestart(Level level)
    {
        ResetButton();
    }

    void ResetButton()
    {
        isPressed = false;
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        if (isPressed)
        {
            GetComponent<MeshRenderer>().material = pressedMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = notPressedMaterial;
        }

    }

    private void OnDestroy()
    {
        LevelManager.Instance.RestartLevelEvent.RemoveListener(OnRestart);
        LevelManager.Instance.StartLevelEvent.RemoveListener(OnRestart);
    }
}
