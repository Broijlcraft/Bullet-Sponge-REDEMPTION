using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour {
    [SerializeField] private bool showCursor;
    
    [Space, SerializeField] private Transform model;
    [SerializeField] private Camera mainCam;

    [SerializeField] private float speed = 1f, sprintSpeed = 1f, rotateSpeed = 1f;
    [Range(-90, 90), SerializeField] private float topClamp, bottomClamp;

    [Space, SerializeField] private float hor, ver, currentSpeed, xRotation = 0f;

    private Vector3 modelStartPos;

    private void Start() {
        modelStartPos = model.localPosition;
        if (!showCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update() {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        CheckSprint();
        float horRot = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;
        float verRot = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;

        #region DirtyNeedFIX!!!!!!!!!!
        Vector3 moveTo = new Vector3(hor, 0f, ver) * Time.deltaTime * currentSpeed;

        model.Translate(moveTo);
        Vector3 pos = model.localPosition;
        pos.y = 0f;
        model.localPosition = modelStartPos;
        //transform.position += pos;
        transform.Translate(pos);
        #endregion

        model.Rotate(horRot * Vector3.up);

        xRotation += verRot;

        if (xRotation > topClamp) {
            xRotation = topClamp;
            verRot = 0f;
            ClampXRotation(-topClamp);
        } else if (xRotation < bottomClamp) {
            xRotation = bottomClamp;
            verRot = 0f;
            ClampXRotation(-bottomClamp);
        }

        mainCam.transform.Rotate(Vector3.left * verRot);
    }

    void ClampXRotation(float value) {
        Transform cam = mainCam.transform;
        Vector3 eulerRotation = cam.localEulerAngles;
        eulerRotation.x = value;
        cam.localEulerAngles = eulerRotation;
    }

    void CheckSprint() {
        float howSpeedy = speed;
        if (Input.GetButton("Sprint")) {
            howSpeedy = sprintSpeed;
        }
        currentSpeed = howSpeedy;
    }
}