using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureBaseState : State
{
    protected CreatureStateMachine stateMachine;

    public CreatureBaseState(CreatureStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    public bool IsInRange()
    {
        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.PlayerRange * stateMachine.PlayerRange;
    }
}
