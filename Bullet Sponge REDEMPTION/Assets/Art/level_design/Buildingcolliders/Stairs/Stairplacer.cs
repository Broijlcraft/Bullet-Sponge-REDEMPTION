using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairplacer : MonoBehaviour
{
    public int stairsToAdd;
    public BoxCollider box;
    public Vector3 offset;
    Transform holder;
    BoxCollider firstBox;
    List<BoxCollider> calls = new List<BoxCollider>();

    void Start()
    {
        //calls.Add(box);
        firstBox = box;
        holder = box.transform;
        for (int i = 0; i < stairsToAdd; i++)
        {
            BoxCollider call = holder.gameObject.AddComponent<BoxCollider>();
            call.center = box.center+offset;
            call.size = box.size;
            box = call;
            calls.Add(box);
        }
    }
    private void Update()
    {
        BoxCollider lastBox = firstBox;
        for (int i = 0; i < calls.Count; i++)
        {
            calls[i].center = lastBox.center + offset;
            lastBox = calls[i];
        }   
    }

}
