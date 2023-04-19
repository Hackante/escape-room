using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;


public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }

    public Controltype controltype;
    public float volume;
    public Language language;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Controltype 
        controltype = (Controltype)PlayerPrefs.GetInt("controltype", (int)Controltype.DPad);
        // Volume
        volume = PlayerPrefs.GetFloat("volume", 1f);
        // Language
        language = (Language)PlayerPrefs.GetInt("language", 0);
    }

    public void SetControltype(Controltype controltype)
    {
        this.controltype = controltype;
        PlayerPrefs.SetInt("controltype", (int)controltype);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void SetLanguage(Language language)
    {
        this.language = language;
        PlayerPrefs.SetInt("language", (int)language);
        PlayerPrefs.Save();
    }
}
