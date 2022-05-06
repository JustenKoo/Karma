// File Name: PlayerHealth.cs
// Author: Justen Koo
// Created: January 5, 2022
// Last Updated January 5, 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public KarmaManager km;

    // Fields
    [SerializeField] private int currHealth = 100;
    [SerializeField] private int maxHealth = 100;
    public enum enemyType
    {
        UNITIALIZED,
        CITIZEN,
        PURIST,
        VIGILANTE
    }
    public enemyType enemy_type = enemyType.UNITIALIZED;

    // Functions
    private void Start()
    {
        km = GameObject.Find("KarmaManager").GetComponent<KarmaManager>();
    }

    public int getCurrHealth() { return currHealth; }
    public void updateCurrHealth(int amt)
    {
        if ((currHealth + amt) >= maxHealth)
        {
            currHealth = maxHealth;
        }
        else if ((currHealth + amt) <= 0)
        {
            handleDeath();
        }
        else
        {
            currHealth += amt;
        }
    }
    public int getMaxHealth() { return maxHealth; }
    public void setMaxHealth(int amt) { maxHealth += amt; }

    // Collisions for Reflecting Damage
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Item")
        {
            switch (collision.collider.name)
            {
                case "Small Health":
                    updateCurrHealth(15);
                    break;
                case "Medium Health":
                    updateCurrHealth(25);
                    break;
                case "Large Health":
                    updateCurrHealth(50);
                    break;
                case "Full Health":
                    updateCurrHealth(maxHealth);
                    break;
            }
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "Hazard")
        {
            updateCurrHealth(-10);
        }

        if (collision.collider.tag == "PlayerProjectile")
        {
            Debug.Log("Enemy Shot!");
            updateCurrHealth(-25);
            switch (collision.collider.name)
            {
                case "SoulBlastProjectile(Clone)":
                    updateCurrHealth(-50);
                    break;
                case "Bullet(Clone)":
                    Debug.Log("Enemy Shot!");
                    updateCurrHealth(-25);
                    break;
            }
        }

        if (collision.collider.name == "Bullet" || collision.collider.name == "Bullet(Clone)")
        {
            Debug.Log("Enemy Shot!");
            updateCurrHealth(-25);
        }
    }

    private int CalculateKarmaValue()
    {
        int value = 0;
        return value;
    }

    private void handleDeath()
    {
        currHealth = 0;
        Destroy(gameObject);
        km.updateKarmaPoints(CalculateKarmaValue());
    }
}
