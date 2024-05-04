using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public override void Enter()
    {
      stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
      Move(deltaTime);

      if(IsInChaseRange())
      {
        stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        return;
      }
      FacePlayer();

      stateMachine.Animator.SetFloat(SpeedHash, 0, 0.1f, deltaTime);
    }

    public override void Exit() { }
}
