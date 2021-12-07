using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives;

    public Text livesText;

    public Vector3 respawnPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = Character2DController.instance.transform.position;



        livesText.text = "x " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        Character2DController.instance.gameObject.SetActive(false);

        lives--;
        livesText.text = "x " + lives;





        yield return new WaitForSeconds(.5f);

        Character2DController.instance.transform.position = respawnPoint;

        Character2DController.instance.gameObject.SetActive(true);
    }
}
