using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : IHealth
{
    [SerializeField] private PlayerObject pr;

    public override void Awake()
    {
        base.Awake();
        GameSignals.ResetPlayerHealth.AddListener(ResetPlayerHealth);
    }

    private void OnDestroy()
    {
        GameSignals.ResetPlayerHealth.RemoveListener(ResetPlayerHealth);
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
}
