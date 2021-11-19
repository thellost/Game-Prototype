using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class TestEnemyMovement : MonoBehaviour
{
    private Timeline time;

    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<Timeline>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * time.deltaTime);
        
    }
}
