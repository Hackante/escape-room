using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static SaveLoad;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private GameObject _questPanel;
    [SerializeField] private QuestObject _questObject;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _player;

    [Header("Header")]
    [SerializeField] private TextMeshProUGUI _titleField;

    [Header("Body")]
    [SerializeField] private Transform _bodyArea;
    [Space()]
    [SerializeField] private Transform _verticalLayoutArea;
    [SerializeField] private Image _bannerImage;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [Space()]
    [SerializeField] private Transform _horizontalLayoutArea;
    [SerializeField] private Transform _iconArea;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _iconText;

    [Header("Footer")]
    [SerializeField] private Transform _footerArea;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private TextMeshProUGUI _footerText;

    [SerializeField] private GameObject Keyboard;

    [Header("Events")]
    public UnityEvent onContinueCallback;

    private void Awake()
    {
        Close();
        _titleField.text = string.Format("Quest - {0}", _questObject.QuestName);
        _descriptionText.text = _questObject.QuestDescription;
        if (_questObject.Image != null)
        {
            _bannerImage.sprite = _questObject.Image;
            _iconImage.sprite = _questObject.Image;
        }
        else
        {
            _bannerImage.gameObject.SetActive(false);
            _iconArea.gameObject.SetActive(false);
        }
        if (_questObject.Layout == Layout.Vertical)
        {
            _verticalLayoutArea.gameObject.SetActive(true);
            _horizontalLayoutArea.gameObject.SetActive(false);
        }
        else
        {
            _verticalLayoutArea.gameObject.SetActive(false);
            _horizontalLayoutArea.gameObject.SetActive(true);
        }
        _iconText.text = _questObject.QuestDescription;
        _confirmButton.onClick.AddListener(OnConfirm);
        _cancelButton.onClick.AddListener(Close);
    }

    public void Close()
    {
        _inputField.text = string.Empty;
        _questPanel.SetActive(false);
        Keyboard.SetActive(false);
    }

    public void Open()
    {
        _footerText.text = "Achtung! Wenn du falsche Antworten gibst, wird dir Strafzeit berechnet.";
        _questPanel.SetActive(true);
    }

    public void OnConfirm()
    {
        if (_questObject.CheckAnswer(_inputField.text))
        {
            onContinueCallback?.Invoke();
            SaveObject x = SaveLoad.Instance.saveObject;
            switch (_questObject.QuestName)
            {
                case "Brücke":
                    x.TaskBrokenBridge = 2;
                    break;
                case "Aufbruch":
                    x.TaskBoatOpened = 2;
                    break;
                case "Schuhe":
                    x.TaskShoesCollected = 2;
                    break;
                case "Kaputtes Schiff":
                    x.TaskHoleFixed = 2;
                    break;
            }
            Close();
        }
        else
        {
            _footerText.text = string.Format("<u>{0}</u> ist leider falsch. Versuche es nochmal.", _inputField.text == string.Empty ? "Nichts..." : _inputField.text);
            // Penalties
            if(_inputField.text != string.Empty) {
                if(!_questObject.AnswersGiven.Contains(_inputField.text)) {
                    _questObject.AnswersGiven.Add(_inputField.text);
                    SaveObject.Instance.wrongAnswers++;
                }
            }
            _inputField.text = string.Empty;
        }
    }
}
