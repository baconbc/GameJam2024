using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHealth : MonoBehaviour 
{
    [SerializeField] private int maxHealth;
    private int health;
    protected StatusEffectManager sem;
    private Transform parentT;

    [Header("Stretch Sprite Vars")]
    [SerializeField] private float stretchAmount = 1.1f;
    [SerializeField] private float maxStretchTime = 0.3f;
    private float stretchTimer;
    private bool stretching = false;
    private Vector2 stretchScale;
    private Vector2 defaultScale;

    public virtual void Awake()
    {
        SetHealth(maxHealth);
        sem = transform.parent.GetComponent<StatusEffectManager>();

        parentT = transform.parent.transform;
        defaultScale = parentT.localScale;
        stretchScale = new Vector2 { x = defaultScale.x, y = defaultScale.y * stretchAmount };
    }

    private void FixedUpdate()
    {
        if (stretching)
        {
            if (stretchTimer >= maxStretchTime)
            {
                stretching = false;
                stretchTimer = 0f;
                ScaleSprite(defaultScale);
            }
            else
                stretchTimer += Time.fixedDeltaTime;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    protected void SetHealth(int value)
    {
        health = value;
        OnSetHealth();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("is projectile");
            Projectile p = collision.gameObject.GetComponent<Projectile>();
            if (ShouldTakeDamage(p))
            {
                Debug.Log("hit by bullet");
                sem.AddEffect(p.effectType);
                TakeDamage(p.GetDamage());
                Destroy(p.gameObject);
            }
        }
    }


    public virtual void TakeDamage(int damage)
    {
        SetHealth(health - damage);
        if (health <= 0)
        {
            Die();
        }
        else
        {
            stretching = true;
            stretchTimer = 0f;
            ScaleSprite(stretchScale);
        }
    }

    private void ScaleSprite(Vector2 newScale)
    {
        Vector3 scale = parentT.localScale;
        scale.Set(newScale.x, newScale.y, 1f);

        parentT.localScale = scale;
    }

    public void ResetHealth()
    {
        SetHealth(maxHealth);
    }

    // Called when health reaches zero
    protected abstract void Die();
    // Called whenever health is set
    protected abstract void OnSetHealth();

    protected abstract bool ShouldTakeDamage(Projectile p);
}
