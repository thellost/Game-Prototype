using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    private Augment augment;
    [SerializeField] SpriteRenderer eraserSprite;

    //menyimpan object yang mau di erase
    public Tower ToBeErased { get; private set; }

    public void ToggleOrderInLayer(bool toFront)

    {

        int orderInLayer = toFront ? 2 : 0;

        eraserSprite.sortingOrder = orderInLayer;


    }
    public void SetErase(Tower newObject)

    {

        ToBeErased = newObject;

    }

    public void EraseTower()
    {
        ToBeErased.EraseThisGameObject();
    }
}
