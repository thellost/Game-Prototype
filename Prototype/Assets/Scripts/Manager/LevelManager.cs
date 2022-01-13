using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    public List<string> namaLevel;
    public List<Transform> posisiLevelIndex0;
    public List<Transform> posisiLevelIndex1;

    public GameObject player;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Awake()
    {
        int doorIndex = GameManager.Progress.lastDoorIndex;
        for (int i = 0; i < namaLevel.Count; i++)
        {
            if (namaLevel[i] == GameManager.Instance.GetPreviousLevel())
            {
                if(doorIndex == 0)
                {

                    player.transform.position = posisiLevelIndex0[i].position;
                }
                else
                {

                    player.transform.position = posisiLevelIndex1[i].position;
                }
            }
        }

        GameManager.Instance.SetLevel();
    }

    /*[SerializeField] private List<Transform> availableSpawnLocation = new List<Transform>();
    public GameObject initialPlayer;
    private GameManager gameManager;
    private Vector3 startLocation;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        startLocation = availableSpawnLocation[0].position;
        gameManager.CreatePlayer(initialPlayer, startLocation);
    }*/
}
