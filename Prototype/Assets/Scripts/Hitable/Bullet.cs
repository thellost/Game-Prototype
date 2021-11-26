using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool deflected;
    [SerializeField] float speed = 20;

    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(-30, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I HIT PLAYER");
    }

    public void Deflected()
    {
        if (!deflected)
        {
            rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
            Debug.Log("Deflected");
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
        }
        deflected = true;
    }
}
