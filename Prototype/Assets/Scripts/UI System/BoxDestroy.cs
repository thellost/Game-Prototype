using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BoxDestroy : MonoBehaviour
{
    public GameObject brokenboxs;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Slash")
            BreakIt ();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Slash")
            BreakIt ();
    }

    private void BreakIt()
    {
        Destroy(this.gameObject);
        GameObject broke = (GameObject)
        Instantiate(brokenboxs, transform.position, Quaternion.identity);
        foreach (Transform child in broke.transform)
        {

        }
    }
}
