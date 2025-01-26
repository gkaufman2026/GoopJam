using TMPro;
using UnityEngine;

public class NotificationManaher : MonoBehaviour
{
    [SerializeField] TMP_Text notifText;

    private void Start()
    {
        notifText.text = string.Empty;
    }

    public void UnlockDash()
    {
        notifText.text = "Dash unlocked. Press Shift";
    }
}
