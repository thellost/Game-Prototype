using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFollowsPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject bullet, bulletParent;
    [SerializeField] private float droneSpeed;
    [SerializeField] private float triggerArea;
    [SerializeField] private float shootingRange;
    [SerializeField] private float fireRate = 1f;

    private float nextFireTime;

    private void FixedUpdate()
    {
        float distanceFromTarget = Vector2.Distance(target.position, transform.position);
        if(distanceFromTarget < triggerArea && distanceFromTarget > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.position, droneSpeed * Time.deltaTime);
        }
        else if (distanceFromTarget <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, triggerArea);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
