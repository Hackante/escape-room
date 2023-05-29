using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] TMP_Text textLabel;
    [SerializeField] private GameObject dialogueBox;

    public bool IsOpen { get; private set; }

    private TypewriterEffect typewriterEffect;
    private ResponseHandler responseHandler;
    [SerializeField] private GameObject[] hideUIElements;
    private SpeakingHandler speakingHandler;
    private bool skipped = false;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        speakingHandler = GetComponent<SpeakingHandler>();
        CloseDialogue();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        HideOtherUI();
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            skipped = false;
            speakingHandler.ShowSpeaking(dialogueObject.getElementOf(i));
            string dialogue = dialogueObject.Dialogue[i];
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || skipped);
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

    private IEnumerator RunTypingEffect(string text)
    {
        typewriterEffect.Run(text, textLabel);
        while (typewriterEffect.IsRunning)
        {
            yield return null;
            if(Input.GetKeyDown(KeyCode.Space) || skipped)
            {
                typewriterEffect.Stop();
                skipped = false;
            }
        }
    }

    public void Skip()
    {
        skipped = true;
    }

    public void CloseDialogue()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        ShowOtherUI();
        speakingHandler.HideSpeaking();
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

