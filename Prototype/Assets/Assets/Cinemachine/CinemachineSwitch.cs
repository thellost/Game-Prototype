using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitch : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] GameObject bossBar;
    private bool inPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        inPlayer = true;
    }
    private void OnEnable()
    {
        if(bossBar != null)
        {
            bossBar.SetActive(true);
        }
        SwitchState();
    }

    private void OnDisable()
    {

        if (bossBar != null)
        {
            bossBar.SetActive(false);
        }
        SwitchState();
    }

    

    public void SwitchState()
    {
        if (inPlayer)
        {
            animator.Play("Boss");
        }
        else
        {
            animator.Play("Player");
        }
        inPlayer = !inPlayer;
    }
}
