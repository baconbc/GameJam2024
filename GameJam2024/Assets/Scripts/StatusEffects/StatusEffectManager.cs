using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private IStatusEffect currentEffect;

    public void AddEffect(StatusEffectType e)
    {
        if (currentEffect == null)
        {
            if (e == StatusEffectType.Burn)
            {
                currentEffect = new Burn(gameObject);
            } 
            if (e == StatusEffectType.Freeze)
            {
                currentEffect = new Freeze(gameObject);
            }
        }
        else if (e == currentEffect.Type())
        {
            currentEffect.Reset();
        }
        else
        {
            RemoveStatusEffect();
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentEffect != null)
        {
            currentEffect.Apply(Time.fixedDeltaTime);
            if (currentEffect.IsFinished())
            {
                RemoveStatusEffect();
            }
        }
    }

    public void RemoveStatusEffect()
    {
        currentEffect.Remove();
        currentEffect = null;
    }
}
