using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    //[SerializeField] protected float bouncesRemaining; For later

    private Rigidbody2D rb;
    private string noCollide = "Enemy";

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        // Fetch/cache the GameObject you collided with
        GameObject other = collision.gameObject;

        Debug.Log(other.tag);


        
        if (other.tag == noCollide)
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
