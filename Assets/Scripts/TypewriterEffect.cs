using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float delay = 1f;
    public bool IsRunning { get; private set; }
    private Coroutine runningCoroutine = null;

    private readonly List<Punctuation> punctuations = new List<Punctuation>() {
        new Punctuation(new HashSet<char>(){'.', '!', '?'}, 0.6f),
        new Punctuation(new HashSet<char>(){',', ';', ':'}, 0.3f),
        new Punctuation(new HashSet<char>(){'-', '—'}, 0.4f),
    };

    public void Run(string text, TMP_Text textLabel)
    {
        runningCoroutine = StartCoroutine(TypeText(text, textLabel));
    }

    public void Stop()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            IsRunning = false;
        }
    }

    private IEnumerator TypeText(string text, TMP_Text textLabel)
    {
        IsRunning = true;
        textLabel.text = string.Empty;
        yield return new WaitForSeconds(delay);

        float t = 0;
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime;
            charIndex = Mathf.FloorToInt(t * speed);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLastChar = i == charIndex - 1;
                textLabel.text = text.Substring(0, charIndex);
                if (isPunctuation(text[i], out float delay) && !isLastChar && !isPunctuation(text[i + 1], out _))
                {
                    yield return new WaitForSeconds(delay);
                }
            }
            yield return null;
        }
        IsRunning = false;
    }

    private bool isPunctuation(char c, out float delay)
    {
        foreach (Punctuation punctuation in punctuations)
        {
            if (punctuation.chars.Contains(c))
            {
                delay = punctuation.delay;
                return true;
            }
        }
        delay = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> chars;
        public readonly float delay;

        public Punctuation(HashSet<char> chars, float delay)
        {
            this.chars = chars;
            this.delay = delay;
        }
    }
}
