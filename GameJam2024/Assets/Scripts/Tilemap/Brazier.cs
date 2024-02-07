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

        if (islit)
        {
            Wind();
            Water();
            Fire();
            Earth();
        }
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
            print("You hear the sound of wind");
            Wind();
        }
    }

    private void Wind()
    {
        if (element == "wind")
        {
            islit = true;
            animator.SetFloat("earth-wind", 1f);
            animator.SetBool("isLit", true);
        }
    }
    private void Water(ISignalParameters parameters)
    {
        if (element == "water" && !islit)
        {
            print("You hear the sound of water");
            Water();
        }
    }

    private void Water()
    {
        if (element == "water")
        {
            islit = true;
            animator.SetFloat("water-fire", 1f);
            animator.SetBool("isLit", true);
        }
    }

    private void Fire(ISignalParameters parameters)
    {
        if (element == "fire" && !islit)
        {
            print("You hear the sound of fire");
            Fire();
        }
    }

    private void Fire()
    {
        if (element == "fire")
        {
            islit = true;
            animator.SetFloat("water-fire", -1f);
            animator.SetBool("isLit", true);
        }
    }

    private void Earth(ISignalParameters parameters)
    {
        if (element == "earth" && !islit)
        {
            print("You hear the sound of earth");
            Earth();
        }
    }

    private void Earth()
    {
        if (element == "earth")
        {
            islit = true;
            animator.SetFloat("earth-wind", -1f);
            animator.SetBool("isLit", true);
        }
    }
}
