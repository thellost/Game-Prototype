using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class EraseUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    public Augment augment;
    static private GameObject _augmentPrefab;
    //ini mengatur eraser yang dipakai
    private Eraser selectedAugment;
    private RectTransform rectTransform;
    private RectTransform CanvasrectTransform;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        CanvasrectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        _augmentPrefab = GameObject.Find("AugmentDragIcon");

        Debug.Log("AWAKE");
    }
    public void OnBeginDrag(PointerEventData eventData)

    {
        Debug.Log("OnBeginDrag");

        
        if (_augmentPrefab == null)
        {
            _augmentPrefab = GameObject.Find("AugmentDragIcon");
        }
        if (_augmentPrefab != null)
        {
            _augmentPrefab.SetActive(true);
            rectTransform = _augmentPrefab.GetComponent<RectTransform>();
            selectedAugment = _augmentPrefab.GetComponent<Eraser>();
            _augmentPrefab.GetComponent<Image>().overrideSprite = augment.artwork;
            
            //rectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            selectedAugment.ToggleOrderInLayer(true);
            rectTransform.anchoredPosition = new Vector2 (eventData.position.x - CanvasrectTransform.anchoredPosition.x , eventData.position.y - CanvasrectTransform.anchoredPosition.y);
            Debug.Log(CanvasrectTransform.anchoredPosition.x);
            Debug.Log(CanvasrectTransform.anchoredPosition.y);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("OnPointerDown");
    }

    // Implementasi dari Interface IDragHandler

    // Fungsi ini terpanggil selama men-drag UI

    public void OnDrag(PointerEventData eventData)

    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        Debug.Log("OnDRAG");

    }
    // Implementasi dari Interface IEndDragHandler

    // Fungsi ini terpanggil sekali ketika men-drop UI ini

    public void OnEndDrag(PointerEventData eventData)

    {

        Debug.Log("OnEndDrag");
        if (selectedAugment.ToBeErased == null)

        {

            _augmentPrefab.SetActive(false);

        }

        else

        {


            selectedAugment.ToggleOrderInLayer(false);

            //hapus object tower yang telah di tentukan
            selectedAugment.EraseTower();



            //hapus object setelah selesai di gunakan
            _augmentPrefab.SetActive(false);

        }

    }
}
