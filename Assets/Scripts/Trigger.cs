using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Enums;
using TMPro;

public class Trigger : MonoBehaviour
{
    [HideInInspector] public bool isScript = true;
    [HideInInspector] public bool isText = false;
    [HideInInspector] public bool isSound = false;
    [HideInInspector] public bool isMusic = false;
    [HideInInspector] public bool isAnimation = false;
    [HideInInspector] public bool isDialogue = false;

    [SerializeField] private TriggerType triggerType;
    [SerializeField] private bool triggerOnce = false;
    private bool triggered = false;
    [ConditionalHide("isText", true)] public string text;
    [ConditionalHide("isText", true)] public float textShowDuration = 2f;
    [ConditionalHide("isText", true)] public float textFadeDuration = 0.5f;
    [ConditionalHide("isSound", true)] public AudioClip sound;
    [ConditionalHide("isSound", true)] public float soundVolume = 1f;
    [ConditionalHide("isMusic", true)] public AudioClip music;
    [ConditionalHide("isMusic", true)] public bool musicLoop = true;
    [ConditionalHide("isScript", true)] public TriggerScript script;
    [ConditionalHide("isAnimation", true)] public GameObject animationObject;
    [ConditionalHide("isAnimation", true)] public string animationName;
    [ConditionalHide("isDialogue", true)] public DialogueObject dialogueObject;


    private GameObject textField;

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
                if (!audio) audio = gameObject.AddComponent<AudioSource>();
                audio.clip = sound;
                audio.volume = soundVolume;
                audio.Play();
                break;
            case TriggerType.Music:
                AudioSource musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
                AudioClip originalTrack = null;
                if (!musicLoop) originalTrack = GameObject.Find("Music").GetComponent<AudioSource>().clip;
                musicSource.clip = music;
                musicSource.volume = PlayerPrefs.GetFloat("volume", 1f);
                musicSource.Play();
                if (!musicLoop) StartCoroutine(PlayOriginalTrackAfter(music.length, originalTrack));
                break;
            case TriggerType.Script:
                if (script) script.trigger();
                break;
            case TriggerType.Animation:
                if (animationObject) animationObject.GetComponent<Animator>().Play(animationName);
                break;
            case TriggerType.Dialogue:
                GameObject.Find("UI/Dialogue").GetComponent<DialogueUI>().ShowDialogue(dialogueObject);
                GameObject.Find("Player").GetComponent<Animator>().SetBool("isMoving", false);
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

    private IEnumerator Fade(float seconds)
    {
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

    private IEnumerator PlayOriginalTrackAfter(float seconds, AudioClip originalTrack)
    {
        yield return new WaitForSeconds(seconds);
        if (originalTrack)
        {
            AudioSource musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
            musicSource.clip = originalTrack;
            musicSource.Play();
        }
        else
        {
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();
        }
    }
}

