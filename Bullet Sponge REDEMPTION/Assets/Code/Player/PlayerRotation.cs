using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerRotation : MonoBehaviour
{
    Vector3 walkDir = Vector3.zero;

    public float rotateSpeed;
    public Transform camHolder;

    public  Vector3 GetDirection(float floatX, float floatZ)
    {
        walkDir = Vector3.zero;

        if (floatX != 0f)
        {
            if(floatX > 0f)
            {
                walkDir += camHolder.right * floatX;
            }
            else
            {
                walkDir -= camHolder.right * -floatX;
            }
        }

        if(floatZ != 0f)
        {
            if (floatZ > 0f)
            {
                walkDir += camHolder.forward * floatZ;
            }
            else
            {
                walkDir -= camHolder.forward * -floatZ;
            }
        }
        walkDir.y = 0f;
        walkDir.Normalize();

        return walkDir;
    }

    public void SetPlayerRotation(Vector3 lookDir,GameObject player)
    {
        if (lookDir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            Vector3 rot = Quaternion.Slerp(player.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
            Quaternion correctPlayerRot = Quaternion.Euler(rot.x, rot.y, 0f);
            player.transform.rotation = correctPlayerRot;
        }
    }

}
