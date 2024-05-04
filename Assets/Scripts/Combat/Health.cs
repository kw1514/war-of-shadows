using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;
    private bool isInvulnerable;

    public event Action OnTakeDamage;
    public event Action OnDie;

    // one line method. returns true if health = 0
    public bool IsDead => health == 0;


    void Start()
    {
        health = maxHealth;
    }


    public int GetStartHealth()
    {
        return maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int damage)
    {
        if (health == 0) { return; } // character is dead

        if(isInvulnerable) { return; } // character is blocking

        health = Mathf.Max(health - damage, 0); // will set health to largest of the two numbers.

        OnTakeDamage?.Invoke();

        if(health == 0)
        {
            OnDie?.Invoke(); // question mark is a null check
        }
    }
}
