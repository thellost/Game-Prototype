using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    private Tower _placedTower;

    //variabel ini bakal double check untuk mengetahui apakah udah ada tower di place tsb
    private bool towerAlreadthere;

    //variabel ini akan menyimpan placedTower dan bool toweralreadthere untuk sementara;
    private bool _tempTowerAlreadthere;
    private Tower _tempplacedTower;


    // Fungsi yang terpanggil sekali ketika ada object Rigidbody yang menyentuh area collider
    private void Start()
    {
        towerAlreadthere = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        //if (collision.gameObject.GetComponent<Bullet>())
        //{
        //    Debug.Log("this is a bullet");
        //    return;
        //}
        //if (collision.gameObject.GetComponent<Eraser>())
        //{
        //    Eraser erase = collision.gameObject.GetComponent<Eraser>();
        //    Debug.Log("This is eraser");
        //    erase.SetErase(_placedTower);
        //    _placedTower = null;
        //    towerAlreadthere = false;
        //    return;
        //}

        //if (_placedTower != null && towerAlreadthere)

        //{
        //    return;

        //}



        //Tower tower = collision.GetComponent<Tower>();

        //if (tower != null)

        //{
        //    Debug.Log("placed");
        //    tower.SetPlacePosition(transform.position);

        //    _placedTower = tower;
        //    towerAlreadthere = true;



        //}

    }



    // Kebalikan dari OnTriggerEnter2D, fungsi ini terpanggil sekali ketika object tersebut meninggalkan area collider

    private void OnTriggerExit2D(Collider2D collision)

    {
        //if (collision.gameObject.GetComponent<Bullet>())
        //{
        //    Debug.Log("this is a bullet");
        //    return;
        //}
        //if (collision.gameObject.GetComponent<Eraser>())
        //{
        //    Debug.Log("This is eraser");
        //    return;
        //}
        //Debug.Log("exited");

        //if (_placedTower == null || !towerAlreadthere)

        //{

        //    _placedTower = null;
        //    return;

        //}
        
        //_placedTower.SetPlacePosition(null);

    }

    //fungsi ini akan me reset status dari tower placement
    private void ResetPlacement()
    {
        _placedTower = null;
        towerAlreadthere = false;
    }
}
