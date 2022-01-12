using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitch : MonoBehaviour
{

    [SerializeField] Animator animator;
    private bool inPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        inPlayer = true;
    }
    private void OnEnable()
    {
        SwitchState();
    }

    private void OnDisable()
    {
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
