using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeMaterial : MonoBehaviour {
    public MeshAndMats[] meshAndMesh = new MeshAndMats[1];
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