using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Chronos;

public class DroneAI : MonoBehaviour
{
    public static Transform droneTarget1;
    public static Transform target;
    public Animator anim;
    public GameObject bullet, bulletParent;
    public Transform enemyGraphic;
    public float enemySpeed;
    public float nextWaypointDistance;
    public bool isAlerted = false;

    private GameObject player;
    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Timeline time;
    private int currentWaypoint = 0;
    private bool isReachedEndOfPath = false;

    [SerializeField] private float shootingRange;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime;
    private void Awake()
    {
        if (droneTarget1 == null)
        {
            droneTarget1 = GameObject.Find("Drone Target 1").GetComponent<Transform>();
        }
        if(target == null)
        {
            target = GameObject.Find("Target").GetComponent<Transform>();

        }
    }
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        //Debug.Log(seeker);
        rb = GetComponent<Rigidbody2D>();
        time = GetComponent<Timeline>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (!isAlerted)
        {
            isAlerted = true;
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        }
    }


    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, droneTarget1.position, OnPathComplete);
        }

    }

    private void OnPathComplete(Path p)
    {
        // check if didn't get any error
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if(path == null)
        {
            UpdatePath();
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            isReachedEndOfPath = true;
            return;
        }
        else
        {
            isReachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * enemySpeed * time.deltaTime;
        rb.velocity=force;
        //Debug.Log(dir);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        float distanceFromTarget = Vector2.Distance(droneTarget1.position, transform.position);
        if (distanceFromTarget <= shootingRange && nextFireTime < time.time)
        {
            GameObject bulletTemp = Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);

            bulletTemp.GetComponent<Bullet>().setTarget(ref target);
            nextFireTime = time.time + fireRate;
        }

        // flip enemy
        if (rb.velocity.x <= 0.01f)
        {
            enemyGraphic.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        } 
        else if(rb.velocity.x >= -0.01f)
        {
            enemyGraphic.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
