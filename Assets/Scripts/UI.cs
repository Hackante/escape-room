using UnityEngine;
using Enums;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _interactBttn;
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _controls;
    [SerializeField] private GameObject DPad;
    [SerializeField] private GameObject Joystick;


    public static UI Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void SetControls(Enums.InputType inputType) {
        switch (inputType) {
            case Enums.InputType.Joystick:
                DPad.SetActive(false);
                Joystick.SetActive(true);
                break;
            case Enums.InputType.DPad:
                DPad.SetActive(true);
                Joystick.SetActive(false);
                break;
            case Enums.InputType.Keyboard:
                DPad.SetActive(false);
                Joystick.SetActive(false);
                break;
            default:
                DPad.SetActive(false);
                Joystick.SetActive(true);
                break;
        }
    }

    public void openSettings() {
        _settingsPanel.SetActive(true);
    }

}
