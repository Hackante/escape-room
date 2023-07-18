using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntHandler : MonoBehaviour
{
    public bool clicked = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickedButton()
    {
        Debug.Log("Hello");
        clicked = true;
    }
}
