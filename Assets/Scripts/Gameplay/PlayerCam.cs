using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    [SerializeField] Transform orientation;

    float xRotation;
    float yRotation;

    [SerializeField] InputCollector input;
    Vector2 lookInput;

    public Slider sliderX, sliderY;

    bool camLocked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //input = GetComponent<InputCollector>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        input.playerActions.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.playerActions.Look.started += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.playerActions.Look.canceled += ctx => lookInput = ctx.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sliderX != null)
        {
            sliderX.value = sensX;
        }

        if (camLocked)
            return;

        float mouseX = lookInput.x * Time.deltaTime * sensX;
        float mouseY = lookInput.y * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void AdjustXSpeed(float newSpeed) {
        sensX = newSpeed;
        sensY = newSpeed;
    }

    public void AdjustYSpeed(float newSpeed) {
        sensY = newSpeed;
    }

    public void SetCamLock(bool value)
    {
        camLocked = value;
    }
}
