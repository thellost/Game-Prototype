using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Money : MonoBehaviour
{
    public static Money Instance { get; private set; }
    private int currentMoney;
    [SerializeField] TextMeshProUGUI display;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        currentMoney = GameManager.Progress.Money;

        display.text = currentMoney.ToString();
    }

   public void addMoney(int num)
    {
        currentMoney += num;
        
        GameManager.Progress.Money += num;
        display.text = currentMoney.ToString();
        if (PopupHandler.Instance != null)
        {
            PopupHandler.Instance.PopupMoney(num.ToString());
        }
    }

}
