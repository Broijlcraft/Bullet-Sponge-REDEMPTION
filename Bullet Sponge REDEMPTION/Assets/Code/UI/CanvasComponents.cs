using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasComponents : MonoBehaviour {
    public static CanvasComponents single_CC;

    public Image healthBar, armorBar;

    private void Awake() {
        if(single_CC && single_CC != this) {
            Destroy(gameObject);
        } else {
            single_CC = this;
        }
    }
}