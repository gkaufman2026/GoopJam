using UnityEngine;
using UnityEngine.Events;

public class UnlockDashScript : MonoBehaviour
{
    public UnityEvent UnlockDashEvent;
    [SerializeField] float unlockDashLevelID = 6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void LevelStart(Level level)
    {
        if (level.levelNumber == unlockDashLevelID)
        {
            UnlockDashEvent.Invoke();
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
