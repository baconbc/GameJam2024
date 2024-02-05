using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerObject pr;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootFrequency;

    [SerializeField] private float timer; // what to start timer as, default to zero
    //private Collider2D cd;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //cd = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer > shootFrequency)
        {
            timer = 0;
            Shoot();
        }
    }


    private void Shoot()
    {
        Vector2 direction = (Vector3)pr.Position - transform.position;
        float rotation = (Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg) + 90;  // Add 90 degrees, since bullet starts facing up
        Vector3 projectileStart = rb.position + 0.1f*direction.normalized;
        Instantiate(projectile, projectileStart, Quaternion.Euler(0, 0, rotation));
    }


}
