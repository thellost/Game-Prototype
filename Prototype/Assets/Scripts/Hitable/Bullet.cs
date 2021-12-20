using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class Bullet : MonoBehaviour, IDamageAble<float>
{
    private Rigidbody2D rb;
    private Timeline time;
    private bool deflected;
    [SerializeField] float bulletSpeed = 30;

    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer;

    private Vector2 speedDirection;
    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<Timeline>();
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(-30, 0);
    }


    private void FixedUpdate()
    {
        // test pakai moveposition
        rb.MovePosition(rb.position + speedDirection * bulletSpeed * time.fixedDeltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I HIT PLAYER");
    }

    public void Deflected()
    {
        if (!deflected)
        {
            //rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
            speedDirection *= -1;
            Debug.Log("Deflected");
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
        }
        deflected = true;
    }
    public void OnHit(float test) {
        Deflected();
    }

    public void setTarget(ref Transform target)
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float xside = Mathf.Cos(angle * Mathf.PI / 180);
        float yside = Mathf.Sin(angle * Mathf.PI / 180);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = transform.rotation * rotation;
        speedDirection = new Vector2(xside, yside);
        Debug.Log(speedDirection);
    }
}
