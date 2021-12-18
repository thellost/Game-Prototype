﻿
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemyAIRanged : EnemyAI {
    [SerializeField] SpriteRenderer selfSprite;
    [SerializeField] GameObject bone;
    [SerializeField] float initialAttackTime;
    [SerializeField] AimEnemy aim;
    public override void attack()
    {
        Debug.Log("hai aku attack");
        fire();
        toggleSprite(false);
        base.attack();

        isAttacking = false;

    }
    public override void chaseTarget()
    {
        toggleSprite(true);
        base.chaseTarget();
    }

    public override void setState(State stateParameter)
    {
        toggleSprite(true);
        base.setState(stateParameter);
    }

    private void toggleSprite(bool isActive)
    {
        bone.SetActive(!isActive);
        selfSprite.enabled = isActive;
    }
    private void fire()
    {
        aim.fire();
    }


}
