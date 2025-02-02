using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitProjectile : Projectile
{
    [SerializeField] GameObject splitProjectile;
    [SerializeField] float timeToSplit;
    [SerializeField] int numSplitProjectiles;
    [SerializeField] float splitAngleDiff; // Degrees between each split projectile;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (timer > timeToSplit)
        {
           
            float rotation = (Mathf.Atan2(-rb.velocity.y, -rb.velocity.x) * Mathf.Rad2Deg) + 90;  // Add 90 degrees, since bullet starts facing up
            rotation -= (numSplitProjectiles - 1) * splitAngleDiff / 2;
            for (int i = 0; i < numSplitProjectiles; i++)
            {
                Instantiate(splitProjectile, transform.position, Quaternion.Euler(0, 0, rotation + i*splitAngleDiff));
            }
            Destroy(gameObject);
        }
    }
}
