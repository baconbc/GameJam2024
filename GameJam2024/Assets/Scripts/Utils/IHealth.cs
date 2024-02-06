using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHealth : MonoBehaviour 
{
    [SerializeField] private int maxHealth;
    private int health;
    protected StatusEffectManager sem;

    public virtual void Awake()
    {
        SetHealth(maxHealth);
        sem = transform.parent.GetComponent<StatusEffectManager>();
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("is projectile");
            Projectile p = collision.gameObject.GetComponent<Projectile>();
            if (ShouldTakeDamage(p))
            {
                Debug.Log("hit by bullet");
                sem.AddEffect(p.effectType);
                TakeDamage(p.GetDamage());
                Destroy(p.gameObject);
            }
        }
    }


    public virtual void TakeDamage(int damage)
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

    protected abstract bool ShouldTakeDamage(Projectile p);
}
