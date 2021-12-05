using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private Enemy_behaviour enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_behaviour>();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;

        }
    }
    private void OntriggerExit2D(Collider2D collider)
    {
        inRange = false;
        gameObject.SetActive(false);
        enemyParent.triggerArea.SetActive(true);
        enemyParent.inRange = false;
        enemyParent.SelectTarget();

    }
}
