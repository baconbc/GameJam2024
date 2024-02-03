using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[SerializeField] override float speed;
//[SerializeField] private float currentDamage;

public class BouncyBall : Projectile
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == noCollide || other.tag == "Projectile")
        {
            return;
        }
        else if (other.tag == "Player") //Destroy Self and Damage Player
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.tag == "Enemy") //Destroy self and Damage Enemy
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else //Reflects off of object
        {
            //Records Normal vector for collision
            Vector2 NormalVector = (collision.transform.position-transform.position).normalized;
            
            Vector2 VelocityVector = rb.velocity;
            rb.velocity = Vector2.Reflect(VelocityVector, NormalVector);
            
        }
        /*Debug.Log("collided");
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
        }*/
    }
}