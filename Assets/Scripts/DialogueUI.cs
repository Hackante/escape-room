using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    [SerializeField] TMP_Text textLabel;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private GameObject dialogueBox;
    private TypewriterEffect typewriterEffect;
    private ResponseHandler responseHandler;
    [SerializeField] private GameObject[] hideUIElements;
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogue();
        ShowDialogue(dialogueObject);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        HideOtherUI();
        if (dialogueBox.GetComponentInChildren<Image>().color.a == 0) StartCoroutine(Fade(dialogueObject));
        else StartCoroutine(StepThroughDialogue(dialogueObject));
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
        StartCoroutine(Fade());
        ShowOtherUI();
    }

    private IEnumerator Fade()
    {
        yield return StartCoroutine(FadeRoutine());
    }

    private IEnumerator Fade(DialogueObject dialogueObject)
    {
        yield return StartCoroutine(FadeRoutine());
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator FadeRoutine()
    {
        float duration = fadeDuration;
        float currentTime = 0f;
        float start = dialogueBox.GetComponentInChildren<Image>().color.a;
        float end = start == 0 ? 1 : 0;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(start, end, currentTime / duration);
            dialogueBox.GetComponentInChildren<Image>().color = new Color(1, 1, 1, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
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
