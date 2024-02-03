using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    //[SerializeField] protected float bouncesRemaining; For later

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        // Fetch/cache the GameObject you collided with
        GameObject other = collision.gameObject;

        Debug.Log(other.tag);


        
        if (other.tag == noCollide || other.tag == "Projectile")
        {
            return;
        }
        else
        {
            if (other.tag == "Player")
                other.GetComponent<PlayerHealth>().TakeDamage(damage);
            else if (other.tag == "Enemy")
                other.GetComponent<EnemyHealth>().TakeDamage(damage);

            if (other.tag != "Shield")
                Destroy(gameObject);
        }
    }

    public void ReturnToSender(Transform shield)
    {
        noCollide = "Player"; // projectile has been parried and now targets enemies instead of players

        float y = (transform.position.y - shield.position.y)/shield.GetComponent<BoxCollider2D>().bounds.size.y;
        float x = (transform.position.x - shield.position.x)/shield.GetComponent<BoxCollider2D>().bounds.size.x;
        Vector2 dir = new Vector2(x, y).normalized;

        rb.velocity = dir * speed;
    }
}
