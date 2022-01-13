using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    public static bool isActive = false;
    public bool noInput = false;
    
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        isActive = true;
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);

            if ((Input.GetKey(KeyCode.E) && isActive == true) || noInput)
            {
                noInput = false;
                enterDialog();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void enterDialog()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}
