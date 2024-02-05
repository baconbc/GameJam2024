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
    private SpriteRenderer sr;

    public Freeze(GameObject obj)
    {
        movement = obj.GetComponent<IMovement>();
        rb = obj.GetComponent<Rigidbody2D>();
        sr = obj.GetComponent<SpriteRenderer>();

        rb.velocity = Vector3.zero;
        if (movement != null)
        {
            movement.enabled = false;
        }
        sr.color = new Color(0.1f, 0.1f, 0.5f);
    }

    public StatusEffectType Type()
    {
        return StatusEffectType.Freeze;
    }

    public void Reset()
    {
        timer = 0;
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
        sr.color = new Color(1.0f, 1.0f, 1.0f);
    }

    public bool IsFinished()
    {
        return finished;
    }
}
