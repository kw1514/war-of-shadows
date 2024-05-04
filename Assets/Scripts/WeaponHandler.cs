using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;

    [SerializeField] AudioClip attackClip;
    [SerializeField] [Range(0f, 1f)] float attackVolume = 1f;

    
    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
        PlayAttackClip();
    }

    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }

    public void PlayAttackClip()
    {
        PlayClip(attackClip, attackVolume);
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
