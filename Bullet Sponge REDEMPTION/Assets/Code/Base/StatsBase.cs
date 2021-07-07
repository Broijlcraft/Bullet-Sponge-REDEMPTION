using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBase : MonoBehaviour
{
    public float maxHealth;
    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        float curH = currentHealth;
        return curH;
    }

    public void SetCurrentHealth(float value)
    {
        currentHealth = value;
    }
   
    protected virtual void Death()
    {
        //Todo 
        //Add Particles
        //Add Animation

        Debug.Log(gameObject.name + " Died");
        //Destroy(gameObject);
    }
}
