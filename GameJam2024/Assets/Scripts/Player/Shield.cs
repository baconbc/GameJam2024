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
    private Animator parryAnim;
    private bool hasPlayedAnim = false;

    // Cursor
    [SerializeField] private Texture2D cursorParry;
    [SerializeField] private Texture2D cursorCanParry;
    [SerializeField] private Texture2D cursorCantParry;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    // Audio
    //public AudioClip shieldHitAudio;
    //public AudioClip shieldReflectAudio;
    [SerializeField] private float slowdownTime;
    [SerializeField] private float timer;
    private bool slowdownCheck = false;
    [SerializeField] private bool doSlowdown;

    private void Awake()
    {
        shieldInput = new PlayerInput();
        shieldInput.InGame.ShieldParry.started += OnShieldParry;
        OnEnable();
        //audio = GetComponent<AudioSource>();
        timer = slowdownTime;
        parryAnim = transform.GetChild(0).GetComponent<Animator>();
        Cursor.SetCursor(cursorCanParry, hotSpot, cursorMode);
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
                hasPlayedAnim = false;
                Cursor.SetCursor(cursorCantParry, hotSpot, cursorMode);
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
                Cursor.SetCursor(cursorCanParry, hotSpot, cursorMode);
            }
        }
    }

    public void OnShieldParry(InputAction.CallbackContext context)
    {
        if (!cooldown)
        {
            parrying = true;
            Cursor.SetCursor(cursorParry, hotSpot, cursorMode);
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
        //Debug.Log("shield hit");
        //Debug.Log(other.name);

        if (other.tag == "Projectile")
        {
            if (parrying)
            {
                Parry(collision);
                if (!hasPlayedAnim)
                {
                    parryAnim.SetTrigger("PlayOnce");
                    hasPlayedAnim = true;
                }

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
        //Debug.Log("Parrying");
        collision.gameObject.GetComponent<Projectile>().ReturnToSender(transform, collision);
        AudioManager.Instance.Play("ShieldReflect", "player");
    }

    private void Block(Collision2D collision)
    {
        Destroy(collision.gameObject);
        AudioManager.Instance.Play("ShieldHit", "player");
    }
}
