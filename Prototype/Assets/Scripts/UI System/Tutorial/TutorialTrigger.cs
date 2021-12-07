using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    
    public Tutorial tutorial;

    public void TriggerTutorial ()
    {
        FindObjectOfType<TutorialManager>().StartTutorial(tutorial);
    }
}
