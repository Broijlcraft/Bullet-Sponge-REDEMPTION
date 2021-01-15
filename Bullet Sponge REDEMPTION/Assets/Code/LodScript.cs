using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LodScript : MonoBehaviour {
    [SerializeField] private Animator anim;
    private bool appear, disappear;

    private void Reset() {
        anim = GetComponent<Animator>();
    }

    public float DistanceBetween(Vector3 playerPos) {
        return Vector3.Distance(transform.position, playerPos);
    }

    public void Appear() {
        if (anim) {
            if (!appear) {
                appear = true;
                anim.ResetTrigger("Disappear");
                anim.SetTrigger("Appear");
                disappear = false;
            }
        }
    }

    public void Disappear() {
        if (anim) {
            if (!disappear) {
                disappear = true;
                anim.ResetTrigger("Appear");
                anim.SetTrigger("Disappear");
                appear = false;
            }
        }
    }
}