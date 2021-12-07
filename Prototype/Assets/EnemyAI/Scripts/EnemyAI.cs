
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemyAI : MonoBehaviour {

    [SerializeField] float moveSpeed;
    [SerializeField] float attackDistance;
    [SerializeField] float attackSpeed;
    [SerializeField] float knockbackForce = 1f;
    [SerializeField] float knockbackDuration = 2f;
    [SerializeField] Collider2D hitBox;
    [SerializeField] Collider2D detectionBox;
    public enum State {
        Patroling,
        ChaseTarget,
        AttackTarget,
        knockback,
        idle
    }

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 attackDir;
    private float nextShootTime;
    private float attackTimer;
    private float knockbackForceprivate;
    private float timeElapsed;
    private State state;
    private Transform target;
    private Animator anim;
    private Timeline time;
    private bool isFacingRight;
    private bool isAttacking;
    private Rigidbody2D rb;

    private void Awake() {
        state = State.AttackTarget;
        target = GameObject.Find("Player").GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        time = gameObject.GetComponent<Timeline>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        knockbackForceprivate = knockbackForce;
    }

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update() {
        //attack timer untuk jeda attack
        attackTimer -= time.deltaTime;

        switch (state) {
            default:
            case State.Patroling:
                moveTowardTarget();
                if(Vector2.Distance(target.position, transform.position) < attackDistance)
                {
                    setState(State.AttackTarget);
                }
                break;

            case State.ChaseTarget:

                if (Vector2.Distance(target.position, transform.position) < attackDistance)
                {
                    setState(State.AttackTarget);
                    break;
                }
                moveTowardTarget();
               
                break;

            case State.AttackTarget:
                if (attackTimer <= 0 && Vector2.Distance(target.position, transform.position) < attackDistance)
                {

                    isAttacking = true;
                    anim.SetBool("attacking", true);
                    attackTimer = attackSpeed;
                    anim.SetTrigger("Attack");
                }
                else if (Vector2.Distance(target.position, transform.position) > attackDistance)
                {

                    anim.SetBool("attacking", false);
                    isAttacking = false;
                    setState(State.ChaseTarget);
                    break;
                }
                if (!isAttacking)
                {
                    checkFlip();
                }
               
                break;

            case State.knockback:
                //math for smoothing
                float temp = Mathf.SmoothStep(knockbackForce,0, timeElapsed/knockbackDuration);
                temp *= time.timeScale;
                if (!isFacingRight)
                {
                    rb.velocity = new Vector2(temp, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-temp, rb.velocity.y);
                }
                timeElapsed += time.deltaTime;

                if(timeElapsed > knockbackDuration)
                {
                    setState(State.ChaseTarget);
                }
                Debug.Log(knockbackForce);
                anim.SetTrigger("knockback");
                
                break;
            case State.idle:
                break;
        }
    }

    private void setAttackFalse()
    {
        isAttacking = false;
    }

    private Vector3 GetRoamingPosition() {
        return Vector3.left;
    }


    private void moveTowardTarget()
    {
        anim.SetTrigger("walk");

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            //ini mengechek flip
            checkFlip();

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * time.deltaTime);
        }

    }

    private void checkFlip()
    {
        //ini mengechek flip
        if ((target.position.x - transform.position.x) < 0 && isFacingRight)
        {
            flip();
        }
        else if ((target.position.x - transform.position.x) > 0 && !isFacingRight)
        {
            flip();
        }
    }
    private void flip()
    {
        if (!isAttacking)
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void setTarget(Transform point)
    {
        target = point;
    }

    public void setAttackDirection(Vector3 direction)
    {
        attackDir = direction;
        checkFlip();
    }
    public void setState(State stateParameter)
    {
        state = stateParameter;
        timeElapsed = 0;
    }

}
