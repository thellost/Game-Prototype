using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionCredit : MonoBehaviour, IOnDialogExit
{

    [SerializeField] Animator transitionAnimator;
    public void DialogExit()
    {
        StartCoroutine(endSequence());
    }

    private IEnumerator endSequence()
    {


        transitionAnimator.SetTrigger("end");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(11);
    }

}
