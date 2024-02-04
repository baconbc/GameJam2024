using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected int bouncesRemaining;
    [SerializeField] public StatusEffectType effectType;

    
    public Rigidbody2D rb;
    public string noCollide = "Enemy"; //at the start, projectiles cannot hit other enemies

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; //Old format - AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == noCollide || other.tag == "Projectile")
        {
            return;
        }
        else if (other.tag == "Player") //Destroy Self and Damage Player
        {
            other.GetComponentInChildren<IHealth>().TakeDamage(damage);
            AudioManager.Instance.Play("PlayerHurt");
            other.GetComponent<StatusEffectManager>().AddEffect(effectType);
            Destroy(gameObject);
        }
        //else if (other.tag == "Enemy") //Destroy self and Damage Enemy
        //{
        //    other.GetComponent<EnemyHealth>().TakeDamage(damage);
        //    AudioManager.Instance.Play("EnemyDeath");
        //    Destroy(gameObject);
        //}
        else //Reflects off of object
        {
            if (bouncesRemaining > 0) //Bounces if bounces are remaining
            {
                Vector2 NormalVector = collision.contacts[0].normal; //Records normal vector for the possibility of more complex calculations later
                Vector2 VelocityVector = rb.velocity;
                rb.velocity = Vector2.Reflect(VelocityVector, NormalVector); 
                bouncesRemaining--;
            }
            else //Deletes otherwise
            {
                if (other.tag == "Player")
                    other.GetComponentInChildren<IHealth>().TakeDamage(damage);
                //else if (other.tag == "Enemy")
                //    other.GetComponent<EnemyHealth>().TakeDamage(damage);

                if (other.tag != "Shield")
                    Destroy(gameObject);
            }
        }
    }

    public virtual void ReturnToSender(Transform shield, Collision2D collision)
    {
        noCollide = ""; // projectile has been parried and now targets enemies instead of players
        
        float y = (transform.position.y - shield.position.y)/shield.GetComponent<BoxCollider2D>().bounds.size.y;
        float x = (transform.position.x - shield.position.x)/shield.GetComponent<BoxCollider2D>().bounds.size.x;
        Vector2 dir = new Vector2(x, y).normalized;

        float rotation = (Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg) + 90;
        rb.rotation = rotation;

        rb.velocity = dir * speed;
    }


    public int GetDamage()
    {
        return damage;
    }
}
