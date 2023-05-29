using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SpeakingHandler : MonoBehaviour
{
    [SerializeField] private RectTransform speakingBox;
    [SerializeField] private RectTransform speakingBoxText;
    private GameObject backgroundImage;
    private Dictionary<string, Color> speakerColors = new Dictionary<string, Color>(){
        {"Santa", Color.red},
        {"Special Elf", Color.magenta},
        {"Grinchelbart", Color.green},
        {"Player", Color.blue},
        {"NPC", Color.gray}
    };

    private void Start()
    {
        backgroundImage = speakingBox.gameObject.transform.GetChild(0).gameObject;
    }

    public void ShowSpeaking(DialogueElement dialogueElement)
    {
        if (dialogueElement?.SpeakerName != null && dialogueElement?.SpeakerName != "")
        {
            Color color;
            string speakerName = dialogueElement.SpeakerName;
            try
            {
                color = speakerColors[speakerName.StartsWith("NPC") ? "NPC" : speakerName];
            }
            catch (KeyNotFoundException)
            {
                color = dialogueElement.SpeakerColor;
            }
            speakerName = speakerName.Replace("NPC ", "");

            speakingBoxText.GetComponent<TMP_Text>().color = GetContrastColor(color);

            speakingBox.gameObject.SetActive(true);
            speakingBoxText.GetComponent<TMP_Text>().text = speakerName == "Player" ? PlayerPrefs.GetString("Username", "Du") : speakerName;
            backgroundImage.GetComponent<Image>().color = color;
        }
        else
        {
            HideSpeaking();
        }
    }

    public void HideSpeaking()
    {
        speakingBox.gameObject.SetActive(false);
    }

    public Color GetContrastColor(Color color)
    {
        double contrastWithWhite = ContrastRatio(color, Color.white);
        double contrastWithBlack = ContrastRatio(color, Color.black);
        if (contrastWithWhite > contrastWithBlack)
        {
            return Color.white;
        }
        else
        {
            return Color.black;
        }
    }

    public static double ContrastRatio(Color color1, Color color2)
    {
        double l1 = CalculateRelativeLuminance(color1);
        double l2 = CalculateRelativeLuminance(color2);

        if (l1 > l2)
        {
            return (l1 + 0.05) / (l2 + 0.05);
        }
        else
        {
            return (l2 + 0.05) / (l1 + 0.05);
        }
    }

    public static double CalculateRelativeLuminance(Color color)
    {
        double r = color.r / 255.0;
        double g = color.g / 255.0;
        double b = color.b / 255.0;

        r = ApplyGammaCorrection(r);
        g = ApplyGammaCorrection(g);
        b = ApplyGammaCorrection(b);

        return 0.2126 * r + 0.7152 * g + 0.0722 * b;
    }

    public static double ApplyGammaCorrection(double colorComponent)
    {
        if (colorComponent <= 0.03928)
        {
            return colorComponent / 12.92;
        }
        else
        {
            return Math.Pow((colorComponent + 0.055) / 1.055, 2.4);
        }
    }
}
