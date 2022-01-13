using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActiveGameObject : MonoBehaviour , IOnDialogExit
{

    [SerializeField] GameObject objToActive;
    [SerializeField] GameObject objToDeactive;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    public void DialogExit()
    {
        objToActive.SetActive(true);
        gameObject.SetActive(false);
        objToDeactive.SetActive(false);
    }
}
