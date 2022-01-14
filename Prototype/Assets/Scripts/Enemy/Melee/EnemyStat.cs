using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDamageAble<int>
{
    [Header("Enemy Basic Stat")]
    public float maxEnemyHp = 100;
    [SerializeField] float invicTimer = 1;
    [SerializeField] AudioClip deathAudio;
    public AudioClip enemyTakeDamageSfx;
    public float currentHp;
    public float damage;
    public bool isInvulnerable;
    public bool dead;
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
        currentHp = maxEnemyHp;

    }
    public bool checkAlive()
    {
        if (currentHp <= 0 && !dead)
        {
            dead = true;
            if (deathAudio != null)
            {
                SoundManager.Instance.PlaySFX(deathAudio);
            }
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
        if (checkAlive())
        {
            if (!isInvulnerable)
            {
                isInvulnerable = true;
                internalTimer = invicTimer;

                currentHp -= dmg;

                SoundManager.Instance.PlaySFX(enemyTakeDamageSfx);
                if(PopupHandler.Instance != null)
                {
                    PopupHandler.Instance.PopupDmg(gameObject, dmg.ToString());
                }
                if (Money.Instance != null)
                {
                    Money.Instance.addMoney(9);

                }
                if (ai != null)
                {
                    ai.setState(EnemyAI.State.knockback);
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
