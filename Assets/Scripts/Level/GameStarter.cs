using UnityEngine;

public class GameStarter : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.StartGame();
        }
    }
}
