using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class GenerateBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float interval = 2;

    private float tempTime;
    private Timeline time;

    private void Start()
    {
        time = GetComponent<Timeline>();
        tempTime = 0;  
    }
    private void Update()
    {
        tempTime += time.deltaTime;
        if (interval < tempTime)
        {
            tempTime = 0;
            bullet = Instantiate(bullet);
            bullet.transform.position = transform.position;
        }
    }
}
