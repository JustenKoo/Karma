// File Name: PlayerHealth.cs
// Author: Justen Koo
// Created: January 4, 2022
// Last Updated January 5, 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Fields
    private int currHealth = 100;
    private int maxHealth = 100;

    // Functions
    public int getCurrHealth() { return currHealth; }
    public void updateCurrHealth(int amt) {
        if ((currHealth + amt) >= maxHealth)
            currHealth = maxHealth;
        else if ((currHealth + amt) <= 0)
            currHealth = 0;
        else
            currHealth += amt;
    }
    public int getMaxHealth() { return maxHealth; }
    public void setMaxHealth(int amt) { maxHealth += amt; }
}