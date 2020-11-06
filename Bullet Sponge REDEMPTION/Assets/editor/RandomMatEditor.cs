using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(RandomizeMaterial))]
public class RandomMatEditor : Editor {

    RandomizeMaterial rm;

    public void OnEnable() {
        rm = (RandomizeMaterial)target;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("Set meshrenderers")) {
            int i;
            for (i = 0; i < rm.meshAndMesh.Length; i++) {
                MeshAndMats mm = rm.meshAndMesh[i];
                int rand = Random.Range(0, mm.mats.Length);
                Material[] sharedMaterials = mm.mats[rand].sharedMaterials;

                for (int iB = 0; iB < mm.renderers.Length; iB++) {
                    mm.renderers[iB].sharedMaterials = sharedMaterials;
                }

                EditorUtility.SetDirty(rm);
            }
            Debug.LogWarning($"Successfully randomized {i} meshrenderers, don't forget to save!");
        }
    }
}
#endif