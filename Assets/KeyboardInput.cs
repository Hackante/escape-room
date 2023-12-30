using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardInput : MonoBehaviour
{
    private TMP_InputField inputField;
    public SetKeyboardInput Object;
    
    void Awake()
    {
       Object = FindObjectOfType<SetKeyboardInput>();
    }

    // Start is called before the first frame update
    void Start()
    {   
        inputField = Object.GetComponent<SetKeyboardInput>().GetInput();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Input(string input)
    {
        inputField.text += input;
    }

    public void DeleteInput()
    {
        if (inputField != null && inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
}
