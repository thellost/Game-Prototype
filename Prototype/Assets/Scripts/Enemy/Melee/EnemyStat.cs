using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDamageAble<int>
{
    [Header("Enemy Basic Stat")]
    [SerializeField] float maxPlayerHP = 100;
    [SerializeField] float invicTimer = 1;

    public AudioClip enemyTakeDamageSfx;
    public float currentHp;
    public float damage;
    public bool isInvulnerable;
    public bool dead;
    public GameObject popupDmg;
    private float internalTimer;
    private EnemyAI ai;
    // Start is called before the first frame update
    void Awake()
    {
        ai = GetComponent<EnemyAI>();
        internalTimer = 0;
        setPlayerStat();
        dead = false;
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
            dead = true;

            this.enabled = false;
            return false;
        }
        return true;
    }

    private void disableRigidbody()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    public bool takeDamage(float dmg)
    {
        if (!dead)
        {
            if (!isInvulnerable)
            {
                isInvulnerable = true;
                internalTimer = invicTimer;

                currentHp -= dmg;

                Debug.Log("tas");
                SoundManager.Instance.PlaySFX(enemyTakeDamageSfx);
                if(popupDmg != null)
                {

                    Instantiate(popupDmg, transform.position, Quaternion.identity);
                }
                
                return true;
            }
           
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
        else
        {
            isInvulnerable = false;
        }
    }

    public void OnHit(int damage)
    {
        //ini kena hit
    }

}