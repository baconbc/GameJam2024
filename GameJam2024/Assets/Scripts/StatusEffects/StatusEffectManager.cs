using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private List<IStatusEffect> effects = new List<IStatusEffect>();


    public void AddEffect(StatusEffectType e)
    {
        if (e == StatusEffectType.Burn)
        {
            Debug.Log("adding burn");
            effects.Add(new Burn(gameObject));
        } 
        else if (e == StatusEffectType.Freeze)
        {
            effects.Add(new Freeze(gameObject));
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = effects.Count - 1; i >= 0; i--)
        {
            IStatusEffect e = effects[i];
            e.Apply(Time.fixedDeltaTime);
            if (e.IsFinished())
            {
                effects.RemoveAt(i);
                // Only remove status effect if it's last of it's type so effects can stack
                if (IsLastEffectOfType(e))
                {
                    e.Remove();
                }
            }
        }
    }

    private bool IsLastEffectOfType(IStatusEffect e)
    {
        foreach (IStatusEffect effect in effects)
        {
            if (effect.Type() == e.Type()) { return false; }
        }
        return true;
    }
}
