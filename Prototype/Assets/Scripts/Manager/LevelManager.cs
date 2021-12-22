using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<string> namaLevel;
    public List<Transform> posisiLevel;

    public GameObject player;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Awake()
    {
        for (int i = 0; i < namaLevel.Count; i++)
        {
            if (namaLevel[i] == GameManager.Instance.currentLevel)
            {
                posisiLevel[i] = player.transform;
            }
        }
        GameManager.Instance.setLevel();
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
