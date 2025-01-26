using TMPro;
using UnityEngine;

public class DashUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Rigidbody target;

    [SerializeField] float greenForce;

    bool dashUnlocked;

    bool lockText;

    private void Start()
    {
        dashUnlocked = false;
        text.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dashUnlocked)
            return; 

        if (!lockText)
        {
            float force = (Mathf.Abs(target.linearVelocity.y));

            text.text = "Dash Force: " + Mathf.Round(force * 100f) / 100f;

            float comp = force / greenForce;
            if (comp < 0.5)
            {
                text.color = Color.Lerp(Color.red, Color.yellow, comp * 2);
            }
            else
            {
                text.color = Color.Lerp(Color.yellow, Color.green, (comp - 0.5f) * 2);
            }
            
        }       
    }

    public void DashEvent()
    {
        lockText = true;
    }

    public void GroundedEvent()
    {
        lockText = false;
    }

    public void UnlockDash()
    {
        dashUnlocked = true;
    }
}
