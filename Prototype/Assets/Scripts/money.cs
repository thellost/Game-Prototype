using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class money : MonoBehaviour
{
    public static money Instance { get; private set; }
    private int currentMoney;
    [SerializeField] TextMeshProUGUI display;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        currentMoney = PlayerPrefs.GetInt("money");
        display.text = currentMoney.ToString();
    }

   public void addMoney(int num)
    {
        currentMoney += num;
        display.text = currentMoney.ToString();
        if (PopupHandler.Instance != null)
        {
            PopupHandler.Instance.PopupMoney(num.ToString());
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("money", currentMoney);
    }
}
