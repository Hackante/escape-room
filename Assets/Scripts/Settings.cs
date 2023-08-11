using UnityEngine;
using Enums;


public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("Volume", 1f));
        SetInputType(PlayerPrefs.GetInt("InputType", 0));

        // Set the volume of all audio sources
        AudioSource[] _sources = FindObjectsOfType<AudioSource>() ?? new AudioSource[0];
        foreach (AudioSource _source in _sources)
        {
            _source.volume = PlayerPrefs.GetFloat("Volume", 1f);
        }
    }

    public void SetInputType(int InputType)
    {
        PlayerPrefs.SetInt("InputType", InputType);
        PlayerPrefs.Save();
        UI.Instance.SetControls((Enums.InputType)InputType);
    }

    public void SetVolume(float volume)
    {
        AudioSource[] _sources = FindObjectsOfType<AudioSource>() ?? new AudioSource[0];
        foreach (AudioSource _source in _sources)
        {
            _source.volume = volume;
        }
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void SetLanguage(Language language)
    {
        PlayerPrefs.SetInt("language", (int)language);
        PlayerPrefs.Save();
    }
}
