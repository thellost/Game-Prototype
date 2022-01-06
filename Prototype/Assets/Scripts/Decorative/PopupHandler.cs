using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupHandler : MonoBehaviour
{
    //singleton
    public static PopupHandler Instance { get; private set; }
    [SerializeField] public GameObject popupDmg;
    [SerializeField] public GameObject popupMoney;



    private void Awake()
    {
        Instance = this;

    }

    // Update is called once per frame

    public void PopupDmg(GameObject enemy , string dmg)
    {
        GameObject obj= Instantiate(popupDmg, enemy.transform.position, Quaternion.identity);
        obj.GetComponent<TextMeshPro>().text = dmg;
        //instatiate
    }
    public void PopupMoney(string num)
    {
        if (popupMoney != null)
        {

            GameObject objm = Instantiate(popupMoney);
            objm.GetComponent<TextMeshPro>().text = "+" + num;
        }
        //instatiate
    }

    private void Update()
    {
        
    }
}

