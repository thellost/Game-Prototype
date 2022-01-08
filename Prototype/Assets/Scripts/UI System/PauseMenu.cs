using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    [SerializeField] Slider VolumeMusic;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }    
    public void QuitGame()
    {
        Application.Quit();

    }

    public void ChangeVolume()
    {
        AudioListener.volume = VolumeMusic.value;
        save();
    }
    private void load()
    {
        VolumeMusic.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void save()
    {
        PlayerPrefs.SetFloat("musicVolume", VolumeMusic.value);
    }
}
