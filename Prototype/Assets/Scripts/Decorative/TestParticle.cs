using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Awake()
    {
        playParticle();
    }
    private void OnEnable()
    {
        playParticle();
    }
    private void OnDisable()
    {
        
    }
    private void playParticle()
    {
        GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        GetComponent<ParticleSystem>().Play(true);
    }
}
