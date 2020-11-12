using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerRotation : MonoBehaviour
{
    Vector3 walkDir = Vector3.zero;

    public Transform camHolder;

    public  Vector3 GetDirection(float floatX, float floatZ)
    {
        walkDir = Vector3.zero;

        if (!Mathf.Approximately(floatX, 0))
        {
            if(floatX > 0)
            {
                walkDir += camHolder.right * floatX;
            }
            else
            {
                walkDir -= camHolder.right * -floatX;
            }
        }

        if(!Mathf.Approximately(floatZ, 0))
        {
            if (floatZ > 0)
            {
                walkDir += camHolder.forward * floatZ;
            }
            else
            {
                walkDir -= camHolder.forward * -floatZ;
            }
        }
        walkDir.y = 0;
        walkDir.Normalize();

        return walkDir;
    }

    public void SetPlayerRotation(Vector3 lookDir,GameObject player)
    {
        if (lookDir != Vector3.zero)
        {
            Quaternion dirWeWant = Quaternion.LookRotation(lookDir);
            player.transform.rotation = dirWeWant;
        }
    }

}
