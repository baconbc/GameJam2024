using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    StatusEffectType Type();

    void Apply(float elapsedTime);

    void Remove();

    Boolean IsFinished();
}
