using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerLocation : MonoBehaviour
{
    private void Start()
    {
        // save player function will called when room scene opened
        SavePlayerLoc();
    }

    private void SavePlayerLoc()
    {
        // get active scene and save scene name to playerprefs
        GameManager.Progress.lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (!GameManager.Progress.levels.ContainsKey(SceneManager.GetActiveScene().name))
        {

            GameManager.Progress.levels.Add(SceneManager.GetActiveScene().name, true);
        }
        //PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
    }
    
}
