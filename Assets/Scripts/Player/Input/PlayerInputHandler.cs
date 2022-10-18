using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public bool TriggerDialog { get; private set; }
    public bool DialogInput { get; private set; }
    public Vector2 RawMovementInput { get; private set; }
    public int NormaInputX { get; private set; }
    public int NormaInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    public bool[] AttackInput { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;
    private float triggerInputStartTime;
    private float dialogInputStartTime;

    private PlayerInput playerInput;

    private void Update()
    {
        ChheckJumpInputTime();
        TriggerDialog = (Time.time >= triggerInputStartTime + inputHoldTime) ? false : true;
        DialogInput = (Time.time >= dialogInputStartTime + inputHoldTime) ? false : true;
    }

    private void Start()
    {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInput = new bool[count];
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput[(int)CombatInputs.primary] = true;
        }
        if (context.canceled)
        {
            AttackInput[(int)CombatInputs.primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput[(int)CombatInputs.secondary] = true;
        }
        if (context.canceled)
        {
            AttackInput[(int)CombatInputs.secondary] = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormaInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormaInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
        }
    }


    public void OnDialogInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DialogInput = true;
        }
    }

    public void OnTriggerDialog(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TriggerDialog = true;
        }
    }

    public void ChangeCurrentActionMapToDialog()
    {
        playerInput.actions.FindActionMap("Gameplay").Disable();
        playerInput.actions.FindActionMap("Dialog").Enable();
    }

    public void ChangeCurrentActionMapToGameplay()
    {
        playerInput.actions.FindActionMap("Dialog").Disable();
        playerInput.actions.FindActionMap("Gameplay").Enable();
    }

    public void UseTriggerDialog() => TriggerDialog = false;

    public void UseDialogInput() => DialogInput = false;

    public void UseDashInput() => DashInput = false;

    public void UseJumpInput() => JumpInput = false;

    private void ChheckJumpInputTime() => JumpInput = (Time.time >= jumpInputStartTime + inputHoldTime) ? false: JumpInput;
}

public enum CombatInputs
{
    primary,
    secondary
}