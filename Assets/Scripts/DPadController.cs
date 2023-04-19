using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class DPadController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Vector2 m_DPadVector;
    private bool upPressed = false;
    private bool downPressed = false;
    private bool leftPressed = false;
    private bool rightPressed = false;

    private void FixedUpdate()
    {
        var vec2 = new Vector2(0, 0);
        if (upPressed)
        {
            vec2.y += 1;
        }
        else if (downPressed)
        {
            vec2.y -= 1;
        }
        if (leftPressed)
        {
            vec2.x -= 1;
        }
        else if (rightPressed)
        {
            vec2.x += 1;
        }
        vec2.Normalize();
        if(vec2 != m_DPadVector)
        {
            _player.GetComponent<PlayerController>().Move(vec2);
            m_DPadVector = vec2;
        }
    }

    public void switchLeft()
    {
        leftPressed = !leftPressed;
    }

    public void switchRight()
    {
        rightPressed = !rightPressed;
    }

    public void switchUp()
    {
        upPressed = !upPressed;
    }

    public void switchDown()
    {
        downPressed = !downPressed;
    }
}
