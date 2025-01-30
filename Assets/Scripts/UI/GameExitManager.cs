using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExitManager : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadScene("Menue");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
