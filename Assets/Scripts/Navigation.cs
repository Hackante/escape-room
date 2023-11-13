using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    [SerializeField] private teleport Teleport;

    public void SetDestination(string SceneName)
    {
        Teleport.SetSceneName(SceneName);
        StartCoroutine(Teleport.LoadScene());
    }
}
