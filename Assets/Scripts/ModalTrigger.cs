using System;
using UnityEngine;
using UnityEngine.Events;

public class ModalTrigger : MonoBehaviour
{
    public string title;
    [TextArea] public string description;
    public Sprite banner;
    public bool triggerOnEnable;
    public UnityEvent onContinueEvent;
    public UnityEvent onCancelEvent;
    public UnityEvent onAlternateEvent;

    private void OnEnable()
    {
        if (!triggerOnEnable) return;

        ShowModal();

        Action continueCallback = null;
        Action cancelCallback = null;
        Action alternateCallback = null;

        if (onContinueEvent.GetPersistentEventCount() > 0)
        {
            continueCallback = onContinueEvent.Invoke;
        }
        if (onCancelEvent.GetPersistentEventCount() > 0)
        {
            cancelCallback = onCancelEvent.Invoke;
        }
        if (onAlternateEvent.GetPersistentEventCount() > 0)
        {
            alternateCallback = onAlternateEvent.Invoke;
        }

        ModalWindowPanel.Instance().ShowAsVertical(title, description, banner, continueCallback, cancelCallback, alternateCallback);
    }

    public void ShowModal()
    {
        ModalWindowPanel.Instance().gameObject.SetActive(true);
    }
}
