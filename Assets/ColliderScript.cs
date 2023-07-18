using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    public IntroHandler handler;
    void Start()
    {
        handler = GameObject.FindGameObjectWithTag("IntroHandler").GetComponent<IntroHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello");
        handler.colldided();
    }
}
