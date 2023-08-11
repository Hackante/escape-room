using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class CreateDefaultPlayerPrefs : MonoBehaviour
{
    public CreateDefaultPlayerPrefs()
    {
        // InputType 
        PlayerPrefs.SetInt("InputType", (int)InputType.Joystick);
        // Volume
        PlayerPrefs.SetFloat("volume", 1f);
        // Language
        PlayerPrefs.SetInt("language", (int)Language.de);
        // Save
        PlayerPrefs.Save();
    }
}
