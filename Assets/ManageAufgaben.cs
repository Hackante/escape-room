using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class ManageAufgaben : MonoBehaviour
{

    public GameObject gameObject;
    public TextMeshProUGUI fragenText;
    public TextMeshProUGUI fragenNummer;
    public string[] fragen;
    public TextMeshProUGUI btn1;
    public TextMeshProUGUI btn2;
    public TextMeshProUGUI btn3;
    public TextMeshProUGUI btn4;

    public GameObject image;

    public string[] antworten = new string[4];

    int rightAnswer = 2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Activated");
        System.Random rnd = new System.Random();
        int randNumber = rnd.Next(0, fragen.Length);
        gameObject.SetActive(true);
        fragenText.text = fragen[randNumber];
        fragenNummer.text = "";
        fragenNummer.text = "Frage " + (randNumber + 1);

        btn1.enabled = true;
        btn2.enabled = true;
        btn3.enabled = true;
        btn4.enabled = true;

        btn1.text = antworten[0];
        btn2.text = antworten[1];
        btn3.text = antworten[2];
        btn4.text = antworten[3];


    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Deactivated");
        gameObject.SetActive(false);
    }

    public void cancelClick()
    {
        gameObject.SetActive(false);
    }

    public void backButton()
    {
        image.SetActive(false);
    }
    public void selectClick()
    {
        Debug.Log("Picture should be tehre");
        image.SetActive(true);
    }

    public bool checkTrue(int number)
    {
        if (number == rightAnswer) return true;
        return false;    
    }

    public void button1()
    {
        bool a = checkTrue(0);
        if(a == false)
        {
            btn1.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void button2()
    {
        bool a = checkTrue(1);

        if (a == false)
        {
            btn2.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void button3()
    {

        bool a = checkTrue(2);
        if (a == false)
        {
            btn3.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void button4()
    {
        bool a = checkTrue(3);
        if (a == false)
        {
            btn4.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
