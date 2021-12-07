using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [Header("Enemy Basic Stat")]
    [SerializeField] float maxPlayerHP = 100;
    [SerializeField] float invicTimer = 1;


    public float currentHp;
    public float damage;
    public bool isInvulnerable;
    private float internalTimer;
    private EnemyAI ai;
    // Start is called before the first frame update
    void Awake()
    {
        ai = GetComponent<EnemyAI>();
        internalTimer = 0;
        setPlayerStat();
    }

    private void setPlayerStat()
    {
        //perlu di ubah kedepannya karena bakal ada implementasi save
        currentHp = maxPlayerHP;

    }
    public bool checkAlive()
    {
        if (currentHp <= 0)
        {
            return false;
        }
        return true;
    }
    public bool takeDamage(float dmg)
    {
        if (!isInvulnerable)
        {
            internalTimer = invicTimer;
            
            currentHp -= dmg;

            
            return true;
        }
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        if(internalTimer > 0)
        {
            isInvulnerable = true;
            internalTimer -= Time.deltaTime;
        }
        else
        {
            isInvulnerable = false;
        }
    }

}
