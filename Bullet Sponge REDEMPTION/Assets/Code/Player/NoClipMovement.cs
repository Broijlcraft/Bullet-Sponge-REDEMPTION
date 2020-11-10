using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoClipMovement : MonoBehaviour {
    public float speed, turnSpeed;

    private void FixedUpdate() {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 newPos = new Vector3(hor, 0, vert) * speed * Time.deltaTime;
        transform.Translate(newPos);

        float mouseY = -Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");
        Vector3 newRot = new Vector3(mouseY, mouseX) * turnSpeed * Time.deltaTime;

        newRot += transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(newRot);
    }
}