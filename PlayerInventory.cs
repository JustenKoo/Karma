// Author: Justen Koo
// Purpose: holds the information for the player's inventory

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int sniperAmmo = 0;
    private GameObject sniperAmmoObj;

    private int shotgunAmmo = 0;
    private GameObject shotgunAmmoObj;

    private int pistolAmmo = 0;
    private GameObject pistolAmmoObj;

    // Start is called before the first frame update
    void Start()
    {
        sniperAmmoObj = (GameObject)Resources.Load("Assets/Assets/SniperAmmoTest", typeof(GameObject));
    }

    public void UpdateInventory(GameItem item, int n)
    {
        if(item.itemType == GameItem.item_category.AMMO)
        {
            if (item.ammoType == GameItem.ammo_type.SNIPER)
                sniperAmmo += n;
            else if (item.ammoType == GameItem.ammo_type.SHOTGUN)
                shotgunAmmo += n;
            else if (item.ammoType == GameItem.ammo_type.PISTOL)
                pistolAmmo += n;
        }
        else if(item.itemType == GameItem.item_category.CONSUMABLE)
        {

        }
        else if(item.itemType == GameItem.item_category.PRIMARY_WEAPON)
        {

        }
        else if(item.itemType == GameItem.item_category.SECONDARY_WEAPON)
        {

        }
    }
}
