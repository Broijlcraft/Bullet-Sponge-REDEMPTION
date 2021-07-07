using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementBase : MonoBehaviour
{
    [Header("Character Movement")]
    public float walkSpeed;
    public float sprintSpeed;

    [Header("Character Jump")]
    public float jumpVelocity;
    public float disFromJump;
    public float disFromGround;
    public Transform bottemOfCharacter;

    [Header("Gravity")]
    public float fallGravityMultiplier;
    public float jumpGravityMultiplier;

    [Header("Misc")]
    [Tooltip("For when you fall off the map")]
    [Range(0f,5f)]
    public float RespawnDelay;

    private Animator myAnimator;
    private Rigidbody rigid;

    [Header("HideinInspector")]
    public bool isRunning;
    public bool isGrounded;

    protected float hor, ver;
    protected float currentSpeed;

    public LayerMask groundLayer;
    public LayerMask actualGoundLayer;

    #region Get & Set

    #region Get
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

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetIsRunning()
    {
        return isRunning;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
    #endregion

    #region Set
    public void SetHor(float setAs)
    {
        hor = setAs;
    }

    public void SetVer(float setAs)
    {
        ver = setAs;
    }

    public void SetCurrentSpeed(float setAs)
    {
        currentSpeed = setAs;
    }

    public void SetIsGrounded(bool setAs)
    {
        isGrounded = setAs;
    }

    public void SetIsRunning(bool setAs)
    {
        isRunning = setAs;
    }
    #endregion

    #endregion

    public bool IsMoving()
    {
        if (Mathf.Approximately(GetHor(), 0) && Mathf.Approximately(GetVer(), 0))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    Vector3 lastPosOnMap = Vector3.zero;
    public bool CheckIfOnGround(Transform offset)
    {
        if (Physics.Raycast(offset.position,Vector3.down, out RaycastHit hit, 100f,actualGoundLayer))
        {
            float dis = Vector3.Distance(hit.point, offset.position);

            if(hit.transform.CompareTag("GroundSpawnPoints"))
            {
                lastPosOnMap = hit.point;
            }

            if(dis <= disFromGround)
            {
                isGrounded = true;
            }
            else if(dis > disFromJump)
            {
                isGrounded = false;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public void OffMap()
    {
        GetRigidbody().velocity = Vector3.zero;
        transform.position = lastPosOnMap;
    }

    public void ApplyGravity()
    {
        Rigidbody myRB = GetRigidbody();
        if (GetRigidbody().velocity.y > 0)
        {
            //going up
            myRB.velocity += Vector3.up * Physics.gravity.y * (jumpGravityMultiplier) * Time.fixedDeltaTime;
        }
        else if (GetRigidbody().velocity.y < 0)
        {
            //going down
            myRB.velocity += Vector3.up * Physics.gravity.y * (fallGravityMultiplier) * Time.fixedDeltaTime;
        }
    }
}
