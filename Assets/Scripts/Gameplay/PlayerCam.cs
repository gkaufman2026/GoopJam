using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    public Slider[] sliders;
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    [SerializeField] Transform orientation;

    float xRotation;
    float yRotation;

    [SerializeField] InputCollector input;
    Vector2 lookInput;

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
        PlayerPrefs.SetFloat("currentSensX", sensX);
        PlayerPrefs.SetFloat("currentSensY", sensY);

        sliders[0].value = sensX;
        sliders[1].value = sensY;

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
    }

    public void AdjustYSpeed(float newSpeed) {
        sensY = newSpeed;
    }
}
