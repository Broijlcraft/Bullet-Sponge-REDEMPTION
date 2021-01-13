using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryManager : MonoBehaviour {
    public static AccessoryManager single_AM;

    Object[] accessoryOB;
    public List<GameObject> accessories = new List<GameObject>();
    
    private void Awake() {
        if (single_AM) {
            Destroy(gameObject);
        } else {
            single_AM = this;
        }
        Init();
    }

    public void Init() {
        accessoryOB = Resources.LoadAll("Accessories", typeof(SO_Accessory));

        for (int i = 0; i < accessoryOB.Length; i++) {
            SO_Accessory accessory = accessoryOB[i] as SO_Accessory;
            GameObject accessoryGO = Instantiate(accessory.model, transform);

            accessoryGO.SetActive(false);
            accessories.Add(accessoryGO);
        }        
    }
}