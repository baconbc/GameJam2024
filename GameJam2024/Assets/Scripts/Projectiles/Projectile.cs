using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected int bouncesRemaining;
    [SerializeField] public StatusEffectType effectType;


    protected float timer;
    public Rigidbody2D rb;
    public Collider2D col;
    public string noCollide = "Enemy"; //at the start, projectiles cannot hit other enemies

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.velocity = transform.up * speed; //Old format - AddForce(transform.up * speed, ForceMode2D.Impulse);
        //col.enabled = false;
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        //if (!col.enabled && timer > 0.1 )
        //{
        //    col.enabled = true;
        //}
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == noCollide || other.tag == "Projectile" || other.tag == "Shield")
        {
            return;
        }
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
                if (other.tag != "Shield")
                    Destroy(gameObject);
            }
        }
    }

    public virtual void ReturnToSender(Transform shield, Collision2D collision)
    {
        Debug.Log("returning to sender");
        noCollide = ""; // projectile has been parried and now targets enemies instead of players

        //Debug.Log(collision.contacts[0].normal);
        //Debug.Log(collision.collider.transform.position);
        //Debug.Log(shield.position);
        //float y = (collision.contacts[0].point.y - shield.position.y)/shield.GetComponent<BoxCollider2D>().bounds.size.y;
        //float x = (collision.contacts[0].point.x - shield.position.x)/shield.GetComponent<BoxCollider2D>().bounds.size.x;
        //Vector2 dir = new Vector2(x, y).normalized;
        Vector2 dir = Vector2.Reflect(rb.velocity, collision.contacts[0].normal).normalized;

        
        float rotation = (Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg) + 90;

        Debug.Log(rb.rotation);
        Debug.Log(rotation);
        rb.rotation = rotation;

        rb.velocity = dir * speed;
        rb.position += dir * 0.1f; // To prevent projectile collider staying inside shield
    }


    public int GetDamage()
    {
        return damage;
    }
}
