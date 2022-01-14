using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public Animator anim;
    PlayerStatManager playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCheckpoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Progress.lastCheckpointIndex = SceneManager.GetActiveScene().buildIndex;
            GameManager.Progress.currentHp = playerStats.maxPlayerHP;
            GameManager.Progress.currentEnergy = playerStats.maxPlayerEnergy;
            GameManager.Save();
        }
    }

/*    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            anim.SetTrigger("Hit");

            GameManager.Instance.respawnPoint = transform.position;
        }
    }*/
}
