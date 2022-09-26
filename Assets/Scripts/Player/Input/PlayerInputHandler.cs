using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    private Vector2 movementInput;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }


    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("jump button pushed down");
        }

        if (context.performed)
        {
            Debug.Log("jump button is being held down");
        }

        if (context.canceled)
        {
            Debug.Log("jump button is has been released");
        }
    }
}
