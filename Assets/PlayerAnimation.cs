using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
   
    void Start()
    {
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        
    }

    void change()
    {
        animator.SetBool("isMoving", true);
    }
}
