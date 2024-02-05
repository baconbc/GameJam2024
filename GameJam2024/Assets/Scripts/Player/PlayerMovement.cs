using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : IMovement
{
    [SerializeField] private int speed = 5;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public void setSpeed(int speed)
    {
        this.speed = speed;
    }

    private void OnToggleInventory(InputValue value)
    {
        Debug.Log("toggle inv");
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime); // use fixedDeltaTime instead of deltaTime cuz in FixedUpdate() not Update()
    }
}
