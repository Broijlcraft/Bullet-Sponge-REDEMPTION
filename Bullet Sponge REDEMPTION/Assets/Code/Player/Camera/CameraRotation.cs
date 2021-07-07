using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("CameraSettings")]
    public float mouseRotateSpeed;
    public float minFOV;
    public float zoomAmount;
    float maxFOV;

    [Header("Clamp Settings")]
    public float topClamp = 50f;
    public float botClamp = 50f;

    [Header("Misc")]
    Camera mainCam;
    public GameObject camHolder;
    float xAxisClamp;
    public bool mouseClamp;
    public float mouseXSensitivity, mouseYSensitivity;

    private bool maxZoomHit;
    protected float cHor, cVer;
    protected bool mouseXInvert, mouseYInvert;

    #region Get & Set

    #region Get
    public float GetHorizontalAxis()
    {
        float hor = cHor;
        return hor;
    }
    public float GetVerticalAxis()
    {
        float ver = cVer;
        return ver;
    }

    public float GetXSensitivity()
    {
        float xSens = mouseXSensitivity;
        return xSens;
    }

    public float GetYSensitivity()
    {
        float ySens = mouseYSensitivity;
        return ySens;
    }

    public bool GetXInvert()
    {
        bool xInvert = mouseXInvert;
        return xInvert;
    }

    public bool GetYInvert()
    {
        bool yInvert = mouseYInvert;
        return yInvert;
    }

    #endregion

    #region Set
    public void SetHorizontalAxis(float value)
    {
        cHor = value;
    }

    public void SetVerticalAxis(float value)
    {
        cVer = value;
    }

    public void SetMouseXSensitivity(float value)
    {
        mouseXSensitivity = value;
    }

    public void SetMouseYSensitivity(float value)
    {
        mouseYSensitivity = value;
    }

    public void SetMouseXInvert(bool value)
    {
        mouseYInvert = value;
    }
    public void SetMouseYInvert(bool value)
    {
        mouseXInvert = value;
    }

    #endregion

    #endregion


    private void Start()
    {
        mainCam = Camera.main;
        maxFOV = mainCam.fieldOfView;
        if (mouseClamp)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        switch (PlayerMovement.single.pMode)
        {
            case PlayerMode.normal:
                if (mainCam.fieldOfView < maxFOV && !maxZoomHit)
                {
                    ZoomIn();
                }
                else
                {
                    maxZoomHit = true;
                }
                break;
            case PlayerMode.aim:
                if (mainCam.fieldOfView > minFOV)
                {
                    ZoomOut();
                    maxZoomHit = false;
                }
                break;
        }
        GetMouseInput();
        CheckForInvert();
    }

    void FixedUpdate()
    {
        ApplyMouseRotation();
    }

    public void  GetMouseInput()
    {
        SetHorizontalAxis(Input.GetAxis("Mouse X") * GetXSensitivity() * Time.deltaTime * mouseRotateSpeed);
        SetVerticalAxis(Input.GetAxis("Mouse Y") * GetYSensitivity() * Time.deltaTime * mouseRotateSpeed);
    }

    public void ZoomIn()
    {
        mainCam.fieldOfView += zoomAmount * Time.deltaTime;
    }

    public void ZoomOut()
    {
        mainCam.fieldOfView -= zoomAmount * Time.deltaTime;
    }

    private void CheckForInvert()
    {
        if (GetXInvert())
        {
            SetHorizontalAxis(-GetHorizontalAxis());
        }
        else
        {
            SetHorizontalAxis(GetHorizontalAxis());
        }
        if (GetYInvert())
        {
            SetVerticalAxis(-GetVerticalAxis());
        }
        else
        {
            SetVerticalAxis(GetVerticalAxis());
        }
    }

    private void ApplyMouseRotation()
    {
        xAxisClamp += GetVerticalAxis();

        camHolder.transform.Rotate(Vector3.up * GetHorizontalAxis(), Space.World);

        CameraClamp();

        camHolder.transform.Rotate(Vector3.left * GetVerticalAxis());
    }

    private void CameraClamp()
    {
        if(xAxisClamp > botClamp)
        {
            xAxisClamp = botClamp;
            SetVerticalAxis(0f);
            ClampXAxisRotationToValue(-botClamp);
        }else if (xAxisClamp < -topClamp)
        {
            xAxisClamp = -topClamp;
            SetVerticalAxis(0f);
            ClampXAxisRotationToValue(topClamp);
        }
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = camHolder.transform.localEulerAngles;
        eulerRotation.x = value;
        camHolder.transform.localEulerAngles = eulerRotation;
    }
}
