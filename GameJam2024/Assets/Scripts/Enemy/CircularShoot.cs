using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CircularShoot : MonoBehaviour
{
    [SerializeField] private PlayerObject pr;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootFrequency;
    [SerializeField] private float numProjectiles;
    [SerializeField] private float angleOffset;

    private float timer;
    private float angleBetweenProjectiles;
    private Collider2D col;

    // Start is called before the first frame update
    void Awake()
    {
        col = GetComponent<Collider2D>();
        angleBetweenProjectiles = 360 / numProjectiles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > shootFrequency)
        {
            for (int i = 0; i < numProjectiles; i++)
            {
                float rotation = i * angleBetweenProjectiles + angleOffset;
                Vector3 playerDirection = (Vector3)pr.Position - col.transform.position;
                Vector3 projectileSpawn = col.transform.position + playerDirection.normalized * 0.75f;
                Instantiate(projectile, projectileSpawn, Quaternion.Euler(0, 0, rotation));
            }
            timer = 0;
        }
    }
}
