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
    [SerializeField] GameObject bloodParticle;

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
        //bakal di lanjutkan di script State Machine , terus di state machine bakal nge triggerAttackRaycast
        //state machine doesnt work dont trust the above
        // kayaknya bagus pake animation event tapi semoga implementasi nya ga ribet kalau banyak
        //anj malah makin ribet
    }

    public void triggerAttackRaycast()
    {

        //check enemies
        RaycastHit2D[] hit = Physics2D.CircleCastAll(attackPoint.position, attackRangeCircle, new Vector2(0, 0), 0f, enemiesLayer);

        //damage the enemies
        foreach (RaycastHit2D enemy in hit)
        {
            Instantiate(bloodParticle, new Vector3(enemy.point.x + attackRangeCircle, enemy.point.y, 0), Quaternion.Euler(-90f, 0, 0));
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
