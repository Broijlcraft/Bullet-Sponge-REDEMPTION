using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3RDPersonControl : MonoBehaviour {

    public float testMagnitute, snapRange;
    public Transform targetHold, target;

    [SerializeField] private Transform camHolder, player;
    [SerializeField] private float mouseSense, walkSpeed, smoothTurnTime, turnSpeed;

    [Header("HideInSpector")]
    [SerializeField] private Vector3 walkDir;
    [SerializeField] private bool invertMouse, rotate;
    [SerializeField] private float hor, ver, horRaw, verRaw, horRot, verRot, turnVelocity;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        horRaw = Input.GetAxisRaw("Horizontal");
        verRaw = Input.GetAxisRaw("Vertical");
        
        horRot = Input.GetAxis("Mouse X");
        verRot = Input.GetAxis("Mouse Y");
        if(horRot > 0) {
            print(horRot * Time.deltaTime * mouseSense);
        }
    }

    private void FixedUpdate() {
        int invert = GetInvertValue();
        Vector3 newRot = new Vector3(0f, horRot, 0f);
        camHolder.Rotate(newRot * invert * mouseSense * Time.deltaTime, Space.World);

        newRot = new Vector3(verRot, 0f, 0f);
        camHolder.Rotate(newRot * -invert * mouseSense * Time.deltaTime);

        walkDir = GetDirection(hor, ver);
        transform.Translate(walkDir * walkSpeed * Time.deltaTime);


        Vector3 inputDir = new Vector3(horRaw, 0f, verRaw).normalized;
        Vector3 cPos = camHolder.localPosition;

        Vector3 tPos = new Vector3(cPos.x+hor, cPos.y, cPos.z+ver);
        Vector3 cRot = new Vector3(0f, camHolder.rotation.eulerAngles.y, 0f);

        target.localPosition = tPos;
        targetHold.rotation = Quaternion.Euler(cRot);

        if(inputDir.magnitude >= testMagnitute) {
            //Vector3 targetRot = Vector3.zero;
            //Vector3 pRot = new Vector3(0, player.rotation.eulerAngles.y, 0f);
            //float angle = Mathf.Atan2(pRot.y, cRot.y) * Mathf.Deg2Rad;
            //if(angle < snapRange) {
            //    targetRot = cRot;
            //} else {

            //}

            //player.transform.rotation = Quaternion.Euler(targetRot);

            Vector3 dir = target.position - player.position;
            Quaternion lookRotation = Quaternion.Euler(Vector3.zero);
            if (dir != Vector3.zero) {
                lookRotation = Quaternion.LookRotation(dir);
            }
            Vector3 newFullRot = Quaternion.Lerp(player.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            Quaternion newPartRotation = Quaternion.Euler(0f, newFullRot.y, 0f);
            player.rotation = newPartRotation;
        }

        //player.transform.rotation = Quaternion.Euler()
    }

    private int GetInvertValue() {
        int invert = 1;
        if (invertMouse) {
            invert = -1;
        }
        return invert;
    }

    public Vector3 GetDirection(float floatX, float floatZ) {
        walkDir = Vector3.zero;

        if (floatX != 0f) {
            if (floatX > 0f) {
                walkDir += camHolder.right * floatX;
            } else {
                walkDir -= camHolder.right * -floatX;
            }
        }

        if (floatZ != 0f) {
            if (floatZ > 0f) {
                walkDir += camHolder.forward * floatZ;
            } else {
                walkDir -= camHolder.forward * -floatZ;
            }
        }
        walkDir.y = 0f;

        return walkDir;
    }
}