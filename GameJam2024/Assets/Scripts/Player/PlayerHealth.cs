using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : IHealth
{
    [SerializeField] private PlayerObject pr;

    protected override void Die()
    {
        print("you died! restarting the level");
        pr.GameObject.transform.position = pr.SpawnPoint;

        Signal signal = GameSignals.PlayerDeath;
        signal.Dispatch();

        ResetHealth();
    }

    protected override void OnSetHealth()
    {
        pr.Health = GetHealth();
        Signal signal = GameSignals.UpdatePlayerHealth;
        signal.Dispatch();
    }
}
