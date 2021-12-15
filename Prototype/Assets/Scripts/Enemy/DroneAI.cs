using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DroneAI : MonoBehaviour
{
    public Transform target;
    public Transform enemyGraphic;
    public float enemySpeed;
    public float nextWaypointDistance;

    Path path;
    Seeker seeker;
    Rigidbody2D rb;
    int currentWaypoint = 0;
    bool isReachedEndOfPath = false;

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
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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

        if(force.x <= 0.01f)
        {
            enemyGraphic.localScale = new Vector3(-1f, 1f, 1f);
        } 
        else if(force.x >= -0.01f)
        {
            enemyGraphic.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
