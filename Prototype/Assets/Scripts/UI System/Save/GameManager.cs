using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int lives;
    public Text livesText;
    public Vector3 respawnPoint;

    [HideInInspector] public string prevLevel;
    [HideInInspector] public string currentLevel;

    //private LevelManager levelManager;

    void Start()
    {
        //levelManager = FindObjectOfType<LevelManager>();

        //respawnPoint = Character2DController.instance.transform.position;
        if (livesText != null)
        {
            livesText.text = "x " + lives;
        }
    
    }

    /*public void CreatePlayer(GameObject initialPlayer, Vector3 location)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            foreach(GameObject obj in players)
            {
                Destroy(obj);
            }
        }
        Instantiate(initialPlayer, location, Quaternion.identity);
        initialPlayer.GetComponent<Movement>().InitializePlayer();
    }*/

    public void setLevel()
    {
        prevLevel = currentLevel;
        currentLevel = SceneManager.GetActiveScene().name;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
       // Character2DController.instance.gameObject.SetActive(false);

        lives--;
        livesText.text = "x " + lives;


        yield return new WaitForSeconds(.5f);

        //Character2DController.instance.transform.position = respawnPoint;

        //Character2DController.instance.gameObject.SetActive(true);
    }
}
