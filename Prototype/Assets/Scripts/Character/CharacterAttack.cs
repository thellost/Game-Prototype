using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Animator animator;
    public float attackSpeed;
    public float attackRangeCircle;
    public Transform attackPoint;
    public LayerMask enemiesLayer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        //play the animation
        animator.SetTrigger("isAttack");

        //check enemies
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRangeCircle, enemiesLayer);

        //damage the enemies
        foreach(Collider2D enemy in hit)
        {
            Debug.Log("We Hit" + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeCircle);
    }
}
