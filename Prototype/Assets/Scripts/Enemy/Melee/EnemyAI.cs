
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class EnemyAI : MonoBehaviour, IDamageAble<int> {

    [SerializeField] float moveSpeed;
    [SerializeField] float attackDistance;
    [SerializeField] protected float attackSpeed;
    [SerializeField] float knockbackForce = 1f;
    [SerializeField] float knockbackDuration = 2f;
    [SerializeField] bool flipLock = false;
    [SerializeField] Collider2D hitBox;
    [SerializeField] Collider2D detectionBox;

    public enum State {
        Patroling,
        ChaseTarget,
        AttackTarget,
        knockback,
        dead,
        idle
    }

    public bool isFacingRight { get; private set; }
    public Transform target { get; private set; }

    protected bool isAttacking;

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 attackDir;
    private float nextShootTime;
    private float attackTimer;
    private float knockbackForceprivate;
    private float timeElapsed;
    private State state;
    private Animator anim;
    private Timeline time;
    private bool dead;
    private Rigidbody2D rb;
    private EnemyStat stats;

    protected virtual void Awake() {
        if (transform.localScale.x < 0)
        {
            isFacingRight = true;
        }
        stats = gameObject.GetComponent<EnemyStat>();
        state = State.idle;
        target = GameObject.Find("Player").transform.GetChild(0).gameObject.transform;
        anim = gameObject.GetComponent<Animator>();
        time = gameObject.GetComponent<Timeline>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        knockbackForceprivate = knockbackForce;
        dead = false ;
    }
    public void OnHit(int damage)
    {

    }

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update() {
        //attack timer untuk jeda attack
        attackTimer -= time.deltaTime;
        if (!stats.checkAlive() && !dead)
        {
            dead = true;
            if (Money.Instance != null)
            {
                Money.Instance.addMoney(9);

            }
            setState(State.dead);

        }
        switch (state) {
            default:
            case State.Patroling:
                moveTowardTarget();
                if(checkPlayerDistance())
                {
                    setState(State.AttackTarget);
                }
                break;

            case State.ChaseTarget:

                if (checkPlayerDistance())
                {
                    setState(State.AttackTarget);
                    break;
                }
                moveTowardTarget();
               
                break;

            case State.AttackTarget:
                if (attackTimer <= 0 && checkPlayerDistance() && (!isAttacking || flipLock))
                {
                    attack();
                }
                else if (!checkPlayerDistance())
                {
                   
                    chaseTarget();
                    break;
                }

                checkFlip();
                break;

            case State.knockback:
                //math for smoothing
                float temp = Mathf.SmoothStep(knockbackForce,0, timeElapsed/knockbackDuration);
                temp *= time.timeScale;
                knockback(temp);

                timeElapsed += time.deltaTime;

                if(timeElapsed > knockbackDuration)
                {
                    setState(State.ChaseTarget);
                }

                anim.SetTrigger("knockback");

                break;
            case State.idle:
                break;
            case State.dead:
                if(timeElapsed > knockbackDuration)
                {
                    return;
                }
                anim.SetTrigger("dead");
                //math for smoothing
                float tempDead = Mathf.SmoothStep(knockbackForce, 0, timeElapsed / knockbackDuration);
                tempDead *= time.timeScale;

                knockback(tempDead);

                knockback(tempDead/2 , true);
                timeElapsed += time.deltaTime;
               
                break;
        }
    }

    protected void setAttackFalse()
    {
        isAttacking = false;
        anim.SetBool("attacking", false);
    }

    private Vector3 GetRoamingPosition() {
        return Vector3.left;
    }

    private void knockback(float power, bool yAxis = false)
    {
        if (!yAxis)
        {
            if (!isFacingRight)
            {
                rb.velocity = new Vector2(power, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-power, rb.velocity.y);
            }
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x, power);
        }
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


    //ini di panggil di animasi
    private void disableHitBox()
    {
        hitBox.enabled = false;
    }
    private void enableHitBox()
    {

        hitBox.enabled = enabled;
    }

    private void disableGameObject()
    {
        gameObject.SetActive(false);
    }
    private void flip()
    {
        if (!isAttacking || !flipLock)
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
    public virtual void setState(State stateParameter)
    {
        isAttacking = false;
        disableHitBox();
        state = stateParameter;
        timeElapsed = 0;
    }

    public virtual bool checkPlayerDistance()
    {
        if(Vector2.Distance(target.position, transform.position) < attackDistance)
        {
            return true;
        }
        return false;
    }

    protected virtual void attack()
    {
        anim.ResetTrigger("walk");
        isAttacking = true;
        anim.SetBool("attacking", true);
        attackTimer = attackSpeed;
        anim.SetTrigger("Attack");
    }

    public virtual void chaseTarget()
    {
        setAttackFalse();
        anim.SetBool("attacking", false);
        setState(State.ChaseTarget);
    }

}
