using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 1f;
    [SerializeField] int sceneIndex;
    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            StartCoroutine(LoadLevel(sceneIndex));
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
