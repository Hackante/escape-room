using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class CreateDefaultPlayerPrefs : MonoBehaviour
{
    public CreateDefaultPlayerPrefs()
    {
        // Controltype 
        PlayerPrefs.SetInt("controltype", (int)Controltype.DPad);
        // Volume
        PlayerPrefs.SetFloat("volume", 1f);
        // Language
        PlayerPrefs.SetInt("language", (int)Language.de);
        // Save
        PlayerPrefs.Save();
    }
}
