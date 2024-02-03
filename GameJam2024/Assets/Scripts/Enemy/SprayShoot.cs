using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayShoot : MonoBehaviour
{
    [SerializeField] private PlayerObject player;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootFrequency;
    [SerializeField] private float sprayFrequency; // Amount of time between bullets fired in spray
    [SerializeField] private float numProjectiles; // Number of projectiles fired in spray
    [SerializeField] private float sprayDegrees;   // Number of degrees of spray fan (if 20, shoots from +20 to -20 degree angle around player)

    private float timer;
    private Rigidbody2D rb;
    private bool isShooting;
    private int bulletsFired;
    private float sprayAngleDiff;
    private float firstSprayAngle;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprayAngleDiff = (sprayDegrees * 2) / (numProjectiles - 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (!isShooting && timer > shootFrequency)
        {
            isShooting = true;
        }

        if (isShooting && timer > sprayFrequency)
        {
            if (bulletsFired == 0)
            {
                Vector3 direction = (Vector3)player.Position - transform.position;
                float rotation = (Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg) + 90;  // Add 90 degrees, since bullet starts facing up
                rotation += sprayDegrees;
                firstSprayAngle = rotation;
            }

            float projectileRotation = firstSprayAngle - (bulletsFired * sprayAngleDiff);
            Instantiate(projectile, rb.position, Quaternion.Euler(0, 0, projectileRotation));
            bulletsFired += 1;
            timer = 0;

            if (bulletsFired == numProjectiles)
            {
                bulletsFired = 0;
                isShooting = false;
            }

        }

    }
}
