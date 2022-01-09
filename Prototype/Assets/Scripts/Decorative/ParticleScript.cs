using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    BulletManager bulletManager;

    // Start is called before the first frame update
    void Awake()
    {

        bulletManager = GameObject.FindGameObjectWithTag("Spawner").GetComponent<BulletManager>();
        playParticle();
    }
    private void OnEnable()
    {
        playParticle();
    }
    private void OnDisable()
    {

        bulletManager.ReturnToPool(gameObject);
    }
    private void playParticle()
    {
        GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        GetComponent<ParticleSystem>().Play(true);
    }
}
