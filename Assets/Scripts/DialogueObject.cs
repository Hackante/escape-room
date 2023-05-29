using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private DialogueElement[] dialogue;

    [SerializeField] private Response[] responses;

    public string[] Dialogue { get => getDialogueText(); }
    public DialogueElement[] DialogueElements { get => dialogue; }
    public Response[] Responses { get => responses; }

    public bool HasResponses { get => this.responses != null && this.responses.Length > 0; }

    private string[] getDialogueText()
    {
        List<string> dialogueText = new List<string>();
        for (int i = 0; i < dialogue.Length; i++)
        {
            for (int j = 0; j < dialogue[i].Dialogue.Length; j++)
            {
                dialogueText.Add(dialogue[i].Dialogue[j]);
            }
        }
        return dialogueText.ToArray();
    }

    public DialogueElement getElementOf(int index)
    {
        string dialogueText = Dialogue[index];
        for (int i = 0; i < dialogue.Length; i++)
        {
            for (int j = 0; j < dialogue[i].Dialogue.Length; j++)
            {
                if (dialogue[i].Dialogue[j] == dialogueText)
                {
                    return dialogue[i];
                }
            }
        }
        return null;
    }
}
