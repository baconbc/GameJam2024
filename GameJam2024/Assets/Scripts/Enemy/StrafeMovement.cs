using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeMovement : MonoBehaviour
{
    [SerializeField] private PlayerObject player;
    [SerializeField] private float speed;
    [SerializeField] private float moveDuration;
    [SerializeField] private float stillDuration;

    private Rigidbody2D rb;
    private float timer;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (isMoving && timer > moveDuration)
        {
            // Stand still;
            rb.velocity = new Vector2(0, 0);
            isMoving = false;
            timer = 0;
        }
        else if (!isMoving && timer > stillDuration)
        {
            // Randomly move in one of two perpendicular directions to enemy
            Vector2 toPlayerDirection = transform.position - (Vector3)player.Position;

            Vector2 movementDirection;
            float r = Random.Range(0f, 1f);
            if (r > 0.5f)
            {
                movementDirection = new Vector2(toPlayerDirection.y, -toPlayerDirection.x);
            }
            else
            {
                movementDirection = new Vector2(-toPlayerDirection.y, toPlayerDirection.x);
            }

            rb.velocity = movementDirection.normalized * speed;
            isMoving = true;
            timer = 0;
        }
    }
}