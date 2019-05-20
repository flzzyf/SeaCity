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
	[HideInInspector] public bool attackPressed;
    //准备重置输入
    bool readyToClearInput;

    void ProcessInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        jumpHeld = jumpHeld || Input.GetButton("Jump");
        crouchPressed = crouchPressed || Input.GetButtonDown("Crouch");
        crouchHeld = crouchHeld || Input.GetButton("Crouch");
		attackPressed = Input.GetButtonDown("Fire1");
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
