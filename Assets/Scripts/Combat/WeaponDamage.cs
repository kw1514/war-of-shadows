using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;

    private List<Collider> alreadyCollideWith = new List<Collider>();

    [SerializeField] AudioClip impactClip;
    [SerializeField] [Range(0f, 1f)] float impactVolume = 1f;


    private void OnEnable() 
    {
        alreadyCollideWith.Clear();
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other == myCollider) { return; }

        if(alreadyCollideWith.Contains(other)) { return; } // already hit the enemy so return

        alreadyCollideWith.Add(other);

        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
            PlayImpactClip();
        }

        // adds knockback force to the weapons
        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }

    public void PlayImpactClip()
    {
        PlayClip(impactClip, impactVolume);
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
