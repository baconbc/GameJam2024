using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : IStatusEffect
{
    private float timer;
    private float tickSpeed = 0.5f;
    private float duration = 4.001f;
    private int numTicks;
    private int damagePerTick = 1;
    private bool finished = false;
    private SpriteRenderer sr;
    private IHealth healthComponent;


    public Burn(GameObject obj)
    {
        numTicks = (int) (duration / tickSpeed);
        Debug.Log(numTicks);
        healthComponent = obj.GetComponentInChildren<IHealth>();
        sr = obj.GetComponent<SpriteRenderer>();
        sr.color = new Color(1.0f, 0.1f, 0.1f);
    }

    public StatusEffectType Type()
    {
        return StatusEffectType.Burn;
    }

    public void Reset()
    {
        timer = 0;
    }

    public void Apply(float elapsedTime)
    {
        timer += elapsedTime;

        if (timer >= tickSpeed)
        {
            healthComponent.TakeDamage(damagePerTick);
            numTicks -= 1;
            timer -= tickSpeed;
            if (numTicks == 0)
            {
                finished = true;
            }
        }        
    }

    public void Remove()
    {
        sr.color = new Color(1f, 1f, 1f);
    }

    public bool IsFinished()
    {
        return finished;
    }
}
