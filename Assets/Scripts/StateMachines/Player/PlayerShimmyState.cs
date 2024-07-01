using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShimmyState : PlayerBaseState
{
    private Vector3 ledgeForward;
    private float horizontalInput;

    private readonly int LeftShimmyHash = Animator.StringToHash("LeftShimmy");
    private readonly int RightShimmyHash = Animator.StringToHash("RightShimmy");
    private const float CrossFadeDuration = 0.1f;
    private const float HorizontalSpeed = 2f;

    public PlayerShimmyState(PlayerStateMachine stateMachine, Vector3 ledgeForward, float horizontalInput) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
        this.horizontalInput = horizontalInput;
    }

    public override void Enter()
    {
        // Determine which shimmy animation to play
        if (horizontalInput > 0f)
        {
            stateMachine.Animator.CrossFadeInFixedTime(RightShimmyHash, CrossFadeDuration);
        }
        else if (horizontalInput < 0f)
        {
            stateMachine.Animator.CrossFadeInFixedTime(LeftShimmyHash, CrossFadeDuration);
        }
    }

    public override void Tick(float deltaTime)
    {
        Vector3 right = Vector3.Cross(Vector3.up, ledgeForward).normalized;
        Vector3 moveDirection = right * horizontalInput;

        // Move the character
        stateMachine.Controller.Move(moveDirection * HorizontalSpeed * deltaTime);

        Vector2 movementInput = stateMachine.InputReader.MovementValue;

        // Switch to hanging state if there's no horizontal input
        if (movementInput.x == 0f)
        {
            stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward, stateMachine.transform.position));
        }
        // Adjust shimmy direction if input changes
        else if (movementInput.x != horizontalInput)
        {
            stateMachine.SwitchState(new PlayerShimmyState(stateMachine, ledgeForward, movementInput.x));
        }
    }

    public override void Exit()
    {
        
    }
}
