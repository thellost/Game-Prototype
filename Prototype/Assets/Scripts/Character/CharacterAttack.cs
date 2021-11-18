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
    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer;
    private bool isAttacking;
    private PlayerVelocity playerVelocity;
    private Vector2 direction;
    // Update is called once per frame

    private void Start()
    {
        playerVelocity = GetComponent<PlayerVelocity>();
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
            setMousePosition();
            dashToMouse();
            animator.SetTrigger("isAttack");
            isAttacking = true;
            StartCoroutine(WaitForAttackCooldown());
            //bakal di lanjutkan di script State Machine , terus di state machine bakal nge triggerAttackRaycast
            //state machine doesnt work dont trust the above
            // kayaknya bagus pake animation event tapi semoga implementasi nya ga ribet kalau banyak
            //anj malah makin ribet
        }
            
    }
    private void setMousePosition()
    {

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void dashToMouse()
    {
        //set dash
        Vector2 temp = direction;
        setTraditionalNormalize(ref temp);
        playerVelocity.OnDashInputDown(temp.x, temp.y);
    }
    public void triggerAttackRaycast()
    { 
        
        //mouse position
        Vector3 directionAnimation = direction.normalized * attackRangeCircle;
        
        //check enemies
        RaycastHit2D[] hit = Physics2D.CircleCastAll(directionAnimation + transform.position, attackRangeCircle, new Vector2(0, 0), 0f, enemiesLayer);

       

        //damage the enemies
        foreach (RaycastHit2D enemy in hit)
        {
            
            Instantiate(bloodParticle, new Vector3(enemy.point.x + attackRangeCircle, enemy.point.y, -2), Quaternion.Euler(-90f, 0, 0));
        }
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRangeCircle, enemiesLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyStat>().takeDamage(attackDamage))
            {
                if (CameraShake.Instance != null)
                {
                    CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
                }

            }
        }
    }

    
    private void setTraditionalNormalize(ref Vector2 vector2)
    {
        float temp = Mathf.Abs(vector2.x) + Mathf.Abs(vector2.y);
        vector2.x = vector2.x / temp;
        vector2.y = vector2.y / temp;

    }


    //ini di panggil melalui event animation
    private void spawnSlashAnimation()
    {
        //mouse position to rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //instantiate the shit out of slash, i should get paid for this
        GameObject gameobj = Instantiate(slashParticle) as GameObject;
        gameobj.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        gameobj.transform.parent = gameObject.transform;
        gameobj.transform.rotation = Quaternion.Euler(0, 0, -68.43f) * rotation; //magic number -68.43f karena rotasi dari animasinya ga lurus
        StartCoroutine(MoveAnimation(gameobj, direction.normalized));

        //set  transform behind player
        Vector2 temp2 = direction;
        setTraditionalNormalize(ref temp2);
        Vector3 temp3 = temp2;
        gameobj.transform.position = transform.position - (0.5f * temp3);
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
            slash.transform.position = Vector2.MoveTowards(slash.transform.position, (direction + transform.position) * attackRangeCircle, 0.05f);
            yield return new WaitForFixedUpdate();
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
