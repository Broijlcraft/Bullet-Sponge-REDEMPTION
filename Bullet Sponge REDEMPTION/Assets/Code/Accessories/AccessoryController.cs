using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.UIElements;
using UnityEngine;

public class AccessoryController : MonoBehaviour {
    public Transform accessoryParent;
    GameObject accessory;
    AccessoryManager am;

    private void Start() {
        am = AccessoryManager.single_AM;
        if (am) {
            int index = Random.Range(0, am.accessories.Count);
            accessory = Instantiate(am.accessories[index], accessoryParent);

            RandomizeMaterial rm = accessory.GetComponent<RandomizeMaterial>();
            if (rm) {
                rm.Init();
            }

            accessory.SetActive(true);
        }
    }
}