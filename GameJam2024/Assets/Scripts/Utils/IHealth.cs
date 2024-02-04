using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHealth : MonoBehaviour 
{
    [SerializeField] private int maxHealth;
    private int health;

    public virtual void Awake()
    {
        SetHealth(maxHealth);
    }


    public int GetHealth()
    {
        return health;
    }

    private void SetHealth(int value)
    {
        health = value;
        OnSetHealth();
    }


    public void TakeDamage(int damage)
    {
        SetHealth(health - damage);
        if (health <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        SetHealth(maxHealth);
    }

    // Called when health reaches zero
    protected abstract void Die();
    // Called whenever health is set
    protected abstract void OnSetHealth();
}
