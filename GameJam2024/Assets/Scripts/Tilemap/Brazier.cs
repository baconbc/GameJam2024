using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier : MonoBehaviour
{
    [SerializeField] string element;
    private SpriteRenderer sr;
    public bool islit = false;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        GameSignals.Fire.AddListener(Fire);
        GameSignals.Earth.AddListener(Earth);
        GameSignals.Water.AddListener(Water);
        GameSignals.Wind.AddListener(Wind);
        sr = gameObject.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
            islit = true;
            print("You hear the sound of wind");
            animator.SetFloat("earth-wind", 1f);
            animator.SetBool("isLit", true);
        }
    }
    private void Water(ISignalParameters parameters)
    {
        if (element == "water" && !islit)
        {
            islit = true;
            print("You hear the sound of water");
            animator.SetFloat("water-fire", 1f);
            animator.SetBool("isLit", true);
        }
    }

    private void Fire(ISignalParameters parameters)
    {
        if (element == "fire" && !islit)
        {
            islit = true;
            print("You hear the sound of fire");
            animator.SetFloat("water-fire", -1f);
            animator.SetBool("isLit", true);
        }
    }

    private void Earth(ISignalParameters parameters)
    {
        if (element == "earth" && !islit)
        {
            islit = true;
            print("You hear the sound of earth");
            animator.SetFloat("earth-wind", -1f);
            animator.SetBool("isLit", true);
        }
    }
}
