using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    #region 操作
    [HideInInspector] public float horizontal;
    [HideInInspector] public bool jumpPressed;
    [HideInInspector] public bool jumpHeld;
    [HideInInspector] public bool crouchPressed;
    [HideInInspector] public bool crouchHeld;
    //准备重置输入
    bool readyToClearInput;

    void ProcessInput()
    {
        horizontal += Input.GetAxis("Horizontal");
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);

        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        jumpHeld = jumpHeld || Input.GetButton("Jump");
        crouchPressed = crouchPressed || Input.GetButtonDown("Crouch");
        crouchHeld = crouchHeld || Input.GetButton("Crouch");

    }

    void ClearInput()
    {
        horizontal = 0;
        jumpPressed = false;
        jumpHeld = false;
        crouchPressed = false;
        crouchHeld = false;
    }

    #endregion

    void Update()
    {
        if (GameManager.isPaused)
            return;

        if(readyToClearInput)
        {
            readyToClearInput = false;

            ClearInput();
        }

        ProcessInput();
    }

    void FixedUpdate()
    {
        readyToClearInput = true;
    }

}
