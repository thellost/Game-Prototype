using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class Bullet : MonoBehaviour, IDamageAble<float>
{
    private Rigidbody2D rb;
    private Timeline time;
    private bool deflected;
    [SerializeField] float bulletSpeed = 30;
    [SerializeField] float damage = 10f;
    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer;
    private BulletManager bulletManager;
    private Vector2 speedDirection;
    // Start is called before the first frame update
    void Awake()
    {
        bulletManager = GetComponentInParent<BulletManager>();
        time = GetComponent<Timeline>();
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(-30, 0);
    }


    private void FixedUpdate()
    {
        // test pakai moveposition
        rb.MovePosition(rb.position + speedDirection * bulletSpeed * time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        deflected = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I HIT PLAYER");
    }

    public void Deflected()
    {
        if (!deflected)
        {
            //rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
            speedDirection *= -1;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
        }
        deflected = true;
    }
    public void OnHit(float test) {
        Deflected();
    }

    public void setTarget(ref Transform target)
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float xside = Mathf.Cos(angle * Mathf.PI / 180);
        float yside = Mathf.Sin(angle * Mathf.PI / 180);
        //quarternion is really weird
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        speedDirection = new Vector2(xside, yside);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !deflected)
        {

            PlayerStatManager playerStatManager;
            playerStatManager = collision.gameObject.GetComponent<PlayerStatManager>();
            if (playerStatManager != null)
            {

                if (playerStatManager.isInvulnerable) 
                {
                    return;
                }

                playerStatManager.takeDamage(damage, transform.position);
            }
            bulletManager.ReturnToPool(gameObject);
            return;
        }
        else if (collision.gameObject.tag == "Ground")
        {
            bulletManager.ReturnToPool(gameObject);

            return;
        }
        if (deflected && collision.gameObject.tag == "Enemy")
        {
            Debug.Log("TEST ENEMY HIT");
            EnemyStat enemyStat = collision.gameObject.GetComponent<EnemyStat>();
            if (enemyStat != null)
            {
                enemyStat.takeDamage(damage);
            }

            bulletManager.ReturnToPool(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            PlayerStatManager playerStatManager;
            playerStatManager = collision.gameObject.GetComponent<PlayerStatManager>();
            if (playerStatManager != null)
            {

                playerStatManager.takeDamage(damage, transform.position);
            }
            bulletManager.ReturnToPool(gameObject);
        }
        else if(collision.gameObject.tag == "Ground")
        {
            Debug.Log("ground");
            bulletManager.ReturnToPool(gameObject);
        } else if(deflected && collision.gameObject.tag == "Enemy")
        {
            EnemyStat enemyStat = collision.gameObject.GetComponent<EnemyStat>();
            if (enemyStat != null)
            {
                enemyStat.takeDamage(damage);
            }

            bulletManager.ReturnToPool(gameObject);
        }
    }
}
