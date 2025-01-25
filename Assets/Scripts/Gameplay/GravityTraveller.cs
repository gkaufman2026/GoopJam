using UnityEngine;
using UnityEngine.Events;

public enum GravityState { Up, Down }

public class GravityTraveller : MonoBehaviour
{
    [Tooltip("This event is invoked when the Gravity Traveller swaps gravity.")]
    public UnityEvent<GravityState> gravityEvent;

    ConstantForce gravitySimulator;
    private void Start()
    {
        gravitySimulator = GetComponent<ConstantForce>();
        gravitySimulator.force = Physics.gravity;
    }

    public void SwapGravity(GravityState state)
    {
        if (state == GravityState.Up)
        {
            gravitySimulator.force = Physics.gravity * -1;
        }
        else if (state == GravityState.Down)
        {
            gravitySimulator.force = Physics.gravity;
        }

        gravityEvent.Invoke(state);
    }
}
