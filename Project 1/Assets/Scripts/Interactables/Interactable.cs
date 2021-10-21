using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool playerInRange;
    public SpriteRenderer sprite1;
    public Sprite sprite2;
    public GameObject interactable;
    public DialogueManager dialogueManager;

    private void Start()
    {
        sprite1 = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            SetSprite();
            if (gameObject.CompareTag("Chest"))
            {
                interactable.SetActive(false);
                Destroy(gameObject, 3f);
                dialogueManager.EndDialog();

            }
            else if(gameObject.CompareTag("Door"))
            {
                gameObject.layer = 0;
            }
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
    }

    public void SetSprite()
    {
        sprite1.sprite = sprite2;
    }

}
