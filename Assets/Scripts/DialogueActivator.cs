using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private UnityEvent[] closeEvents;
    [SerializeField] private bool useCloseEvents = true;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    public void Interact(PlayerController playerController)
    {
        foreach (DialogueResponseEvents responseEvent in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvent.DialogueObject == dialogueObject)
            {
                playerController.DialogueUI.AddResponseEvents(responseEvent.ResponseEvents);
                break;
            }
        }
        foreach (UnityEvent closeEvent in closeEvents)
        {
            if(useCloseEvents) playerController.DialogueUI.AddCloseEvent(closeEvent);
        }

        playerController.DialogueUI.ShowDialogue(dialogueObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.Interactable = this;
            UI.Instance.SetInteractBttnInteractable(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            if (playerController.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                playerController.Interactable = null;
                UI.Instance.SetInteractBttnInteractable(false);
            }
        }
    }

    public void SetUseCloseEvents(bool useCloseEvents)
    {
        this.useCloseEvents = useCloseEvents;
    }
}
