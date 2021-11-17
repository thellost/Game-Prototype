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
    public float attackDamage;
    public float animationOffset;
    [SerializeField] GameObject bloodParticle;
    [SerializeField] GameObject slashParticle;
    [SerializeField] float cameraShakeIntensity;
    [SerializeField] float cameraShakeTimer;
    private bool isAttacking;

    // Update is called once per frame

    private void Start()
    {
        isAttacking = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (!isAttacking) {
            //play the animation
            animator.SetTrigger("isAttack");
            isAttacking = true;
            //bakal di lanjutkan di script State Machine , terus di state machine bakal nge triggerAttackRaycast
            //state machine doesnt work dont trust the above
            // kayaknya bagus pake animation event tapi semoga implementasi nya ga ribet kalau banyak
            //anj malah makin ribet
        }
            
    }

    public void triggerAttackRaycast()
    { 
        
        //mouse position
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector3 directionAnimation = direction.normalized * attackRangeCircle;

        //check enemies
        RaycastHit2D[] hit = Physics2D.CircleCastAll(directionAnimation + transform.position, attackRangeCircle, new Vector2(0, 0), 0f, enemiesLayer);

        //damage the enemies
        foreach (RaycastHit2D enemy in hit)
        {
            if(CameraShake.Instance != null)
            {
                CameraShake.Instance.ShakeCamera(cameraShakeIntensity , cameraShakeTimer);
            }
            Instantiate(bloodParticle, new Vector3(enemy.point.x + attackRangeCircle, enemy.point.y, -2), Quaternion.Euler(-90f, 0, 0));
        }
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRangeCircle, enemiesLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStat>().takeDamage(attackDamage);
        }
    }

    
    

    private void spawnSlashAnimation()
    {
        //mouse position to rotation
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //instantiate the shit out of slash, i should get paid for this
        GameObject gameobj = Instantiate(slashParticle) as GameObject;
        gameobj.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        gameobj.transform.position = transform.position;
        gameobj.transform.parent = gameObject.transform;
        gameobj.transform.rotation = Quaternion.Euler(0, 0, -68.43f) * rotation; //magic number -68.43f karena rotasi dari animasinya ga lurus
        StartCoroutine(MoveAnimation(gameobj, direction.normalized));
    }
    

    //this is fucking useless need to replace it in animator
 
    private IEnumerator WaitForAttackCooldown()
    {
         yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }

    private IEnumerator MoveAnimation(GameObject slash, Vector3 direction)
    {
        while (slash != null)
        {
            if(slash == null)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
            slash.transform.position = Vector2.MoveTowards(slash.transform.position, (direction + transform.position) * attackRangeCircle, 0.05f);
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
