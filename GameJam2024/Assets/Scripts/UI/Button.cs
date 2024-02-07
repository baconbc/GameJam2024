using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject parryIcon;
    private TMPro.TextMeshProUGUI text;
    private Color defaultTextColor;

    private Image image;
    private Sprite buttonSprite;
    [SerializeField] private Sprite clickedSprite;

    private void Awake()
    {
        parryIcon = transform.GetChild(0).gameObject;

        text = transform.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        defaultTextColor = text.color;

        image = GetComponent<Image>();
        buttonSprite = image.sprite;
    }

    void Start()
    {
        ResetClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = new Color(defaultTextColor.r, defaultTextColor.g, defaultTextColor.b, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = defaultTextColor;
    }

    public void Click()
    {
        image.sprite = clickedSprite;
        text.color = new Color(0.655f, 0.969f, 1f, 1f);
        parryIcon.SetActive(true);
    }

    public void ResetClick()
    {
        image.sprite = buttonSprite;
        text.color = defaultTextColor; 
        parryIcon.SetActive(false);
    }
}
