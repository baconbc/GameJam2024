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

    // Start is called before the first frame update
    void Awake()
    {
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
                Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, rotation));
            }
            timer = 0;
        }
    }
}
