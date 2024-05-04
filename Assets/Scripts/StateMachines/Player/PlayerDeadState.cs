using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
       // toggle Ragdoll
       stateMachine.Ragdoll.ToggleRagdoll(true);
       PlayDieClip();
       stateMachine.Weapon.gameObject.SetActive(false);
       stateMachine.LevelManager.LoadGameOver();
    }

    public override void Tick(float deltaTime)
    {
       
    }

    public override void Exit()
    {
        
    }

    public void PlayDieClip()
    {
        PlayClip(stateMachine.dieClip, stateMachine.dieVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
