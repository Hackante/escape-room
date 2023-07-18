using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Transfer : MonoBehaviour
{

    public GameObject point1;
    public GameObject point2;
    public int posRate = 2;


    public GameObject rb;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(posRate % 2 != 0 || posRate == 0)
        {
            posRate++;
            return;
        }
        teleport(point2.transform.position);
        posRate++;


    }

    public void teleport(Vector3 destination)
    {
        rb.transform.position = destination;
    }

}
