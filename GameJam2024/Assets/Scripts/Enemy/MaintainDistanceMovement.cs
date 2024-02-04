using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainDistanceMovement : IMovement
{
    [SerializeField] private PlayerObject player;
    [SerializeField] private float distance;
    [SerializeField] private float maxSpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        Vector3 fromPlayerDirection = transform.position - (Vector3)player.Position;
        Vector3 pointFromPlayer = (Vector3)player.Position + fromPlayerDirection.normalized * distance;
        Vector3 directionToPoint = pointFromPlayer - transform.position;
        float distanceToPoint = Vector3.Distance(pointFromPlayer, transform.position);
        float speed = Mathf.Min(distanceToPoint, maxSpeed);
        rb.velocity = directionToPoint.normalized * Mathf.Min(distanceToPoint, maxSpeed);
    }
}
