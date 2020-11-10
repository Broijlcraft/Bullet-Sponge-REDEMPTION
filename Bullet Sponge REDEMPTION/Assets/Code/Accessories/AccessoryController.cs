using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.UIElements;
using UnityEngine;

public class AccessoryController : MonoBehaviour {
    public List<AccessoryPoints> accessoryPoints = new List<AccessoryPoints>();
    AccessoryManager am;


    private void Start() {
        am = AccessoryManager.single_AM;
        SwitchAllAccessories();
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            SwitchAllAccessories();
        }
    }

    public void SwitchAllAccessories() {
        if (am) {
            for (int i = 0; i < accessoryPoints.Count; i++) {
                AccessoryPoints ap = accessoryPoints[i];

                if (ap.accessory) {
                    Destroy(ap.accessory);
                }

                int index = Random.Range(0, am.accessories.Count);
                ap.accessory = Instantiate(am.accessories[index], ap.accessoryParent);

                RandomizeMaterial rm = ap.accessory.GetComponent<RandomizeMaterial>();
                if (rm) {
                    rm.Init();
                }

                ap.accessory.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class AccessoryPoints {
    public Transform accessoryParent;
    [HideInInspector]
    public GameObject accessory;
}