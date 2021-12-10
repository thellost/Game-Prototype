using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class UImanager : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject map;

    private Clock enemyClock, playerClock, bulletClock;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isPaused)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void ToggleActiveMap()
    {
        map.SetActive(!inventory.activeInHierarchy);
    }

    // resume & pause
    private void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
        /*enemyClock.localTimeScale = 0;
        playerClock.localTimeScale = 0;
        bulletClock.localTimeScale = 0;*/
        isPaused = true;
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        /*enemyClock.localTimeScale = 1;
        playerClock.localTimeScale = 1;
        bulletClock.localTimeScale = 1;*/
        isPaused = false;
    }

    // inventory
    private void OpenInventory()
    {
        inventory.SetActive(true);
        Time.timeScale = 0;
        /*enemyClock.localTimeScale = 0;
        playerClock.localTimeScale = 0;
        bulletClock.localTimeScale = 0;*/
        isPaused = true;
    }

    private void CloseInventory()
    {
        inventory.SetActive(false);
        Time.timeScale = 1;
        /*enemyClock.localTimeScale = 1;
        playerClock.localTimeScale = 1;
        bulletClock.localTimeScale = 1;*/
        isPaused = false;
    }
}
