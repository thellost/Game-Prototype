using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TutorialText;


    private Queue<string> sentences;

    private PlayerInput playeInput;
    private CharacterAttack playeAttack;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Awake()
    {
        playeInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        playeAttack = GameObject.Find("Player").GetComponent<CharacterAttack>();
    }

    public void StartTutorial (Tutorial tutorial)
    {
        Debug.Log("Starting tutorial");

        sentences.Clear();

        foreach (string sentence in tutorial.sentences)
        {
            sentences.Enqueue(sentence);
        }

        playeInput.enabled = false;
        playeAttack.enabled = false;

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
        TutorialText.text = sentence;
    }

    void EndTutorial()
    {
        Debug.Log("End Tutorial");

        playeInput.enabled = true;
        playeAttack.enabled = true;
    }
}
