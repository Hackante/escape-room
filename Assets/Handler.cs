using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Handler : MonoBehaviour
{
 
    public void clickedBackButton()
    {
        SceneManager.LoadScene("MainMenue");
    }
}
