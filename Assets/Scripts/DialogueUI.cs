using UnityEngine;
using TMPro;
using System.Collections;

public class DialogUI : MonoBehaviour
{
    [SerializeField] TMP_Text textLabel;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private GameObject dialogueBox;
    private TypewriterEffect typewriterEffect;
    [SerializeField] private GameObject[] hideUIElements;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogue();
        ShowDialogue(dialogueObject);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        HideOtherUI();
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        CloseDialogue();
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
