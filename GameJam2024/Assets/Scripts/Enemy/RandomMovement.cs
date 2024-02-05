using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : IMovement
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float wanderDuration;

    private Rigidbody2D rb;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = RandomDirection() * maxSpeed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        Move();
    }

    protected override void Move()
    {
        if (timer > wanderDuration)
        {
            rb.velocity = RandomDirection() * maxSpeed;
            timer = 0;
        }
    }

    private Vector2 RandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        return new Vector2(x, y).normalized;
    }
}
