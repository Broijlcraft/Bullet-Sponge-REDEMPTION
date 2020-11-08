using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpVelocity;

    private Animator myAnimator;
    private Rigidbody rigid;

    //HideinInspector
    public bool isGrounded;
    public bool isRunning;
    private float hor, ver;
    private float currentSpeed;

    public Animator GetAnimator()
    {
        Animator ownersAnimator = myAnimator;

        if(ownersAnimator == null)
        {
            ownersAnimator = GetComponent<Animator>();
        }
        return ownersAnimator;
    }

    public Rigidbody GetRigidbody()
    {
        Rigidbody ownersRigidbody = rigid;

        if(ownersRigidbody == null)
        {
            ownersRigidbody = GetComponent<Rigidbody>();
        }
        return ownersRigidbody;
    }

    public float GetHor()
    {
        return hor;
    }

    public float GetVer()
    {
        return ver;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void SetHor(float set)
    {
        hor = set;
    }

    public void SetVer(float set)
    {
        ver = set;
    }

    public void SetCurrentSpeed(float set)
    {
        currentSpeed = set;
    }
}
