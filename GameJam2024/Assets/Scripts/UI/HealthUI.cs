using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerObject pr;
    Image fill;

    void Awake()
    {
        GameSignals.UpdatePlayerHealth.AddListener(UpdateHealth);
        fill = transform.GetChild(1).GetComponent<Image>();
    }

    private void Start()
    {
        UpdateHealth();
    }

    void OnDestroy()
    {
        GameSignals.UpdatePlayerHealth.RemoveListener(UpdateHealth);
    }

    private void UpdateHealth(ISignalParameters parameters)
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        float health = (float)pr.Health / (float)pr.MaxHealth;
        fill.fillAmount = health;
    }
}
