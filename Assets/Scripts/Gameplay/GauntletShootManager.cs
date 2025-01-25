using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GauntletShootManager : MonoBehaviour
{
    [SerializeField] GameObject bubbleTemplate;
    [SerializeField] Transform shootDirectionTarget;
    [SerializeField] Transform shootPositionTarget;

    [SerializeField] float shootForce = 100;

    float maxBubbles = 0;
    float currentBubbles;

    [SerializeField] List<GameObject> bubbles;

    InputCollector input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<InputCollector>();

        input.playerActions.Attack.started += ctx => TryShoot();

        //ctx.ReadValue<bool>();

        LevelManager.Instance.RestartLevelEvent.AddListener(OnBeginLevel);
        LevelManager.Instance.StartLevelEvent.AddListener(OnBeginLevel);

        SetBubbleVisual();
    }

    void TryShoot()
    {
        // TODO: Manage ammunition
        if (currentBubbles <= 0)
        {
            Debug.LogWarning("NO BUBBLES LEFT!!! TODO: ALERT PLAYER");
            return;
        }

        currentBubbles -= 1;

        LaunchProjectile();
        SetBubbleVisual();
    }

    void LaunchProjectile()
    {
        GameObject bubble = Instantiate(bubbleTemplate, shootPositionTarget.position, shootDirectionTarget.rotation);
        Rigidbody rb = bubble.GetComponent<Rigidbody>();

        rb.AddForce(shootDirectionTarget.forward * shootForce);
    }

    void OnBeginLevel(Level level)
    {
        maxBubbles = level.bubbleCount;
        currentBubbles = maxBubbles;

        SetBubbleVisual();
    }

    void SetBubbleVisual()
    {
        foreach (var bubble in bubbles)
        {
            bubble.SetActive(true);
        }

        if (currentBubbles < 3)
        {
            bubbles[2].SetActive(false);
        }
        if (currentBubbles < 2)
        {
            bubbles[1].SetActive(false);
        }
        if (currentBubbles < 1)
        {
            bubbles[0].SetActive(false);
        }
    }
}
