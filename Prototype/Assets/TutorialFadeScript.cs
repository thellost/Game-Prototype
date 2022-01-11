using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeScript : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("fadeIn");
            anim.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.ResetTrigger("fadeIn"); 
        anim.SetTrigger("fadeOut");
    }


}
