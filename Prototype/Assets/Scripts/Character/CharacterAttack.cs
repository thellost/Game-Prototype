using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class CharacterAttack : MonoBehaviour
{
    public Animator animator;
    public float attackSpeed;
    public float attackRangeCircle;
    public Transform attackPoint;
    public LayerMask enemiesLayer;
    public LayerMask bulletLayer;
    public float attackDamage;
    public float animationOffset;
    [SerializeField] GameObject explosionParticle;
    [SerializeField] GameObject bloodParticle;
    [SerializeField] GameObject slashParticle;
    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer = 0.1f;
    [SerializeField] float slashAnimationSpeed;

    private bool isAttacking;
    private PlayerVelocity playerVelocity;
    private Vector2 direction;
    private Timeline time;
    private Movement movement;
   
    // Update is called once per frame

    private void Start()
    {
        movement = GetComponent<Movement>();
        playerVelocity = GetComponent<PlayerVelocity>();
        time = GetComponent<Timeline>();
        isAttacking = false;
    }
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float xside = Mathf.Cos(angle * Mathf.PI / 180);
        float yside = Mathf.Sin(angle * Mathf.PI / 180);
       
        playerVelocity.OnDashInputDown(xside, yside);
    }

    public void triggerAttackRaycast()
    {
        //mouse position
        Vector3 directionAnimation = direction.normalized * attackRangeCircle;
        
        //check enemies
        Collider2D[] hit = Physics2D.OverlapCircleAll(directionAnimation + transform.position, attackRangeCircle, enemiesLayer);
        //check Bullet
        Collider2D[] hitBullet = Physics2D.OverlapCircleAll(directionAnimation + transform.position, attackRangeCircle, bulletLayer);

        //damage the enemies
        foreach (Collider2D enemy in hit)
        {
            EnemyStat enemyComponent = enemy.transform.gameObject.GetComponent<EnemyStat>();
            EnemyAI enemeyAI = enemy.transform.gameObject.GetComponent<EnemyAI>();
            DroneAI droneAI = enemy.transform.gameObject.GetComponent<DroneAI>();
            //handle damage and camera shake also buller
            if (enemyComponent != null) {
                if (enemyComponent.takeDamage(attackDamage))
                {
                    //handle the AI
                    if (enemeyAI != null)
                    {

                        enemeyAI.setAttackDirection(directionAnimation);
                    }
                    if(droneAI != null)
                    {
                        Explosion(enemy);
                        Destroy(droneAI.gameObject);
                        //lakukan sesuai dengan drone damage
                    }
                    else
                    {
                        spawnBlood(enemy);
                    }
                    if (CameraShake.Instance != null)
                    {
                        CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
                    }
                }
            } 

        }
        foreach(Collider2D projectile in hitBullet)
        {

            Bullet bullet = projectile.transform.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Deflected();
            }
        }

     
    }

    private Vector3 setLocalToParent(Vector3 scale)
    {
        if (transform.localScale.x < 0)
        {
            scale = new Vector3(scale.x * -1, scale.y, scale.z);
        }
        else
        {

            scale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
        }
        return scale;
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
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //instantiate the shit out of slash, i should get paid for this
        GameObject gameobj = Instantiate(slashParticle) as GameObject;
        gameobj.transform.localScale = new Vector3(transform.localScale.x * 0.6f, transform.localScale.y*1.4f, transform.localScale.z);
        gameobj.transform.parent = gameObject.transform;
        gameobj.transform.rotation = gameobj.transform.rotation * rotation; //magic number -68.43f karena rotasi dari animasinya ga lurus
        gameobj.transform.localScale *= 1.5f;

        gameobj.transform.localScale = setLocalToParent(gameobj.transform.localScale);
        StartCoroutine(MoveAnimation(gameobj, direction.normalized));
        
        //set  transform behind player
        Vector2 temp2 = direction;
        setTraditionalNormalize(ref temp2);
        Vector3 temp3 = temp2;
        gameobj.transform.position = transform.position - (0.5f * temp3);
    }

    private void spawnBlood(Collider2D enemy)
    {

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject gameobj = Instantiate(bloodParticle, new Vector3(enemy.transform.position.x + attackRangeCircle, enemy.transform.position.y, -2), Quaternion.Euler(-90f, 0, 0));
        gameobj.transform.rotation = Quaternion.Euler(-90f, 0, 0) * rotation;

    }

    private void Explosion(Collider2D drone)
    {
        //GameObject particle = (GameObject)Instantiate(explosionParticle);
        GameObject gameobj = Instantiate(explosionParticle, drone.transform.position, Quaternion.identity) as GameObject;
        Debug.Log("Explode!");
    }


    //this is fucking useless need to replace it in animator

    private IEnumerator WaitForAttackCooldown()
    {
        float timeElapsed = 0;
        while (timeElapsed < attackSpeed)
        {
            timeElapsed += time.deltaTime;

            yield return null;
        }
        isAttacking = false;
    }

    private IEnumerator MoveAnimation(GameObject slash, Vector3 direction)
    {

        while (slash != null)
        {
            slash.transform.position = Vector2.MoveTowards(slash.transform.position, (direction + transform.position) * attackRangeCircle, slashAnimationSpeed * time.deltaTime); //10f magic number

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
