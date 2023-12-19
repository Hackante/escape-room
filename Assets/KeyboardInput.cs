using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
    public GameObject Object;
    private InputField inputField;
    // Start is called before the first frame update
    void Start()
    {   
     inputField = Object.GetComponent<SetKeyboardInput>().GetInput();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Input(string input){
        inputField.text += input;
    }

    public void DeleteInput()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
}