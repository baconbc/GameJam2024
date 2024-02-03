using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerObject pr;

    private void Awake()
    {
        pr.GameObject = gameObject;
        pr.SpawnPoint = transform.position;
    }

    private void Update()
    {
        pr.MousePos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        pr.Position = transform.position;
    }
}
