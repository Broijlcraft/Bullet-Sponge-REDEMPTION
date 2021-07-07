﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBase
{
    public PlayerMode pMode;
    public static PlayerMovement single;
    public GameObject playerModel;
    Vector3 move = Vector3.zero;
    PlayerRotation pRot;

    private void Awake()
    {
        if (!single)
        {
            single = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
         pRot = GetComponent<PlayerRotation>();
    }

    private void Update()
    {
        CollectInputs();
        
        if (CheckIfOnGround(bottemOfCharacter))
        {
            if (IsInvoking(nameof(OffMap)))
            {
                CancelInvoke(nameof(OffMap));
            }
        }
        else
        {
            if (!IsInvoking(nameof(OffMap)))
            {
                Invoke(nameof(OffMap), RespawnDelay);
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ApplyJump();
        }
    }
    private void FixedUpdate()
    {
        ApplyInputs();
        move = pRot.GetDirection(GetHor(), GetVer());
    }

    public void LateUpdate()
    {
        if (!IsMoving() || pMode != PlayerMode.normal)
        {
            Vector3 rot = Quaternion.Slerp(playerModel.transform.rotation, pRot.camHolder.rotation, Time.deltaTime * pRot.rotateSpeed).eulerAngles;
            Quaternion correctPlayerRot = Quaternion.Euler(0f, rot.y, 0f);
            playerModel.transform.rotation = correctPlayerRot;
            return;
        }

        pRot.SetPlayerRotation(move, playerModel);

    }

    private void CollectInputs()
    {
        SetHor(Input.GetAxis("Horizontal"));
        SetVer(Input.GetAxis("Vertical"));
    }

    public void ApplyInputs()
    {
        ApplyGravity();
        ApplyMovement();
    }

    public void ApplyMovement()
    {
        if (RunningCheck())
        {
            TranslateMovement(move,sprintSpeed);
        }
        else
        {
            TranslateMovement(move,walkSpeed);
        }
    }

    public void TranslateMovement(Vector3 dir,float multiplier)
    {
        transform.Translate(dir * Time.deltaTime * multiplier);
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

public enum PlayerMode
{
    normal,aim,fire
}
