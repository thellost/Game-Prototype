using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class AimEnemy : MonoBehaviour
{
    // The target marker.
    [SerializeField] Transform area;
    [SerializeField] GameObject gunMuzzle;
    [SerializeField] EnemyAI ai;
    [SerializeField] Timeline time;
    [SerializeField] float turnRate;
    [SerializeField] float initialTimer = 1.5f;
    private Transform bulletSpawnDump;
    [SerializeField] GameObject bullet;
    // Angular speed in radians per sec.
    public float offset = 80f;
    public float radius = 1;
    private Animator anim;

    private Vector2 _startPos;

    private Transform target;
    private float timer;
    private Coroutine LookCoroutine;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        GameObject bulletSpawnDumpObject = GameObject.Find("BulletSpawn");
        if (bulletSpawnDumpObject == null)
        {
            bulletSpawnDump = transform;
        }
        else
        {
            bulletSpawnDump = bulletSpawnDumpObject.GetComponent<Transform>();
        }
        timer = 1;
    }

    private void Start()
    {

        target = ai.target;
    }
    private void disableAnimator()
    {
        Debug.Log("disabled");
        anim.enabled = false;
        timer = 1;
    }

    private void OnEnable()
    {
        anim.enabled = true;
        timer = initialTimer;
    }
    void Update()
    {

        // Mengubah posisi gun ke world position
            _startPos = area.position;
        if(LookCoroutine == null)
        {
            rotate();
        }
        move();
        timer -= time.deltaTime;
        
    }

    private void rotate()
    {
        if (ai.isFacingRight)
        {

            transform.right = (target.position - transform.position);
        }
        else
        {
            transform.right = -(target.position - transform.position);
        }
        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
        transform.Rotate(new Vector3(0, 0, offset));
    }

    private void move()
    {
        Vector2 p = target.position;
        //Hitung supaya 'karet' ketapel berada dalam radius yang ditentukan
        Vector2 dir = p - _startPos;
        if (dir.sqrMagnitude > radius)
            dir = dir.normalized * radius;
        transform.position = _startPos + dir;
    }

    public void fire()
    {
        if (timer < 0)
        {

            if (target != null)
            {
                GameObject bulletTemp = Instantiate(bullet, bulletSpawnDump);
                bulletTemp.GetComponent<Bullet>().setTarget(ref target);
                bullet.GetComponent<Transform>().position = gunMuzzle.transform.position;
            }
        }
    }
}
