using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public Vector2? PlacePosition { get; private set; }
    // Tower Component

    [SerializeField] private SpriteRenderer _towerPlace;

    [SerializeField] private SpriteRenderer _towerHead;

    // Tower Properties

    [SerializeField] private int _shootPower = 1;

    [SerializeField] private float _shootDistance = 1f;

    [SerializeField] private float _shootDelay = 5f;

    [SerializeField] private float _bulletSpeed = 1f;

    [SerializeField] private float _bulletSplashRadius = 0f;


    // Digunakan untuk menyimpan posisi yang akan ditempati selama tower di drag


    private float _runningShootDelay;

    private Quaternion _targetRotation;

    public void SetPlacePosition(Vector2? newPosition)

    {

        PlacePosition = newPosition;

    }



    // Mengecek musuh terdekat

    
    //ini akan menghapus object
    public void EraseThisGameObject()
    {

        InventoryManager.instance.DequipThisItem(this.gameObject);
        Destroy(this.gameObject);

    }

    public void LockPlacement()

    {

        transform.position =(Vector2) PlacePosition;

    }


    // Mengubah order in layer pada tower yang sedang di drag

    public void ToggleOrderInLayer(bool toFront)

    {

        int orderInLayer = toFront ? 2 : 0;

        _towerPlace.sortingOrder = orderInLayer;

        _towerHead.sortingOrder = orderInLayer;

    }


    // Fungsi yang digunakan untuk mengambil sprite pada Tower Head

    public Sprite GetTowerHeadIcon()

    {

        return _towerHead.sprite;

    }





}

