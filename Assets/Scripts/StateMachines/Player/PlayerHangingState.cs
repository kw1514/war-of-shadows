using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
     private Vector3 ledgeForward;
     private Vector3 closestPoint;

     private readonly int HangingHash = Animator.StringToHash("Hanging");
     private readonly int LeftShimmyHash = Animator.StringToHash("LeftShimmy");
     private readonly int RightShimmyHash = Animator.StringToHash("RightShimmy");
     private const float CrossFadeDuration = 0.1f;
     private const float HorizontalSpeed = 2f;

     public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward, Vector3 closestPoint) : base(stateMachine)
     {
          this.ledgeForward = ledgeForward;
          this.closestPoint = closestPoint;
     }

     public override void Enter()
     {
          stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);

          stateMachine.Controller.enabled = false;
          stateMachine.transform.position = closestPoint - (stateMachine.LedgeDetector.transform.position - stateMachine.transform.position);
          stateMachine.Controller.enabled = true;

          stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFadeDuration);
     }

     public override void Tick(float deltaTime)
     {
          if (stateMachine.InputReader.MovementValue.y > 0f)
          {
               stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
          }
          else if (stateMachine.InputReader.MovementValue.y < 0f)
          {
               stateMachine.Controller.Move(Vector3.zero);
               stateMachine.ForceReceiver.Reset();
               stateMachine.SwitchState(new PlayerFallingState(stateMachine));
          }
          else if (stateMachine.InputReader.MovementValue.x != 0f)
          {
            Vector3 right = Vector3.Cross(Vector3.up, ledgeForward).normalized;
            Vector3 moveDirection = right * stateMachine.InputReader.MovementValue.x;
            stateMachine.Controller.Move(moveDirection * HorizontalSpeed * deltaTime);

               if (stateMachine.InputReader.MovementValue.x > 0f)
               {
                    stateMachine.Animator.CrossFadeInFixedTime(RightShimmyHash, CrossFadeDuration);
               }
               else if (stateMachine.InputReader.MovementValue.x < 0f)
               {
                    stateMachine.Animator.CrossFadeInFixedTime(LeftShimmyHash, CrossFadeDuration);
               }
          }
     }

     public override void Exit()
     {

     }
}
