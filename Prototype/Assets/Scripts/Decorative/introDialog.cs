using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class introDialog : MonoBehaviour, IOnDialogExit
{

    [SerializeField] int nextIndexScene;
    private void Awake()
    {
        if(nextIndexScene == null)
        {
            nextIndexScene = SceneManager.GetActiveScene().buildIndex + 1;
        }
    }
    public void DialogExit()
    {
        SceneManager.LoadScene(nextIndexScene);
    }
}
