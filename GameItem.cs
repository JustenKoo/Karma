// File Name: PlayerHealth.cs
// Author: Justen Koo
// Created: ?
// Last Updated January 4, 2022
// Purpose: Applies tags for items

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public GameObject item;
    public string itemName;

    public enum item_category
    {
        UNITIALIZED,
        PRIMARY_WEAPON,
        SECONDARY_WEAPON,
        AMMO,
        CONSUMABLE
    }
    public item_category itemType = item_category.UNITIALIZED;

    public int itemValue;

    // For weapons
    public int itemDamage;

    // For healing items
    public int restorationValue;

    // Ammo
    public enum ammo_type
    {
        UNITIALIZED,
        SNIPER,
        SHOTGUN,
        PISTOL
    }
    public ammo_type ammoType = ammo_type.UNITIALIZED;

    public enum consumable_type
    {
        UNITIALIZED,
        HEALTH,
        SOUL
    }
    public consumable_type consumableType = consumable_type.UNITIALIZED;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 50);
    }
}
