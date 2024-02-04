using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : IHealth
{
    [SerializeField] private GameObject bloodSplatter;
    private StatusEffectManager sem;

    public override void Awake()
    {
        base.Awake();
        sem = transform.parent.GetComponent<StatusEffectManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile p = collision.gameObject.GetComponent<Projectile>();
            if (p.noCollide != "Enemy")
            {
                Debug.Log("hit by bullet");
                sem.AddEffect(p.effectType);
                TakeDamage(p.GetDamage());
                Destroy(p.gameObject);
            }
        }
    }

    protected override void Die()
    {
        Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }

    protected override void OnSetHealth()
    {
        return;
    }
}
