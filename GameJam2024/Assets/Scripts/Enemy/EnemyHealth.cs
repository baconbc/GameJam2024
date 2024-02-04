using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : IHealth
{
    [SerializeField] CapsuleCollider2D cc;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Projectile p = collision.gameObject.GetComponent<Projectile>();
            if (p.noCollide != "Enemy")
            {
                Debug.Log("hit by bullet");
                TakeDamage(p.GetDamage());
                Destroy(p.gameObject);
            }
        }
    }

    //public void TakeDamage(int damage)
    //{
    //    health -= damage;
    //    Debug.Log(health);
    //    if (health <= 0)
    //    {
    //        Die();
    //    }
    //}

    protected override void Die()
    {
        Destroy(transform.parent.gameObject);
    }

    protected override void OnSetHealth()
    {
        return;
    }
}
