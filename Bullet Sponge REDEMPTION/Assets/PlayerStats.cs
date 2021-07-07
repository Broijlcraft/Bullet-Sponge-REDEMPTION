using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : StatsBase
{
    public static PlayerStats single;
    private void Awake()
    {
        if (!single)
        {
            single = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetCurrentHealth() <= 0)
        {
            Death();
        }
    }
}
