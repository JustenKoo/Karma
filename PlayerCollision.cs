// File Name: PlayerCollision.cs
// Author: Justen Koo
// Created: January 4, 2022
// Last Updated January 4, 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerHealth pHealth;
    public PlayerInventory pInv;
    private SoulManager sm;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pHealth = player.GetComponent<PlayerHealth>();
        pInv = player.GetComponent<PlayerInventory>();
        sm = player.GetComponent<SoulManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Player picks up an item
        // TODO: Make this script load the healValue from the GameItem instance rather than hardcoding it
        if (collision.collider.tag == "Item")
        {
            switch(collision.collider.name)
            {
                case "Small Health":
                    pHealth.updateCurrHealth(15);
                    break;
                case "Medium Health":
                    pHealth.updateCurrHealth(25);
                    break;
                case "Large Health":
                    pHealth.updateCurrHealth(50);
                    break;
                case "Full Health":
                    pHealth.updateCurrHealth(pHealth.getMaxHealth());
                    break;
                case "Soul":
                    sm.UpdateSoul(100);
                    break;

            }
            Destroy(collision.collider.gameObject);
        }

        // Player picks up a weapon
        if(collision.collider.tag == "Weapon")
        {
            pInv.PickupWeapon(collision.collider.gameObject);
            Destroy(collision.collider.gameObject);
        }

        // Player hits a hazardous object or surface
        if (collision.collider.tag == "Hazard")
        {
            pHealth.updateCurrHealth(-10);
        }

        // Player picks up ammo
        // TODO: Make this script load the amount from the GameItem instance rather than hardcoding it
        if (collision.collider.tag == "Ammo")
        {
            switch (collision.collider.name)
            {
                case "AR Ammo Pack":
                    pInv.UpdateAmmo("AR", 30);
                    Debug.Log("Ammo: " + pInv.GetAmmoString("AR Ammo"));
                    break;
                case "SR Ammo":
                    pInv.UpdateAmmo("SR", 10);
                    break;
                case "SG":
                    pInv.UpdateAmmo("SG", 8);
                    break;
                case "Pistol Ammo Pack":
                    pInv.UpdateAmmo("Pistol", 15);
                    break;
            }
            Destroy(collision.collider.gameObject);
        }
    }
}
