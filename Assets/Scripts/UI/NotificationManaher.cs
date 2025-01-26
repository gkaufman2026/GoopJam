using TMPro;
using UnityEngine;

public class NotificationManaher : MonoBehaviour
{
    [SerializeField] TMP_Text notifText;

    float currentLevel;

    private void Start()
    {
        notifText.text = string.Empty;
    }

    public void UnlockDash()
    {
        notifText.text = "Dash unlocked. Press Shift";
    }

    public void StartLevel(Level level)
    {
        currentLevel = level.levelNumber;

        SetLevelText();
    }

    public void RestartLevel()
    {
        SetLevelText();
    }

    void SetLevelText()
    {
        switch (currentLevel)
        {
            case 1:
                notifText.text = "WASD to move. Space to jump.";
                break;
            case 2:
                notifText.text = "Left Mouse to shoot.";
                break;
            case 3:
                notifText.text = "Buttons...";
                break;
            case 4:
                notifText.text = "I Believe In You";
                break;
            case 5:
                notifText.text = "Tricky";
                break;
            case 6:
                notifText.text = "Dash unlocked. Press Shift.";
                break;
            case 7:
                notifText.text = "The Ceiling is Lavs";
                break;
            case 8:
                notifText.text = "All Around";
                break;
            case 9:
                notifText.text = "This Is It";
                break;
            default:
                notifText.text = "How did you get here...";
                break;
        }
    }

    public void SetRestartPrompt()
    {
        notifText.text = "Press R to Restart";
    }
}
