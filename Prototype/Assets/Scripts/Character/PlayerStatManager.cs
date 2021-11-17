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


    [Header("Energy Parameter")]
    [SerializeField] float energyDrainRate = 0.5f;

    [Header("UI Reference")]
    [SerializeField] private Slider hpProgressUI = null;
    [SerializeField] private Slider energyProgressUI = null;

    public TimeManager timeManager;
    public float currentHp;
    public float currentEnergy;
    private bool isUsingEnergy;
    private float internalTimer;
    // Start is called before the first frame update
    void Start()
    {
        isUsingEnergy = false;
        setPlayerStat();
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

    public void takeDamage(float dmg)
    {
        currentHp -= dmg;
        hpProgressUI.value = currentHp;
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
        if(internalTimer > invicTimer)
        {
            internalTimer = 0;
            takeDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            useEnergy();
            
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
}
