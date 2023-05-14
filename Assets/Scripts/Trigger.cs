using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Enums;
using TMPro;

public class Trigger : MonoBehaviour
{
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private bool triggerOnce = false;
    private bool triggered = false;
    [HideInInspector] public string text;
    [HideInInspector] public float textShowDuration = 2f;
    [HideInInspector] public float textFadeDuration = 0.5f;
    [HideInInspector] public AudioClip sound;
    [HideInInspector] public float soundVolume = 1f;
    [HideInInspector] public AudioClip music;
    [HideInInspector] public float musicVolume = 1f;
    [HideInInspector] public bool musicLoop = true;
    [HideInInspector] public Animation anim;
    [HideInInspector] public MonoScript script;
    private GameObject textField;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !triggered)
        {
            trigger();
            if (triggerOnce) triggered = true;
        }
    }

    public void trigger()
    {
        switch (triggerType)
        {
            case TriggerType.Text:
                if (!textField) textField = GameObject.Find("UI/DisplayText");
                TextMeshProUGUI textMesh = textField.GetComponent<TextMeshProUGUI>();
                textMesh.alpha = 0;
                textMesh.SetText(text);
                ShowText();
                HideTextAfter(textShowDuration);
                break;
            case TriggerType.Sound:
                AudioSource audio = GetComponent<AudioSource>();
                if(!audio) audio = gameObject.AddComponent<AudioSource>();
                audio.clip = sound;
                audio.volume = soundVolume;
                audio.Play();
                break;
            case TriggerType.Music:
                Debug.Log(music.name);
                break;
            case TriggerType.Script:
                Debug.Log(script.name);
                break;
            case TriggerType.Animation:
                Debug.Log(anim.name);
                break;
        }
    }
    public TriggerType getTriggerType()
    {
        return triggerType;
    }

    private void HideTextAfter(float seconds)
    {
        StartCoroutine(HideText(seconds));
    }

    private IEnumerator HideText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(Fade(textFadeDuration));
    }

    private void ShowText()
    {
        StartCoroutine(Fade(textFadeDuration));
    }

    private IEnumerator Fade(float seconds) {
        float duration = seconds;
        float currentTime = 0f;
        float start = textField.GetComponent<TextMeshProUGUI>().alpha;
        float end = start == 0 ? 1 : 0;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(start, end, currentTime / duration);
            textField.GetComponent<TextMeshProUGUI>().alpha = alpha;
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}

