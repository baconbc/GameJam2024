using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralWave : Projectile
{
    [SerializeField] private float SpiralStrength;
    [SerializeField] private float SpiralLength; //Longer value for longer cycle of time before spiral starts shrinking again
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector2 CurrentDirection = rb.velocity.normalized;
        Vector2 Perpendicular = new Vector2(-CurrentDirection.y, CurrentDirection.x);
        Vector2 Variation = Mathf.Sin(Time.time / SpiralLength) * Perpendicular;
        rb.velocity = (rb.velocity.normalized + SpiralStrength * Variation).normalized * speed;
    }
}
