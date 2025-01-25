using UnityEngine;

// This script is just a holder of the New Input System so that it's decoupled from everything else
// access it as necessary via InputCollector.playerActions
public class InputCollector : MonoBehaviour
{
    InputSystem_Actions inputActions;

    // just grab this to add input events
    public InputSystem_Actions.PlayerActions playerActions { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        playerActions = inputActions.Player;
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }
}

//inputActions.Player.Move.performed += ctx => ProcessMove(ctx.ReadValue<Vector2>());
//inputActions.Player.Move.canceled += ctx => ProcessMove(ctx.ReadValue<Vector2>());

//inputActions.Player.Jump.performed += ctx => movement.Jump();

//inputActions.Player.Attack.performed += ctx => StartSwing();

//inputActions.Player.Sprint.performed += ctx => Dash();