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
    public int doorNumber;
    private const string PROGRESS_KEY = "Progress";
    public static UserProgressData Progress;


    [HideInInspector] public static string prevLevel;
    [HideInInspector] public static string currentLevel;
    [HideInInspector] public static float currentHP;
    [HideInInspector] public static float currentEnergy;
    //private LevelManager levelManager;

    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(Progress);

        PlayerPrefs.SetString(PROGRESS_KEY, json);

    }

    void Start()
    {
        //levelManager = FindObjectOfType<LevelManager>();

        //respawnPoint = Character2DController.instance.transform.position;
        if (livesText != null)
        {
            livesText.text = "x " + lives;
        }
    
    }

    private void Update()
    {

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

    public void SetLevel()
    {
        prevLevel = currentLevel;
        currentLevel = SceneManager.GetActiveScene().name;
    }

    public void setPlayerStat(float HP, float Energy)
    {
        currentEnergy = Energy;
        currentHP = HP;
    }

    public string GetPreviousLevel()
    {
        return prevLevel;
    }

    public string GetCurrentLevel()
    {
        return currentLevel;
    }

    private void LoadData()
    {
        // Cek apakah ada data yang tersimpan sebagai PROGRESS_KEY

        if (!PlayerPrefs.HasKey(PROGRESS_KEY))

        {

            // Jika tidak ada, maka buat data baru

            Progress = new UserProgressData();

            Save();

        }

        else

        {

            // Jika ada, maka timpa progress dengan yang sebelumnya

            string json = PlayerPrefs.GetString(PROGRESS_KEY);

            Progress = JsonUtility.FromJson<UserProgressData>(json);

        }
        SetLevel();
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
