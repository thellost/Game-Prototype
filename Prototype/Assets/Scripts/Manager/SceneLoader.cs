using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 1f;
    private void Start()
    {
        transitionAnimator = GameObject.Find("Crossfade").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PrevRoom")
        {
           StartCoroutine( LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
        }
        
        else if (collision.gameObject.tag == "NextRoom")
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int index)
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("start");
        }
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }

}
