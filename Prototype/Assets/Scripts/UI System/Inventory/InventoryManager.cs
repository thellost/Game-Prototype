using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public static GameObject invetoryManager;
    // Start is called before the first frame update
    void Awake()
    {
        invetoryManager = GameObject.Find("Inventory Manager");

        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(invetoryManager);
        }
    }

    //De equipt certain items
    public void DequipThisItem(GameObject item)
    {

    }

    //Equip Certain Items
    public void EquipThisItem(GameObject item)
    {

    }
}

  