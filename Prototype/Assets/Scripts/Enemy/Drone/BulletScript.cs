using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    private Rigidbody2D rb;
    private GameObject target;
    private Vector2 previousSpeed;
    private Timeline time;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        Vector2 moveDir = (target.transform.position - transform.position).normalized * bulletSpeed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        previousSpeed = rb.velocity;
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity *= Time.timeScale;
        if(Time.timeScale == 1)
        {
            rb.velocity = previousSpeed;
        }
    }

    public void setTimeline(Timeline timeTemp)
    {
        time = timeTemp;
    }
}
