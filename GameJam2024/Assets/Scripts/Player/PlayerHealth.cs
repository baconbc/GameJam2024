using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : IHealth
{
    [SerializeField] private PlayerObject pr;
    [SerializeField] private int regenAmount;

    public override void Awake()
    {
        base.Awake();
        GameSignals.ResetPlayerHealth.AddListener(ResetPlayerHealth);
        GameSignals.RegenPlayerHealth.AddListener(RegenPlayerHealth);
    }

    private void OnDestroy()
    {
        GameSignals.ResetPlayerHealth.RemoveListener(ResetPlayerHealth);
        GameSignals.RegenPlayerHealth.RemoveListener(RegenPlayerHealth);
    }

    protected override void Die()
    {
        print("you died! restarting the level");
        pr.GameObject.transform.position = pr.SpawnPoint;

        Signal signal = GameSignals.PlayerDeath;
        signal.Dispatch();
        ResetHealth();
        sem.RemoveStatusEffect();
        AudioManager.Instance.Play("Die", "player");
    }

    protected override void OnSetHealth()
    {
        pr.Health = GetHealth();
        Signal signal = GameSignals.UpdatePlayerHealth;
        signal.Dispatch();
    }

    protected override bool ShouldTakeDamage(Projectile p)
    {
        return true;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        AudioManager.Instance.Play("Damage", "player");
    }

    private void ResetPlayerHealth(ISignalParameters parameters)
    {
        ResetHealth();
    }

    public void RegenPlayerHealth(ISignalParameters parameters)
    {
        int h = GetHealth();
        int m = GetMaxHealth();
        if (h < m)
        {
            h += regenAmount;
            if (h > m) h = GetMaxHealth();
            SetHealth(h);
            print(GetHealth());
        }
    }
}
