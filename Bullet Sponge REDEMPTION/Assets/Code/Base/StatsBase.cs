using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBase : MonoBehaviour
{
    public float damage;
    public float Maxhealth;

    protected float currentHealth;

    protected float GetCurrentHealth()
    {
        float curH = currentHealth;
        return curH;
    }

    protected void SetCurrentHealth(float value)
    {
        currentHealth = value;
    }
}
