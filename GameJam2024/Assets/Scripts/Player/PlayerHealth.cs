using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerObject pr; 
    [SerializeField] private int maxHealth;

    private void Awake()
    {
        pr.Health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        pr.Health -= damage;
        if (pr.Health <= 0)
        {
            pr.Health = 0;
            Die();
        }
        Signal signal = GameSignals.UpdatePlayerHealth;
        signal.Dispatch();
    }

    private void Die()
    {
        print("you died! restarting the level");
        pr.GameObject.transform.position = pr.SpawnPoint;

        ResetHealth();
    }

    public void ResetHealth()
    {
        pr.Health = maxHealth;

        Signal signal = GameSignals.UpdatePlayerHealth;
        signal.Dispatch();
    }
}
