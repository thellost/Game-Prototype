using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorScene : MonoBehaviour
{

    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 1f;
    [SerializeField] int sceneIndex;
    //ini index door keberapa
    [SerializeField] int doorIndex;
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(LoadLevel(sceneIndex));
        }
        IEnumerator LoadLevel(int index)
        {
            if (transitionAnimator != null)
            {
                transitionAnimator.SetTrigger("start");
            }

            yield return new WaitForSeconds(transitionTime);
            GameManager.Progress.lastDoorIndex = doorIndex;
            GameManager.Save();
            SceneManager.LoadScene(index);
        }
    }
}
