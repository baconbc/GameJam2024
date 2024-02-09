using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : IHealth
{
    [Header("Enemy Vars")]
    [SerializeField] private GameObject bloodSplatter;
    [SerializeField] private string damageSound = "Hurt";

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Projectile")
    //    {
    //        Projectile p = collision.gameObject.GetComponent<Projectile>();
    //        if (p.noCollide != "Enemy")
    //        {
    //            Debug.Log("hit by bullet");
    //            sem.AddEffect(p.effectType);
    //            TakeDamage(p.GetDamage());
    //            Destroy(p.gameObject);
    //        }
    //    }
    //}

    protected override void Die()
    {
        Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        AudioManager.Instance.Play(damageSound, "enemy");
        Destroy(transform.parent.gameObject);
    }

    protected override void OnSetHealth()
    {
        return;
    }

    protected override bool ShouldTakeDamage(Projectile p)
    {
        return p.noCollide != "Enemy";
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        AudioManager.Instance.Play(damageSound, "enemy");
    }
}
