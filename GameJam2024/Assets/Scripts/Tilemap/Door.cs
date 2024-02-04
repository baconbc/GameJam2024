using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Sprite doorOpen;
    [SerializeField] private Sprite doorClosed;

    private BoxCollider2D collider;
    private bool isClosed = false;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        doorOpen = sr.sprite;
        collider = GetComponent<BoxCollider2D>();
    }

    public void CloseDoor()
    {
        sr.sprite = doorClosed;
        collider.enabled = true;

        isClosed = true;
    }

    public void OpenDoor()
    {
        sr.sprite = doorOpen;
        collider.enabled = true;

        isClosed = false;
    }
}
