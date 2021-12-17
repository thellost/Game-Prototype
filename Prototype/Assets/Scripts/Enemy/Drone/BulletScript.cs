using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    private Rigidbody2D rb;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        Vector2 moveDir = (target.transform.position - transform.position).normalized * bulletSpeed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
