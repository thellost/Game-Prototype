using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartTutorial (Tutorial tutorial)
    {
        Debug.Log("Starting tutorial");

        sentences.Clear();

        foreach (string sentence in tutorial.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextTutorial();
    }

    public void DisplayNextTutorial()
    {
        if (sentences.Count == 0)
        {
            EndTutorial();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }

    void EndTutorial()
    {
        Debug.Log("End Tutorial");
    }
}
