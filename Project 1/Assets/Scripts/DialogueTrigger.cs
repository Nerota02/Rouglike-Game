using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool playerInRange;
    public Dialogue dialogue;
    public DialogueManager dialogueManager;
    public GameObject interactable;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            TriggerDialogue();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactable.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
        interactable.SetActive(false);
        if (dialogueManager.animator.GetBool("isOpen"))
        {
            dialogueManager.EndDialog();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
