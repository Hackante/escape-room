using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{

    public float moveSpeed;
    void Start()
    {
        
    }

 
    void Update()
    {
        transform.position = transform.position + Vector3.up* Time.deltaTime * moveSpeed;
    }
}
