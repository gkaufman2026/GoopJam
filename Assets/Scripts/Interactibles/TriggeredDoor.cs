using UnityEngine;

public class TriggeredDoor : MonoBehaviour
{
    [SerializeField] Vector3 OpenedDoorOffset = Vector3.up;

    bool doorMoving;
    bool doorActivated;

    float doorMoveDuration = 2;
    float activateTime;

    Vector3 startPosition;

    [SerializeField] Material pressedMaterial;
    [SerializeField] Material notPressedMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelManager.Instance.RestartLevelEvent.AddListener(ResetDoor);
        LevelManager.Instance.StartLevelEvent.AddListener(ResetDoor);

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorMoving)
        {
            transform.position = startPosition + Vector3.Lerp(Vector3.zero, OpenedDoorOffset, ((Time.time - activateTime) / doorMoveDuration));

            if (Time.time - activateTime > doorMoveDuration)
            {
                doorMoving = false;
            }
        }
    }

    public void OnTriggered()
    {
        doorActivated = true;
        doorMoving = true;

        activateTime = Time.time;

        UpdateMaterial();
    }

    public void ResetDoor(Level level)
    {
        doorMoving = false;
        doorActivated = false;

        transform.position = startPosition;
        activateTime = -1;

        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        if (doorActivated)
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
        LevelManager.Instance.RestartLevelEvent.RemoveListener(ResetDoor);
        LevelManager.Instance.StartLevelEvent.RemoveListener(ResetDoor);
    }
}
