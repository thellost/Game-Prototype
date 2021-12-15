using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEnemy : MonoBehaviour
{
    // The target marker.
    public Transform target;
    public Transform area;
    // Angular speed in radians per sec.
    public float offset = 80f;
    public float radius = 1;

    private Vector2 _startPos;
    private void Start()
    {
        _startPos = area.position;
    }
    void Update()
    {
        transform.right = target.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
        transform.Rotate(new Vector3(0, 0, offset));
        // Mengubah posisi mouse ke world position
        Vector2 p = target.position;
        //Hitung supaya 'karet' ketapel berada dalam radius yang ditentukan
        Vector2 dir = p - _startPos;
        if (dir.sqrMagnitude > radius)
            dir = dir.normalized * radius;
        transform.position = _startPos + dir;
    }
}
