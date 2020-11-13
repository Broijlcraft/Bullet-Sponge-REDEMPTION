using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBase
{
    public GameObject playerModel;

    private void Update()
    {
        CollectInputs();

        CheckIfOnGround(bottemOfCharacter);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ApplyJump();
        }
    }
    private void FixedUpdate()
    {
        ApplyInputs();
    }

    private void CollectInputs()
    {
        SetHor(Input.GetAxis("Horizontal"));
        SetVer(Input.GetAxis("Vertical"));
    }

    public void ApplyInputs()
    {
        ApplyGravity();
        ApplyMovement(GetHor(), GetVer());
    }

    public void ApplyMovement(float horizontalInput, float verticalInput)
    {
        PlayerRotation pRot = GetComponent<PlayerRotation>();

        Vector3 move = pRot.GetDirection(horizontalInput, verticalInput);

        if (!IsMoving())
        {
            return;
        }

        pRot.SetPlayerRotation(move, playerModel);

        if (RunningCheck())
        {
            TranslateMovement(sprintSpeed);
        }
        else
        {
            TranslateMovement(sprintSpeed);
        }
    }

    public void TranslateMovement(float multiplier)
    {
        transform.Translate(playerModel.transform.forward * Time.deltaTime * multiplier);
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
            if (Input.GetButtonDown("Sprint") || GetIsRunning())
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
        GetRigidbody().velocity = Vector3.zero;
        GetRigidbody().velocity = Vector3.up * -jumpVelocity * Physics.gravity.y;
    }
}
