using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerTest : TriggerScript
{
    GameObject player = null;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public override void trigger()
    {
        player.GetComponent<PlayerInput>().enabled = false;
        Animator anim = player.GetComponent<Animator>();
        anim.SetBool("isMoving", false);
        anim.SetInteger("facing", 1);
    }
}
