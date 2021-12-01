using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerLocation : MonoBehaviour
{
    private void Start()
    {
        SavePlayerLoc();
    }

    private void SavePlayerLoc()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
    }
    
}
