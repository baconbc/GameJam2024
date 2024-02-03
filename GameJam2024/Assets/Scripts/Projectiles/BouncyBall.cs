using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    [SerializeField] private float speed = 3f; //Default value of 3
    [SerializeField] private Vector2 direction; //Declared as a SerializeFields for testing/debugging purposes
    [SerializeField] private Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidBody.velocity = direction * speed * Time.fixedDeltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision) //called when collision occurs for this object
    {
        Debug.Log("I'm working!");
        //Check for Tag here - if not proper type, delete object
        Vector2 NormalVector = collision.contacts[0].normal; //Records normal vector for the possibility of more complex calculations later
        Vector2 VelocityVector = rigidBody.velocity; //Records current velocity rather than direction
        rigidBody.velocity = Vector2.Reflect(VelocityVector, NormalVector); //Uses base reflect function, mirroring across the normal vector
        direction = rigidBody.velocity.normalized; //Records normal vector for direction so speed can be set in FixedUpdate manually
    }
}
