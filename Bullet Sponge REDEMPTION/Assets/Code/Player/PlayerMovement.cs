using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBase
{
    bool jump;
    private void Update()
    {
        CheckIfOnGround(bottemOfCharacter);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        CollectInput();
    }
    public void CollectInput()
    {
        SetHor(Input.GetAxis("Horizontal"));
        SetVer(Input.GetAxis("Vertical"));
        
        ApplyGravity();
        ApplyJump();
        ApplyMovement(GetHor(), GetVer());
    }

    public void ApplyMovement(float horizontalInput, float verticalInput)
    {
        Vector3 movement = new Vector3(horizontalInput,0,verticalInput);

        if (RunningCheck())
        {
            transform.Translate(movement * Time.deltaTime * sprintSpeed);
        }
        else
        {
            transform.Translate(movement * Time.deltaTime * walkSpeed);
        }
    }

    public bool RunningCheck()
    {
        if (!IsMoving())
        {
            SetIsRunning(false);
            return false;
        }
        if (MenuManager.single.GetRunningToggle())
        {
            if (Input.GetButtonDown("Sprint")|| GetIsRunning())
            {
                SetIsRunning(true);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Input.GetButton("Sprint"))
            {
                SetIsRunning(true);
                return true;
            }
            else
            {
                SetIsRunning(false);
                return false;
            }
        }
    }

    public void ApplyJump()
    {
        if (jump)
        {
            GetRigidbody().velocity = Vector3.zero;
            GetRigidbody().velocity = Vector3.up * -jumpVelocity * Physics.gravity.y;
            jump = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(bottemOfCharacter.position, Vector3.down * 100f, Color.red);
    }
}
