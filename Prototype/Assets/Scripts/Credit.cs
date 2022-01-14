using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credit : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 27f;
    [SerializeField] GameObject video;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCredit(1));
    }

    IEnumerator StartCredit(int index)
    {
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("end");
        StartCoroutine(endScreen());
    }
    IEnumerator endScreen()
    {

        yield return new WaitForSeconds(1f);

        transitionAnimator.SetTrigger("end");

        yield return new WaitForSeconds(1f);
        
        video.SetActive(false);

        SceneManager.LoadScene(0);

    }

}
