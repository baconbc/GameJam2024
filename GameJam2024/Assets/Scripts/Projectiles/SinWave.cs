using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SinWave : Projectile
{
    [SerializeField] private Vector2 MainDirection;
    [SerializeField] private float Amplitude; //Determines Amplitude of sin wave
    [SerializeField] private float TimeScale; //Determines how fast each phase moves
    
    public override void Start()
    {
        base.Start();

        MainDirection = rb.velocity.normalized;
    }
    
    public override void FixedUpdate() //Overall idea is for t to traverse 2pi radians every 3 seconds
    {
        base.FixedUpdate();
        Vector2 PerpendicularDirection = new Vector2(-MainDirection.y, MainDirection.x);
        Vector2 Variation = PerpendicularDirection * Mathf.Sin(Time.time * TimeScale) * Amplitude; //Creates a perpendicular vector that variates around the primary direction
        rb.velocity = MainDirection * speed + Variation;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        MainDirection = rb.velocity.normalized;
    }

    public override void ReturnToSender(Transform shield, Collision2D collision)
    {
        base.ReturnToSender(shield, collision);

        MainDirection = rb.velocity.normalized;
    }
}
