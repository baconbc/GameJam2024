using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBall : Projectile
{
    [SerializeField] private GameObject target;
    [SerializeField] private float Curvature; //A good value for this is about 5
    void FixedUpdate()
    {
        Vector2 NormalToTarget = new Vector2(target.transform.position.x-rb.position.x, target.transform.position.y-rb.position.y).normalized; //Measures normal vector to target
        rb.velocity = new Vector2(rb.velocity.x + NormalToTarget.x * Curvature * Time.fixedDeltaTime, rb.velocity.y + NormalToTarget.y * Curvature * Time.fixedDeltaTime);
    }
}
