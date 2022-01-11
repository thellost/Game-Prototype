using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBroken : MonoBehaviour
{
    public int health = 1;
    public UnityEngine.Object brokenboxs;
    public UnityEngine.Object particleExplosion;

    private AudioSource audioSource;

    bool isShaking = false;
    float shakeAmount = .01f;

    Vector2 startPos;

    void Start()
    {
        
    }

    void Update()
    {
        if(isShaking)
        {
            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmount;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Slash")
            startPos = transform.position;
            health--;

            if(health <= 0)
            {
                BreakIt();
            }
            else {
                isShaking = true;
                Invoke("ResetShake", .2f);
            }
    }

    void ResetShake()
    {
        isShaking = false;
        transform.position = startPos;
    }

    private void BreakIt()
    {
        GameObject destructable = (GameObject)Instantiate(brokenboxs);
        GameObject particles = (GameObject)Instantiate(particleExplosion);

        //map the newly loaded destructable object tothe x and y position of previously destroyed box
        destructable.transform.position = transform.position;
        particles.transform.position = transform.position;

        Destroy(gameObject);
    }
}
