using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private Slider hpProgressUI = null;
    private EnemyStat stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<EnemyStat>();
        hpProgressUI.maxValue = stats.maxEnemyHp;
    }

    // Update is called once per frame
    private void Update()
    {
        hpProgressUI.value = stats.currentHp;
    }
}
