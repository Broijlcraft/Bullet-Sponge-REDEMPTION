using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBase
{
    private void Update()
    {
        CollectInput();
    }
    public void CollectInput()
    {
        SetHor(Input.GetAxis("Horizontal"));
        SetVer(Input.GetAxis("Vertical"));

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
            isRunning = false;
            return false;
        }
        if (MenuManager.single.GetRunningToggle())
        {
            if (Input.GetButtonDown("Sprint")|| isRunning)
            {
                isRunning = true;
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
                isRunning = true;
                return true;
            }
            else
            {
                isRunning = false;
                return false;
            }
        }
    }

    public bool IsMoving()
    {
        Debug.Log(Mathf.Approximately(GetHor(), 0));
        Debug.Log(Mathf.Approximately(GetVer(), 0));
        if(Mathf.Approximately(GetHor(), 0) && Mathf.Approximately(GetVer(), 0))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
