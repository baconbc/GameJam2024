using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    [SerializeField] private List<Sprite> bloodPatterns;

    [SerializeField] private float disappearAfter = 7;
    private float opacity = 1.0f;
    private float timer;
    private SpriteRenderer sr;


    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        int bloodIndex = Random.Range(0, bloodPatterns.Count);
        sr.sprite = bloodPatterns[bloodIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > disappearAfter)
        {
            opacity -= 0.001f;
            sr.color = new Color(1.0f, 1.0f, 1.0f, opacity);
            if (opacity <= 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }
}
