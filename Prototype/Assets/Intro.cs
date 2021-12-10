using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime;
    [SerializeField] float splashTime = 4;
    [SerializeField] DialogueTrigger dialogTrigger;
    [SerializeField] GameObject video;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(endSplashScreen(splashTime));
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
    IEnumerator endSplashScreen(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        transitionAnimator.SetTrigger("start");
        StartCoroutine(startDialog());
    }
    IEnumerator startDialog()
    {
        yield return new WaitForSeconds(2f);

        video.SetActive(false);
        dialogTrigger.enterDialog();
        transitionAnimator.SetTrigger("end");

    }

}
