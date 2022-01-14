using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class UImanager : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject map;
    [SerializeField] Animator animator;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory != null)
            {
                if (isPaused)
                {
                    CloseInventory();
                    animator.SetTrigger("CloseInventory");
                }
                else
                {
                    OpenInventory();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map != null)
            {
                if (isPaused)
                {
                    CloseMap();
                }
                else
                {
                    OpenMap();
                }
            }
        }
    }

    // inventory
    private void OpenInventory()
    {
        inventory.SetActive(true);
        SetPauseTimescale();
    }
    private void CloseInventory()
    {
        inventory.SetActive(false);
        SetResumeTimescale();
    }

    // map
    private void OpenMap()
    {
        map.SetActive(true);
        SetPauseTimescale();
    }
    private void CloseMap()
    {
        map.SetActive(false);
        SetResumeTimescale();
    }

    // set timescale
    public void SetPauseTimescale()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void SetResumeTimescale()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
