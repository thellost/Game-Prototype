using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAITrigger : MonoBehaviour
{
    [SerializeField] EnemyStat stats;
    Collider2D hitBox;

    // Start is called before the first frame update
    void Awake()
    {
        hitBox = GetComponent<Collider2D>();  
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stats.dead)
        {
            this.enabled = false;
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerStatManager playerStatManager;
            playerStatManager = collision.gameObject.GetComponent<PlayerStatManager>();
            if (playerStatManager != null)
            {

                playerStatManager.takeDamage(stats.damage, transform.position);
            }
        }
    }

}
