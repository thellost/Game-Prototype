using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerPrefsDetector : MonoBehaviour
{
    public string prefsName;
    public UnityEvent onTrue;
    public UnityEvent onFalse;

    private void Start()
    {
        CheckPrefs();
    }

    private void CheckPrefs()
    {
        //int isVisited = PlayerPrefs.GetInt(prefsName);

        int tmpVisited = GameManager.Progress.levels.ContainsKey(prefsName) ? 1 : 0;
        int isVisited = tmpVisited;

        if (isVisited == 0)
        {
            onFalse?.Invoke();
        } else
        {
            onTrue?.Invoke();
        }
    }
}
