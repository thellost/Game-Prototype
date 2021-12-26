using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrucedRigid : MonoBehaviour
{
    public Vector2 forceDirection;
    public float torque;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        float randTorque = UnityEngine.Random.Range(-50, 50);
        float randForceX = UnityEngine.Random.Range(forceDirection.x -50, forceDirection.x +50);
        float randForceY = UnityEngine.Random.Range(forceDirection.y, forceDirection.x +50);

        forceDirection.x = randForceX;
        forceDirection.y = randForceY;

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(forceDirection);
        rb2d.AddTorque(randTorque);    

        Invoke("DestroySelf", UnityEngine.Random.Range(2.5f, 4f));
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
