using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerObject pr;
    private TMPro.TextMeshProUGUI tmpro;

    void Awake()
    {
        GameSignals.UpdatePlayerHealth.AddListener(UpdateHealth);
        tmpro = GetComponent<TMPro.TextMeshProUGUI>();
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
        tmpro.text = pr.Health.ToString();
    }
}
