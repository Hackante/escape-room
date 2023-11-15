using UnityEngine;
using Enums;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quest/QuestObject")]
public class QuestObject : ScriptableObject
{
    [SerializeField] private string questName;
    [SerializeField, TextArea] private string questDescription;
    [SerializeField] private Sprite image;
    [SerializeField] private string answer;
    [SerializeField] private Layout layout;
    private List<string> answersGiven = new List<string>();

    public string QuestName { get => questName; }
    public string QuestDescription { get => questDescription; }
    public Sprite Image { get => image; }
    public Layout Layout { get => layout; }
    public List<string> AnswersGiven { get => answersGiven; set => answersGiven = value; }
    public QuestNames saveObjectQuestName;
    public bool CheckAnswer(string answer) {
        // Sanitize answer
        answer = answer.Trim().ToLower();
        answer = answer.Replace(" ", "");
        answer = answer.Replace(",", ".");

        // Return true if answer is correct
        return this.answer == answer;
    }
}