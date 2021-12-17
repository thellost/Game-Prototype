using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class AimEnemy : MonoBehaviour
{
    // The target marker.
    [SerializeField] Transform area;
    [SerializeField] EnemyAI ai;
    [SerializeField] Timeline time;
    [SerializeField] float turnRate;
    // Angular speed in radians per sec.
    public float offset = 80f;
    public float radius = 1;
    private Animator anim;

    private Vector2 _startPos;

    private Transform target;

    private Coroutine LookCoroutine;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {

        target = ai.target;
    }
    private void disableAnimator()
    {
        Debug.Log("disabled");
        anim.enabled = false;
    }

    private void OnEnable()
    {
        anim.enabled = true;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z),turnRate * time.deltaTime);
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


}
