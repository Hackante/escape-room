using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float delay = 1f;

    public void Run(string text, TMP_Text textLabel)
    {
        StartCoroutine(TypeText(text, textLabel));
    }

    private IEnumerator TypeText(string text, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        yield return new WaitForSeconds(delay);

        float t = 0;
        int charIndex = 0;
        
        while (charIndex < text.Length)
        {
            t += Time.deltaTime;
            charIndex = Mathf.FloorToInt(t * speed);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);
            textLabel.text = text.Substring(0, charIndex);
            yield return null;
        }
        textLabel.text = text;
    }
}
