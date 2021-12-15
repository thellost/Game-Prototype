
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemyAIRanged : EnemyAI {

    public override void attack()
    {
        
        isAttacking = true;
        anim.SetBool("attacking", true);
        attackTimer = attackSpeed;
        anim.SetTrigger("Attack");
    }
}
