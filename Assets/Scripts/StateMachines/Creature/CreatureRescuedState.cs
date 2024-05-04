using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatureRescuedState : CreatureBaseState
{
    public CreatureRescuedState(CreatureStateMachine stateMachine) : base(stateMachine) { }

    private readonly int RescueHash = Animator.StringToHash("Rescue");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private int origianalCount;
    

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(RescueHash, 0.1f);
        stateMachine.PlayRescueClip();
        stateMachine.UIDisplay.SetPopUp(true, "You Rescued Me!");

        GameObject creature = GameObject.FindGameObjectWithTag("Creature");

        creature.tag = "Rescued";

        origianalCount = stateMachine.GetOriginalCount();
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(SpeedHash, 1, 0.1f, deltaTime);

        if(!IsInRange())
        {
            stateMachine.UIDisplay.SetPopUp(false, "");
            stateMachine.SwitchState(new CreatureStandingState(stateMachine));
        }

        int length = GameObject.FindGameObjectsWithTag("Rescued").Length;

        string text = ((length - 1) + " of " + origianalCount + " Rescued");
        stateMachine.UIDisplay.SetCountText(text);
    }

    public override void Exit()
    {
    }
}
