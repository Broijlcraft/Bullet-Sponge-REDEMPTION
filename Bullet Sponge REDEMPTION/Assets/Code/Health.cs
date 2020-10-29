using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [Header("Armor - - - - - - - - - - - - - - - - - - - - - - -")]
    public int startArmor;
    public RangeI armorSettings;
    [Header("Health - - - - - - - - - - - - - - - - - - - - - - -")]
    public int startHealth;
    public RangeI healthSettings;

    [HideInInspector] public bool isAlive;
    int currentArmor, currentHealth;

    private void Start() {
        currentHealth = startHealth;
        currentArmor = startArmor;
        UpdateUI();
        isAlive = true;
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            ChangeHealthAndArmor(-1);
        }
    }

    public int ChangeHealthAndArmor(int value) {
        int damage = 0;
        if (isAlive) {
            if (currentArmor > armorSettings.min) {
                currentArmor += value;
                if (currentArmor < armorSettings.min) {
                    value = currentArmor;
                } else {
                    value = 0;
                }
            }
            currentArmor = Mathf.Clamp(currentArmor, armorSettings.min, armorSettings.max);

            currentHealth += value;
            if (currentHealth <= healthSettings.min) {
                currentHealth = 0;
                OutOfHealth();
            } 

            UpdateUI();
        }
        return damage;
    }

    public virtual void OutOfHealth() {
        isAlive = false;
    }

    public virtual void UpdateUI() {
        CanvasComponents cc = CanvasComponents.single_CC;
        if (cc) {
            cc.armorBar.fillAmount = (float)currentArmor / armorSettings.max;
            cc.healthBar.fillAmount = (float)currentHealth / healthSettings.max;
        }
    }
}