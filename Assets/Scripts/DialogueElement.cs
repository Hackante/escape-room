using UnityEngine;

[System.Serializable]
public class DialogueElement
{
    [SerializeField, TextArea] private string[] dialogue;
    [SerializeField] private string speakerName;
    [SerializeField] private Color speakerColor = Color.white;

    public string[] Dialogue { get => dialogue; }

    public string SpeakerName { get => speakerName; }
    public Color SpeakerColor { get => speakerColor; }
}
