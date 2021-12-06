using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaAI : MonoBehaviour
{
    private EnemyAI enemyParent;
    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyAI>();

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemyParent.setState(EnemyAI.State.ChaseTarget);
            gameObject.SetActive(false);

        }
    }
}
