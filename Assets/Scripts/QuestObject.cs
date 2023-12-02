using UnityEngine;
using Enums;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quest/QuestObject")]
public class QuestObject : ScriptableObject
{
    [SerializeField] private string questName;
    [SerializeField, TextArea] private string questDescription;
    [SerializeField] private Sprite image;
    [SerializeField] private string[] answers;
    [SerializeField] private Layout layout;
    private List<string> answersGiven = new List<string>();

    public string QuestName { get => questName; }
    public string QuestDescription { get => questDescription; }
    public Sprite Image { get => image; }
    public Layout Layout { get => layout; }
    public List<string> AnswersGiven { get => answersGiven; set => answersGiven = value; }
    public QuestNames saveObjectQuestName;
    public bool CheckAnswer(string answer)
    {
        // Sanitize answer
        
        foreach (string possibleAnswer in answers)
        {
            if (possibleAnswer.ToLower() == answer)
            {
                  answer = answer.Trim().ToLower();
                  answer = answer.Replace(" ", "");
                  answer = answer.Replace(",", ".");
             }
        }

        // Check if answer is in the answers array
        foreach (string possibleAnswer in answers)
        {
            if (possibleAnswer.ToLower() == answer)
            {
                return true; // Return true if answer is correct
            }
        }

        return false; // Return false if answer is incorrect
    }
}