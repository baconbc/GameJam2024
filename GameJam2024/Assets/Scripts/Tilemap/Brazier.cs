using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier : MonoBehaviour
{
    [SerializeField] string element;
    [SerializeField] Sprite lit;
    private SpriteRenderer sr;
    public bool islit = false;

    // Start is called before the first frame update
    void Awake()
    {
        GameSignals.Fire.AddListener(Fire);
        GameSignals.Earth.AddListener(Earth);
        GameSignals.Water.AddListener(Water);
        GameSignals.Wind.AddListener(Wind);
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnDestroy()
    {
        GameSignals.Fire.RemoveListener(Fire);
        GameSignals.Earth.RemoveListener(Earth);
        GameSignals.Water.RemoveListener(Water);
        GameSignals.Wind.RemoveListener(Wind);
    }

    private void Wind(ISignalParameters parameters)
    {
        if (element == "wind" && !islit)
        {
            sr.sprite = lit;
            islit = true;
        }
    }
    private void Water(ISignalParameters parameters)
    {
        if (element == "water" && !islit)
        {
            sr.sprite = lit;
            islit = true;
        }
    }

    private void Fire(ISignalParameters parameters)
    {
        if (element == "fire" && !islit)
        {
            sr.sprite = lit;
            islit = true;
        }
    }

    private void Earth(ISignalParameters parameters)
    {
        if (element == "earth" && !islit)
        {
            sr.sprite = lit;
            islit = true;
        }
    }
}
