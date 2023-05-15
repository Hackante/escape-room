using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] TMP_Text textLabel;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private GameObject dialogueBox;
    private TypewriterEffect typewriterEffect;
    private ResponseHandler responseHandler;
    [SerializeField] private GameObject[] hideUIElements;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogue();
        //ShowDialogue(dialogueObject);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        HideOtherUI();
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typewriterEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogue();
        }
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        ShowOtherUI();
    }

    private void HideOtherUI()
    {
        foreach (GameObject element in hideUIElements)
        {
            element.SetActive(false);
        }
    }

    private void ShowOtherUI()
    {
        foreach (GameObject element in hideUIElements)
        {
            element.SetActive(true);
        }
    }
}

