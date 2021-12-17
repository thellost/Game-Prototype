using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DroneAI : MonoBehaviour
{
    public GameObject bullet, bulletParent;
    public Transform droneTarget1;
    public Transform enemyGraphic;
    public float enemySpeed;
    public float nextWaypointDistance;

    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;
    private int currentWaypoint = 0;
    private bool isReachedEndOfPath = false;

    [SerializeField] private float shootingRange;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
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
        Vector2 force = dir * enemySpeed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        float distanceFromTarget = Vector2.Distance(droneTarget1.position, transform.position);
        if (distanceFromTarget <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }

        // flip enemy
        if (force.x <= 0.01f)
        {
            enemyGraphic.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        } 
        else if(force.x >= -0.01f)
        {
            enemyGraphic.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    private void Flip()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
