using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroHandler : MonoBehaviour
{
    public GameObject player;
    bool alreadyCollided = false;
    bool started = false;
    public float moveSpeed = 1;

    public bool changeAnimation = false;


    void Start()
    {

    }

    
    void Update()
    {
        if (started && !alreadyCollided)
        { 
            move();
        }
        else if (started && alreadyCollided)
        {
            SceneManager.LoadScene("Elfendorf");
        }
    }

    public void clickedPlayButton()
    {
        started = true;
    }

    public void move()
    {
        player.transform.position = player.transform.position + (Vector3.right*moveSpeed) * Time.deltaTime;
    }

    public void colldided()
    {       
        alreadyCollided = true;
    }

    public void openSettings()
    {
        Debug.Log("Settings opened");
        SceneManager.LoadScene("Settings");
    }
    
    public void openCredits()
    {
        Debug.Log("Credits opened");
        SceneManager.LoadScene("Credits");
    }
}
