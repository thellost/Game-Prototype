using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        int isVisited = PlayerPrefs.GetInt(prefsName);
        
        if(isVisited == 0)
        {
            onFalse?.Invoke();
        } else
        {
            onTrue?.Invoke();
        }
    }
}