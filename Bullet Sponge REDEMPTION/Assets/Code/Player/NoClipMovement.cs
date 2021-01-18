using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoClipMovement : MonoBehaviour {
    [Range(1, 20)]
    public int speed;
    public float turnSpeed = 120f;

    public int fontSize = 75;
    public float outline = 15f;
    [Space]
    public float width = 200f;
    public float height = 200f;
    public float x = 100f, y = 100f;

    [Space]
    public float waitForFade = 1f;
    public TextAnchor anchor = TextAnchor.MiddleCenter;

    string text;
    Coroutine lastCoroutine = null;
    float hor, vert, mouseX, mouseY, upDown;
    bool gui = false, buttonDown = false;

    private void Start() {
        speed = PlayerPrefs.GetInt("NoClip", 10);
    }

    private void Update() {
        buttonDown = Input.GetMouseButton(1);
        if (buttonDown) {
            hor = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
            mouseY = -Input.GetAxisRaw("Mouse Y");
            mouseX = Input.GetAxisRaw("Mouse X");
            upDown = Input.GetAxis("NC_UpDown");

            float f = Input.GetAxis("Mouse ScrollWheel");
            if (f!=0) {
                speed += (int)Mathf.Sign(f);
                speed = Mathf.Clamp(speed, 1, 20);
                gui = true;
                text = (speed * .1f).ToString();
                PlayerPrefs.SetInt("NoClip", speed);
                UpdateGUI();
            }
        }
    }

    int GetSprint() {
        int i = 1;
        if (Input.GetButton("Sprint")) {
            i = 2;
        }
        return i;
    }

    private void FixedUpdate() {
        if (buttonDown) {
            Vector3 newPos = new Vector3(hor, upDown, vert) * speed * Time.deltaTime;
            newPos *= GetSprint();
            transform.Translate(newPos);

            Vector3 horRot = new Vector3(mouseY, 0f, 0f) * turnSpeed * Time.deltaTime;
            Vector3 verRot = new Vector3(0f, mouseX, 0f) * turnSpeed * Time.deltaTime;

            transform.Rotate(horRot);
            transform.Rotate(verRot, Space.World);
        }
    }

    void UpdateGUI() {
        if (lastCoroutine != null) {
            StopCoroutine(lastCoroutine);
        }
        lastCoroutine = StartCoroutine(IeUpdateGUI());
    }

    IEnumerator IeUpdateGUI() {
        yield return new WaitForSeconds(waitForFade);
        gui = false;
    }

    private void OnGUI() {
        if (gui) {
            GUIStyle style = new GUIStyle();
            style.fontSize = fontSize;
            style.alignment = anchor;
            style.fixedHeight = height;
            style.fixedWidth = width;
            style.normal.textColor = Color.white;

            float w = Screen.width / 2;
            float h = Screen.height / 2;
            Rect rect = new Rect(w - x, h - y, 0f, 0f);

            DrawOutline(rect, text, style, Color.black, Color.white, outline);
        }
    }

    public void DrawOutline(Rect rect, string text, GUIStyle style, Color outColor, Color inColor, float size) {
        float halfSize = size * 0.5F;
        GUIStyle backupStyle = new GUIStyle(style);
        Color backupColor = GUI.color;

        backupStyle.normal.textColor = outColor;
        GUI.color = outColor;

        rect.x -= halfSize;
        GUI.Label(rect, text, style);

        rect.x += size;
        GUI.Label(rect, text, style);

        rect.x -= halfSize;
        rect.y -= halfSize;
        GUI.Label(rect, text, style);

        rect.y += size;
        GUI.Label(rect, text, style);

        rect.y -= halfSize;
        style.normal.textColor = inColor;
        GUI.skin.box = style;
        GUI.color = backupColor;
        GUI.Label(rect, text, style);

        style = backupStyle;
    }
}