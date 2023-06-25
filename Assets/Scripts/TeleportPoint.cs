using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public bool IsAvtive = true;
    GameObject otherTpPoint;

    void Start()
    {
        GameObject.Find("Teleport");
    }
}
