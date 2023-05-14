
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
        switch (trigger.getTriggerType())
        {
            case TriggerType.Text:
                trigger.text = EditorGUILayout.TextField("Text:", trigger.text);
                trigger.textShowDuration = EditorGUILayout.FloatField("Duration (s):", trigger.textShowDuration);
                trigger.textFadeDuration = EditorGUILayout.FloatField("Fade (s):", trigger.textFadeDuration);
                break;
            case TriggerType.Sound:
                trigger.sound = (AudioClip)EditorGUILayout.ObjectField("Sound:", trigger.sound, typeof(AudioClip), false);
                break;
            case TriggerType.Music:
                trigger.music = (AudioClip)EditorGUILayout.ObjectField("Music:", trigger.music, typeof(AudioClip), false);
                break;
            case TriggerType.Script:
                trigger.script = (MonoScript)EditorGUILayout.ObjectField("Script:", trigger.script, typeof(MonoScript), false);
                break;
            case TriggerType.Animation:
                trigger.anim = (Animation)EditorGUILayout.ObjectField("Animation:", trigger.anim, typeof(Animation), false);
                break;
        }
    }
}
