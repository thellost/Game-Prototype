using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaDrone : MonoBehaviour
{
    private DroneAI enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<DroneAI>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemyParent.isAlerted = true;
            gameObject.SetActive(false);

        }
    }
}
