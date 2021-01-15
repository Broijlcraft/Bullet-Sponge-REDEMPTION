using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LodControl : MonoBehaviour {
    [SerializeField] private List<LodScript> lods = new List<LodScript>();
    [SerializeField] private float closest = 10f, distanceCheckDelay = 0.001f;

    private void Start() {
        StartCoroutine(DistanceCheck());
    }

    IEnumerator DistanceCheck() {
        while (true) {
            int i;
            for (i = 0; i < lods.Count; i++) {
                yield return new WaitForSeconds(distanceCheckDelay);
                LodScript lod = lods[i];
                if (lod.DistanceBetween(transform.position) < closest) {
                    lod.Appear();
                } else {
                    lod.Disappear();
                }
            }
            i = 0;
            yield return new WaitForSeconds(distanceCheckDelay);
        }
    }
}