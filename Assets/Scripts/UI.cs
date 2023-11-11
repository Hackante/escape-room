using UnityEngine;
using Enums;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject controls;
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
        settingsPanel.SetActive(true);
    }

    public void SetInteractBttnInteractable(bool active) {
        interactButton.GetComponent<Button>().interactable = active;
    }
}
