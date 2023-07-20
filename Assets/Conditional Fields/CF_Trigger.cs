#if UNITY_EDITOR
using UnityEditor;
using Enums;
using UnityEngine;

[CustomEditor(typeof(Trigger))]
public class CF_Trigger : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Trigger trigger = (Trigger)target;
        trigger.isScript = false;
        trigger.isText = false;
        trigger.isSound = false;
        trigger.isMusic = false;
        trigger.isAnimation = false;
        trigger.isDialogue = false;
        trigger.isTeleport = false;
        switch (trigger.getTriggerType())
        {
            case TriggerType.Text:
                trigger.isText = true;
                break;
            case TriggerType.Sound:
                trigger.isSound = true;
                break;
            case TriggerType.Music:
                trigger.isMusic = true;
                break;
            case TriggerType.Script:
                trigger.isScript = true;
                break;
            case TriggerType.Animation:
                trigger.isAnimation = true;
                break;
            case TriggerType.Dialogue:
                trigger.isDialogue = true;
                break;
            case TriggerType.Teleport:
                trigger.isTeleport = true;
                break;
        }
    }
}
#endif