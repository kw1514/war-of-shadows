using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue {get; private set;} // is a property
    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }

    public event Action JumpEvent; // when the event happens (the space bar is clicked)
    public event Action DodgeEvent;
    public event Action TargetEvent;

    private Controls controls;

    void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);  // this references the input reader

        controls.Player.Enable();
    }

    private void OnDestroy() 
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        
        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
       if (!context.performed) { return; }
        
        TargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
       if (context.performed) // becuase using bool the pplayer can hold button down to attack
       {
        IsAttacking = true;
       }
       else if (context.canceled)
       {
        IsAttacking = false;
       }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed) // becuase using bool the pplayer can hold button down to block
       {
        IsBlocking = true;
       }
       else if (context.canceled)
       {
        IsBlocking = false;
       }
    }

    public void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
