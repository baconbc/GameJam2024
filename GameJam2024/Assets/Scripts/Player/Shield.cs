using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shield : MonoBehaviour
{
    [SerializeField] private PlayerObject pr;

    // Movement
    [SerializeField] private float radius;

    // Input
    private PlayerInput shieldInput;
    AudioSource audio;

    private void Awake()
    {
        shieldInput = new PlayerInput();
        shieldInput.InGame.ShieldParry.started += OnShieldParry;
        OnEnable();
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        shieldInput.Enable();
    }

    private void OnDisable()
    {
        shieldInput.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnShieldParry(InputAction.CallbackContext context)
    {
        audio.Play(0);
        print("parry");
    }

    public void Move()
    {
        Vector2 direction = (pr.MousePos - pr.Position).normalized;
        Vector2 pos = (pr.Position += new Vector2(0, 0.25f)) + (direction * radius);

        // Calculate the angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.SetPositionAndRotation(pos, Quaternion.Euler(new Vector3(0f, 0f, angle + 180f)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Projectile")
        {
            //Change this later when the parry is in place
            audio.Play(0);
            other.GetComponent<Projectile>().ReturnToSender(transform);
        }
    }
}
