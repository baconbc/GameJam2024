using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_Noclip : MonoBehaviour
{
    [SerializeField] private bool Noclip = false;
    private BoxCollider2D collider;

    internal PlayerMovement pm;

    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            if(Noclip) Noclip = false;
            else if (!Noclip) Noclip = true;
            print("Toggled Noclip");
        }
        if(Noclip)
        {
            collider.enabled = false;
            pm.setSpeed(10);
        }
        else
        {
            collider.enabled = true;
            pm.setSpeed(5);
        }
    }
}
