using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeMaterial : MonoBehaviour {
    public MeshAndMats[] meshAndMesh = new MeshAndMats[1];

    public void Init() {
        for (int i = 0; i < meshAndMesh.Length; i++) {
            MeshAndMats mm = meshAndMesh[i];
            int rand = Random.Range(0, mm.mats.Length);
            Material[] sharedMaterials = mm.mats[rand].sharedMaterials;

            for (int iB = 0; iB < mm.renderers.Length; iB++) {
                mm.renderers[iB].sharedMaterials = sharedMaterials;
            }
        }
    }
}

[System.Serializable]
public class MeshAndMats {
    public string meshName = $"Example: {"DoorFrame"}";
    public MeshRenderer[] renderers = new MeshRenderer[1];
    public Mats[] mats = new Mats[1];
}

[System.Serializable]
public class Mats {
    public string materialName = $"Example: {"Dirty"}";
    public Material[] sharedMaterials = new Material[1];
}