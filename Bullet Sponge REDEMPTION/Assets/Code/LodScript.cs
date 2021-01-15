using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LodScript : MonoBehaviour {
    [SerializeField] private Animator anim;

    private void Reset() {
        anim = GetComponent<Animator>();
    }

    public float DistanceBetween(Vector3 playerPos) {
        return Vector3.Distance(transform.position, playerPos);
    }

    public void Appear() {
        if (anim) {
            anim.ResetTrigger("Disappear");
            anim.SetTrigger("Appear");
        }
    }

    public void Disappear() {
        if (anim) {
            anim.ResetTrigger("Appear");
            anim.SetTrigger("Disappear");
        }
    }
}