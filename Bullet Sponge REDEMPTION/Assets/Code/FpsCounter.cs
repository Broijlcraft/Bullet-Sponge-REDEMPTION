using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCounter : MonoBehaviour {
    private const float updateInterval = 0.5f;
    private float fpsAccum;
    private int fpsFrames;
    private float fpsTimeLeft = updateInterval;
    private float fps;

    private void Update() {
        fpsTimeLeft -= Time.deltaTime;
        fpsAccum += Time.timeScale / Time.deltaTime;
        fpsFrames++;
        if(fpsTimeLeft <= 0) {
            fps = fpsAccum / fpsFrames;
            fpsTimeLeft = updateInterval;
            fpsAccum = 0;
            fpsFrames = 0;
        }
    }

    private void OnGUI() {
        GUILayout.BeginArea(new Rect(20, 20, 500, 500));
        GUILayout.Label("FPS :" + fps.ToString("f1"));
        GUILayout.EndArea();
    }
}