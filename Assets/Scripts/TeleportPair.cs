using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPair : MonoBehaviour
{
    public bool IsAvtive = true;
    GameObject[] tpPoints;

    void Start()
    {
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        foreach(Transform transform in transforms) {
            
        }
    }
}
