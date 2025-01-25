using UnityEngine;
using UnityEngine.Rendering;

public class GauntletShootManager : MonoBehaviour
{
    [SerializeField] GameObject bubbleTemplate;
    [SerializeField] Transform shootDirectionTarget;
    [SerializeField] Transform shootPositionTarget;

    [SerializeField] float shootForce = 100;

    float bubbleCount = 3;

    InputCollector input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<InputCollector>();

        input.playerActions.Attack.started += ctx => TryShoot();
        
        //ctx.ReadValue<bool>();
    }

    void TryShoot()
    {
        // TODO: Manage ammunition
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        GameObject bubble = Instantiate(bubbleTemplate, shootPositionTarget.position, shootDirectionTarget.rotation);
        Rigidbody rb = bubble.GetComponent<Rigidbody>();

        rb.AddForce(shootDirectionTarget.forward * shootForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
