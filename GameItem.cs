// Author: Justen Koo
// Purpose: A base structure of GameItems

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
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

    // For Ammo
    public enum ammo_type
    {
        UNITIALIZED,
        SNIPER,
        SHOTGUN,
        PISTOL
    }
    public ammo_type ammoType = ammo_type.UNITIALIZED;
}
