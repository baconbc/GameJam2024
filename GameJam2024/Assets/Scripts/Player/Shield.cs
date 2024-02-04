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
    private Vector2 mousePos;

    // Parry
    [SerializeField] private float parryCooldown; // 1 sec cooldown so that players cannot spam click and must time parry right
    private bool cooldown = false;
    private bool parrying = false;
    [SerializeField] private float maxParryTime = 0.1f; // how long player has to parry something
    private float parryTimer;

    // Audio
    AudioSource audio;
    public AudioClip shieldHitAudio;
    public AudioClip shieldReflectAudio;
    [SerializeField] private float slowdownTime;
    [SerializeField] private float timer;
    private bool slowdownCheck = false;
    [SerializeField] private bool doSlowdown;

    private void Awake()
    {
        shieldInput = new PlayerInput();
        shieldInput.InGame.ShieldParry.started += OnShieldParry;
        OnEnable();
        audio = GetComponent<AudioSource>();
        timer = slowdownTime;
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
        if (mousePos != pr.MousePos)
        {
            //For timer
            /*if (slowdownCheck == true)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    slowdownCheck = false;
                    timer = slowdownTime;
                    Time.timeScale = 1f;

                }
            }*/

            mousePos = pr.MousePos;
            Move();
        }

        if (parrying)
        {
            if (parryTimer < maxParryTime)
                parryTimer += Time.fixedDeltaTime;
            else
            {
                parryTimer = 0f;
                parrying = false;
                cooldown = true;
            }
        }

        if (cooldown)
        {
            if (parryTimer < parryCooldown)
                parryTimer += Time.fixedDeltaTime;
            else
            {
                parryTimer = 0f;
                cooldown = false;
            }
        }
    }

    public void OnShieldParry(InputAction.CallbackContext context)
    {
        if (!cooldown)
        {
            parrying = true;
        }
    }

    public void Move()
    {
        Vector2 direction = (mousePos - pr.Position).normalized;
        Vector2 pos = pr.Position + (direction * radius);

        // Calculate the angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.SetPositionAndRotation(pos, Quaternion.Euler(new Vector3(0f, 0f, angle + 180f)));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Projectile")
        {
            if (parrying)
            {
                Parry(collision);
            }
            else
            {
                Block(collision);
            }
        }
    }

    private void Parry(Collision2D collision)
    {
        //Change this later when the parry is in place
        /*if (doSlowdown == true)
        {
            Time.timeScale = 0.2f;
            slowdownCheck = true;
        }*/

        collision.gameObject.GetComponent<Projectile>().ReturnToSender(transform, collision);
        AudioManager.Instance.Play("ShieldReflect");
    }

    private void Block(Collision2D collision)
    {
        Destroy(collision.gameObject);
        AudioManager.Instance.Play("ShieldHit");
    }
}
