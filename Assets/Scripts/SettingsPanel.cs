using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TMP_Dropdown _inputTypeDropdown;
    [SerializeField] private UnityEvent _onSaveAction;
    [SerializeField] private Action _onCloseAction;
    private Settings _settings;
    [SerializeField] private ModalTrigger _modalTrigger;
    static SettingsPanel _instance;

    public static SettingsPanel Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType(typeof(SettingsPanel)) as SettingsPanel;
            if (!_instance)
            {
                Debug.LogError("There needs to be one active SettingsPanel script on a GameObject in your scene.");
            }
        }

        return _instance;
    }

    private void Start()
    {
        _instance = this;
        _settings = Settings.Instance;
        Close();
    }

    public void OnEnable()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        _inputTypeDropdown.value = PlayerPrefs.GetInt("InputType", 0);
    }

    public void Close()
    {
        _modalTrigger.enabled = false;
        gameObject.SetActive(false);
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Cancel()
    {
        _modalTrigger.enabled = true;
    }

    public void Save()
    {
        Debug.Log("Settings saved");
        _settings.SetVolume(_volumeSlider.value);
        _settings.SetInputType(_inputTypeDropdown.value);
        Close();
    }
}
