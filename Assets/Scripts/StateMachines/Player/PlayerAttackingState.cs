using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }


    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if(normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else  // go back to locomotion or targeting state
        {
           if(stateMachine.Targeter.CurrentTarget != null)
           {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
           }
           else
           {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
           }
        }

        

        previousFrameTime = normalizedTime;
    }


    public override void Exit()
    {

    }



    private void TryComboAttack(float normalizedTime) // checks to see if an attack can combo
    {
        if (attack.ComboAttackTime == -1) { return; } // make sure there is a combo attack

        // make sure far enough through it for the next attack
        if (normalizedTime < attack.ComboAttackTime) { return; }

        // then switch to the next attack
        stateMachine.SwitchState
        (
             new PlayerAttackingState
             (
                 stateMachine,
                 attack.ComboStateIndex
             )
        );
    }


    private void TryApplyForce()
    {
        if(alreadyAppliedForce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }
}
