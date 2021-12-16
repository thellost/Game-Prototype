using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawCircle : MonoBehaviour
{
    [SerializeField] float radius;
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
