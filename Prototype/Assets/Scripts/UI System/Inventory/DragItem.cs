using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Image image;
    public Sprite chip, equippedChip;
    private Vector3 defaultPosition;
    [HideInInspector] public bool droppedOnSlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        defaultPosition = GetComponent<RectTransform>().localPosition;
        image = GetComponent<Image>();
        image.sprite = chip;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        
        eventData.pointerDrag.GetComponent<DragItem>().droppedOnSlot = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        StartCoroutine("Return");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void ChangeToEquippedChipSprite()
    {
        if (image.sprite == chip)
        {
            image.sprite = equippedChip;
        }
    }

    IEnumerator Return()
    {
        yield return new WaitForEndOfFrame();
        if (droppedOnSlot == false)
        {
            rectTransform.anchoredPosition = defaultPosition;
            if (image.sprite == equippedChip)
            {
                image.sprite = chip;
            }
        }
    }
}
