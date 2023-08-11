using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenueHandler : MonoBehaviour
{
    [SerializeField] private Animator crossFade;
    [SerializeField] private float delay = 1f;
    [SerializeField] private TriggerScript hideElements;

    public void openSettings()
    {
        Debug.Log("Settings opened");
    }

    public void openCredits()
    {
        crossFade.SetTrigger("Start");
        hideElements.trigger();
        StartCoroutine(LoadScene("Credits"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void OpenMainMenue()
    {
        crossFade.SetTrigger("Start");
        StartCoroutine(LoadScene("MainMenue"));
        hideElements.trigger();
    }
}
