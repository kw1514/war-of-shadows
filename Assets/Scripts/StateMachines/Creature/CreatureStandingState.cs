using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStandingState : CreatureBaseState
{
    public CreatureStandingState(CreatureStateMachine stateMachine) : base(stateMachine) { }

    private readonly int IdleHash = Animator.StringToHash("Idle");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(IdleHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        stateMachine.Animator.SetFloat(SpeedHash, 0, 0.1f, deltaTime);
    }

    public override void Exit()
    {
        
    }
}
