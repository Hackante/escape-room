using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Solve : MonoBehaviour
{
    public UnityEvent onContinueCallback;

    public void SolveQuest()
    {
        onContinueCallback?.Invoke();
    }
}
