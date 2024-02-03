using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartBouncyBall : MonoBehaviour
{
    [SerializeField] private float speed = 5f; //Default value of 3
    [SerializeField] private Vector2 direction; //Declared as a SerializeFields for testing/debugging purposes
    [SerializeField] protected int damage;
    [SerializeField] private Rigidbody2D rigidBody;
    private string dontCollide = "Enemy"; //at the start, projectiles cannot hit other enemies

    [SerializeField] private GameObject target; //Target for aiming towards
    [SerializeField] private float Curvature = 3f; //Amount that the projectile curves towards the target

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = direction * speed;
    }

    void FixedUpdate()
    {
        Vector2 NormalToTarget = new Vector2(target.transform.position.x-rigidBody.position.x, target.transform.position.y-rigidBody.position.y).normalized; //Measures normal vector to target
        //rigidBody.AddForce(Curvature * NormalToTarget * Time.fixedDeltaTime);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x + NormalToTarget.x * Curvature * Time.fixedDeltaTime, rigidBody.velocity.y + NormalToTarget.y * Curvature * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) //called when collision occurs for this object - functions the same as Projectile but overrides the collision function
    {
        if (collision.gameObject.tag == dontCollide || collision.gameObject.tag == "Projectile") //Flies through ignored object type, or other projectiles
        {
            return;
        }
        else if (collision.gameObject.tag == "Player") //Destroy self and damage player
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy") //Destroy self and damage enemy
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else //Reflects off of object 
        {
            //Possibly add a counter here for the amount of times it has been reflected, destroy if the limit is reached
            Vector2 NormalVector = collision.contacts[0].normal; //Records normal vector for the possibility of more complex calculations later
            Vector2 VelocityVector = rigidBody.velocity; //Records current velocity rather than direction
            rigidBody.velocity = Vector2.Reflect(VelocityVector, NormalVector); //Uses base reflect function, mirroring across the normal vector
            direction = rigidBody.velocity.normalized; //Records normal vector for direction so speed can be set in FixedUpdate manually
        }
    }

    public void ReturnToSender(Transform shield) //Uses implementation from Projectile.cs - need to make it so shield can function for other projectile types
    {
        dontCollide = "Player"; // projectile has been parried and now targets enemies instead of players

        float y = (transform.position.y - shield.position.y)/shield.GetComponent<BoxCollider2D>().bounds.size.y;
        float x = (transform.position.x - shield.position.x)/shield.GetComponent<BoxCollider2D>().bounds.size.x;
        Vector2 dir = new Vector2(x, y).normalized;

        rigidBody.velocity = dir * speed;
    }
}
