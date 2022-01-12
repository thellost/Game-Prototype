using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private Slider hpProgressUI = null;
    
    private bool isDead;
    private Animator anim;
    private EnemyStat stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<EnemyStat>();
        anim = GetComponent<Animator>();
        isDead = stats.dead;
        hpProgressUI.maxValue = stats.maxEnemyHp;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(stats.dead);
        if (stats.dead)
        {
            anim.SetBool("Dead", true);
        }
        hpProgressUI.value = stats.currentHp;
    }
    private void endFightBoss()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
