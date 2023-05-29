using UnityEngine;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    public void Interact(PlayerController playerController)
    {
        foreach(DialogueResponseEvents responseEvent in GetComponents<DialogueResponseEvents>()) 
        {
            if(responseEvent.DialogueObject == dialogueObject)
            {
                playerController.DialogueUI.AddResponseEvents(responseEvent.ResponseEvents);
                break;
            }
        }

        playerController.DialogueUI.ShowDialogue(dialogueObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.Interactable = this;
            GameObject.Find("UI/InteractBttn").GetComponent<Button>().interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            if (playerController.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                playerController.Interactable = null;
                GameObject.Find("UI/InteractBttn").GetComponent<Button>().interactable = false;
            }
        }
    }
}
