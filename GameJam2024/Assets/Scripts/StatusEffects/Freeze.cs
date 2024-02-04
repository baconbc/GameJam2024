using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Freeze : IStatusEffect
{
    private float timer;
    private float duration = 1.5f;
    private bool finished = false;
    private IMovement movement;
    private Rigidbody2D rb;

    public Freeze(GameObject obj)
    {
        movement = obj.GetComponent<IMovement>();
        rb = obj.GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.zero;
        movement.enabled = false;
    }

    public StatusEffectType Type()
    {
        return StatusEffectType.Freeze;
    }

    public void Apply(float elapsedTime)
    {
        timer += elapsedTime;
        if (timer >= duration)
        {
            finished = true;
        }
    }

    public void Remove()
    {
        Debug.Log("unfreeze");
        movement.enabled = true;
    }

    public bool IsFinished()
    {
        return finished;
    }
}
