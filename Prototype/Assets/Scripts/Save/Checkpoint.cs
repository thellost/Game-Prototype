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
        if (Input.GetKey(KeyCode.E))
        {
            //GameManager.Progress.lastCheckpointIndex = SceneManager.GetActiveScene().buildIndex;
            playerStats.currentHp = playerStats.maxPlayerHP;
            playerStats.currentEnergy = playerStats.maxPlayerEnergy;
            //GameManager.Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnCheckpoint();
        }
    }
}
