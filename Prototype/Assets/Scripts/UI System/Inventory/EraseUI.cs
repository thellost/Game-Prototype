using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class EraseUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Augment augment;
    [SerializeField] private Eraser _eraserPrfab;
    //ini mengatur eraser yang dipakai
    private Eraser CurrentEraser;
    public void OnBeginDrag(PointerEventData eventData)

    {

        GameObject newTowerObj = Instantiate(_eraserPrfab.gameObject);

        CurrentEraser = newTowerObj.GetComponent<Eraser>();

        CurrentEraser.ToggleOrderInLayer(true);

    }

    // Implementasi dari Interface IDragHandler

    // Fungsi ini terpanggil selama men-drag UI

    public void OnDrag(PointerEventData eventData)

    {

        Camera mainCamera = Camera.main;

        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = -mainCamera.transform.position.z;

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);



        CurrentEraser.transform.position = targetPosition;

    }
    // Implementasi dari Interface IEndDragHandler

    // Fungsi ini terpanggil sekali ketika men-drop UI ini

    public void OnEndDrag(PointerEventData eventData)

    {

        if (CurrentEraser.ToBeErased == null)

        {

            Destroy(CurrentEraser.gameObject);

        }

        else

        {


            CurrentEraser.ToggleOrderInLayer(false);

            //hapus object tower yang telah di tentukan
            CurrentEraser.EraseTower();
            


            //hapus object setelah selesai di gunakan
            Destroy(CurrentEraser.gameObject);

        }

    }
}
