using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.StartNextLevel();
        }
    }
}
