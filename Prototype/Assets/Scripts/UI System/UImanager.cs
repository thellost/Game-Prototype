using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject map;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleActiveInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleActivePause();
        }
    }
    public void ToggleActiveInventory()
    {
        inventory.SetActive(!inventory.activeInHierarchy);

    }
    public void ToggleActivePause()
    {
        pause.SetActive(!pause.activeInHierarchy);
        Pause();
    }
    public void ToggleActiveMap()
    {
        map.SetActive(!inventory.activeInHierarchy);
    }

    private void Pause()
    {
        if (pause.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
