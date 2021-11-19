using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [Header("Enemy Basic Stat")]
    [SerializeField] float maxPlayerHP = 100;
    [SerializeField] float invicTimer = 1;

    public float currentHp;
    private float internalTimer;
    // Start is called before the first frame update
    void Start()
    {
        setPlayerStat();
    }

    private void setPlayerStat()
    {
        //perlu di ubah kedepannya karena bakal ada implementasi save
        currentHp = maxPlayerHP;

    }

    public bool takeDamage(float dmg)
    {
        if (internalTimer <= 0)
        {
            internalTimer = invicTimer;
            
            currentHp -= dmg;

            if (currentHp <= 0)
            {
                Destroy(gameObject);
            }
            return true;
        }
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        if(internalTimer > 0)
        {
            internalTimer -= Time.deltaTime;
        }
    }
}