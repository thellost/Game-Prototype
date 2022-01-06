using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    [SerializeField] float damage;
    Collider2D hitBox;

    // Start is called before the first frame update
    void Awake()
    {
        hitBox = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStatManager playerStatManager;
            playerStatManager = collision.gameObject.GetComponent<PlayerStatManager>();
            if (playerStatManager != null)
            {

                playerStatManager.takeDamage(damage, transform.position);
            }
        } else if (collision.gameObject.tag == "Ground")
        {
            Destroy(this);
        }
    }

}
