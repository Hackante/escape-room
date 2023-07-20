using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideElements : TriggerScript
{
    [SerializeField] private GameObject[] elements;
    [SerializeField] private bool hide = true;

    public override void trigger()
    {
        foreach (GameObject element in elements)
        {
            element.SetActive(!hide);
        }
    }   
}
