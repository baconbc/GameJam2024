using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainDistanceMovement : MonoBehaviour
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
    void FixedUpdate()
    {
        Vector3 fromPlayerDirection = transform.position - (Vector3)player.Position;
        Vector3 pointFromPlayer = (Vector3)player.Position + fromPlayerDirection.normalized * distance;
        Debug.Log($"pointFromPlayer: {pointFromPlayer}");
        Vector3 directionToPoint = pointFromPlayer - transform.position;
        float distanceToPoint = Vector3.Distance(pointFromPlayer, transform.position);
        Debug.Log(distanceToPoint);
        float speed = Mathf.Min(distanceToPoint, maxSpeed);
        Debug.Log(speed);
        rb.velocity = directionToPoint.normalized * Mathf.Min(distanceToPoint, maxSpeed);
    }
}
