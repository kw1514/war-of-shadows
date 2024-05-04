using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatureStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public UIDisplay UIDisplay { get; private set; }

    [field: SerializeField] public float PlayerRange { get; private set; }
    public List<int> Creatures { get; private set; }
    
    [SerializeField] AudioClip rescueClip;
    [SerializeField] [Range(0f, 1f)] float rescueVolume = 1f;

    public GameObject Player { get; private set; }

    public int length { get; private set; }


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        length = GameObject.FindGameObjectsWithTag("Creature").Length;            

        string text = ("0 of " + length + " Rescued");
        this.UIDisplay.SetCountText(text);

        SwitchState(new CreatureIdleState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerRange);
    }

    public int GetOriginalCount()
    {
        return length;
    }

    public void PlayRescueClip()
    {
        PlayClip(rescueClip, rescueVolume);
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
