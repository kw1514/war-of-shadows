using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureIdleState : CreatureBaseState
{
    public CreatureIdleState(CreatureStateMachine stateMachine) : base(stateMachine) { }

    private readonly int RescueHash = Animator.StringToHash("Rescue");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(RescueHash, 0.1f);
        
        // foreach(GameObject creature in stateMachine.Creatures)
        // {
        //     stateMachine.Creatures.Add(creature);
        // }

        //stateMachine.Creatures.Add(1);
        //stateMachine.Creatures.Add(1);

        // string text = ("0 of " + stateMachine.Creatures.Count + " Rescued");
        // stateMachine.UIDisplay.SetCountText(text);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (IsInRange())
        {
            stateMachine.SwitchState(new CreatureRescuedState(stateMachine));
            return;
        }

        stateMachine.Animator.SetFloat(SpeedHash, 0, 0.1f, deltaTime);
    }

    public override void Exit() { }
}
