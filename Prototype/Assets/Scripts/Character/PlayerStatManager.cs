using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{
    [Header("Player Basic Stat")]
    [SerializeField] float maxPlayerHP = 100;
    [SerializeField] float maxPlayerEnergy = 50;
    [SerializeField] float invicTimer = 1;
    [SerializeField] float knockbackPower = 10;

    
    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer = 0.1f;

    [Header("Energy Parameter")]
    [SerializeField] float energyDrainRate = 0.5f;
    [SerializeField] float energyRegenRate = 0.5f;

    [Header("UI Reference")]
    [SerializeField] private Slider hpProgressUI = null;
    [SerializeField] private Slider energyProgressUI = null;

    public TimeManager timeManager;
    public float currentHp;
    public float currentEnergy;

    public bool isDead;
    private bool isUsingEnergy;
    private float internalTimer;
    private PlayerVelocity player;
    private CharacterAttack attack;
    private PlayerAnimator animator;
    // Start is called before the first frame update
    void Start()
    {
        isUsingEnergy = false;
        setPlayerStat();
        player = GetComponent<PlayerVelocity>();
        attack = GetComponent<CharacterAttack>();
        animator = GetComponent<PlayerAnimator>();
    }

    private void setPlayerStat()
    {
        //perlu di ubah kedepannya karena bakal ada implementasi save
        currentHp = maxPlayerHP;
        currentEnergy = maxPlayerEnergy;


        hpProgressUI.maxValue = maxPlayerHP;
        hpProgressUI.value = currentHp;
        energyProgressUI.maxValue = maxPlayerEnergy;
        energyProgressUI.value = currentEnergy;

    }

    public void takeDamage(float dmg, Vector3 enemyPosition)
    {
        CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
        currentHp -= dmg;
        hpProgressUI.value = currentHp;
        Vector2 direction = transform.position - enemyPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float xside = Mathf.Cos(angle * Mathf.PI / 180) * knockbackPower;
        float yside = Mathf.Sin(angle * Mathf.PI / 180) * knockbackPower;
        player.knockback(xside , yside);
    }

    public void useEnergy()
    {
        isUsingEnergy = !isUsingEnergy;
       
    }

    // Update is called once per frame
    void Update()
    {
        energyProgressUI.value = currentEnergy;
        internalTimer += Time.deltaTime;
        //if(internalTimer > invicTimer)
        //{
        //    internalTimer = 0;
        //    takeDamage(5);
        //}
        if (Input.GetKeyDown(KeyCode.X))
        {
            useEnergy();
            
        }

        if(currentHp <= 0)
        {
            dead();
            
        }


        if (isUsingEnergy)
        {
            currentEnergy -= energyDrainRate * Time.unscaledDeltaTime;
            
            if (currentEnergy <= 0)
            {
                timeManager.stopSlowMotion();
            }
        }

    }

    public void dead()
    {
        animator.SetDead(true);
        animator.enabled = false;
        attack.enabled = false;
        isDead = true;
    }
}
