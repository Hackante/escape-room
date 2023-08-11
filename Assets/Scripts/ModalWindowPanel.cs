using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalWindowPanel : MonoBehaviour
{
    [Header("Header")]
    [SerializeField] private Transform _headerArea;
    [SerializeField] private TextMeshProUGUI _titleField;

    [Header("Body")]
    [SerializeField] private Transform _bodyArea;
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
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _alternateButton;

    private Action onConfirmAction;
    private Action onDeclineAction;
    private Action onAlternateAction;
    public UnityEvent onContinueCallback;
    public UnityEvent onCancelCallback;
    public UnityEvent onAlternateCallback;
    [SerializeField] private GameObject player;

    private static ModalWindowPanel _instance;

    public static ModalWindowPanel Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType(typeof(ModalWindowPanel)) as ModalWindowPanel;
            if (!_instance)
                Debug.LogError("There needs to be one active ModalWindowPanel script on a GameObject in your scene.");
        }

        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        Close();
    }

    public void Confirm()
    {
        Close();
        onConfirmAction?.Invoke();
    }

    public void Decline()
    {
        Close();
        onDeclineAction?.Invoke();
    }

    public void Alternate()
    {
        Close();
        onAlternateAction?.Invoke();
    }

    public void Close()
    {
        EnablePlayerMovement();
        this.gameObject.SetActive(false);
    }

    public void DisablePlayerMovement()
    {
        player.GetComponent<PlayerController>().canMove = false;
    }

    public void EnablePlayerMovement()
    {
        player.GetComponent<PlayerController>().canMove = true;
    }

    public void ShowAsVertical(string title, string description, Sprite banner, Action onConfirm, Action onDecline,
        Action onAlternate = null, string confirmLabel = "Bestätigen", string declineLabel = "Abbrechen", string alternateLabel = null
        )
    {
        DisablePlayerMovement();
        _headerArea.gameObject.SetActive(string.IsNullOrEmpty(title) == false);
        _titleField.text = title;
        
        _bodyArea.gameObject.SetActive(string.IsNullOrEmpty(description) == false);
        _descriptionText.text = description;
        _bannerImage.gameObject.SetActive(banner != null);
        _bannerImage.sprite = banner;

        _confirmButton.gameObject.SetActive(onConfirm != null);
        _confirmButton.GetComponentInChildren<TextMeshProUGUI>().text = confirmLabel;
        onConfirmAction = onConfirm;

        _cancelButton.gameObject.SetActive(onDecline != null);
        _cancelButton.GetComponentInChildren<TextMeshProUGUI>().text = declineLabel;
        onDeclineAction = onDecline;

        _alternateButton.gameObject.SetActive(onAlternate != null);
        _alternateButton.GetComponentInChildren<TextMeshProUGUI>().text = alternateLabel;
        onAlternateAction = onAlternate;

        _horizontalLayoutArea.gameObject.SetActive(false);
        _verticalLayoutArea.gameObject.SetActive(true);
    }

    public void ShowAsVertical(string title, string description, Sprite banner, Action onConfirm, string confirmLabel)
    {
        ShowAsVertical(title, description, banner, onConfirm, null, null, confirmLabel);
    }

    public void ShowAsVertical(string title, string description, Sprite banner, Action onConfirm)
    {
        ShowAsVertical(title, description, banner, onConfirm, null, null, "Weiter");
    }

    public void ShowAsVertical(string title, string description, Sprite banner, Action onConfirm, Action onDecline,
        string confirmLabel, string declineLabel)
    {
        ShowAsVertical(title, description, banner, onConfirm, onDecline, null, confirmLabel, declineLabel);
    }

    public void ShowAsVertical(string title, string description, Sprite banner, Action onConfirm, Action onDecline)
    {
        ShowAsVertical(title, description, banner, onConfirm, onDecline, null, "Weiter", "Zurück");
    }
}
