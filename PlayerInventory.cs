// Author: Justen Koo
// Purpose: holds the information for the player's inventory

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int sniperAmmo = 0;
    private int shotgunAmmo = 0;
    private int pistolAmmo = 0;
    private int arAmmo = 0;

    private GameObject handsEmpty;
    private GameObject handsAR;
    private GameObject handsPistol;

    // Assignable Weapon Slots
    [SerializeField] private GameObject primaryWeapon;
    [SerializeField] private GameObject secondaryWeapon;
    [SerializeField] private GameObject meleeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        handsEmpty = (GameObject)Resources.Load("Hands_Empty", typeof(GameObject));
        handsAR = (GameObject)Resources.Load("Hands_AR", typeof(GameObject));
        handsPistol = (GameObject)Resources.Load("Hands_Gun", typeof(GameObject));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void PickupWeapon(GameObject item)
    {
        switch (item.name)
        {
            case "AR":
                primaryWeapon = handsAR;
                break;
            case "Pistol":
                secondaryWeapon = handsPistol;
                break;
        }
    }

    public GameObject GetWeapon(string slot)
    {
        GameObject weapon = handsEmpty;
        
        switch(slot)
        {
            case "Primary":
                weapon = primaryWeapon;
                break;
            case "Secondary":
                weapon = secondaryWeapon;
                break;
            case "Melee":
                weapon = meleeWeapon;
                break;
        }

        return weapon;
    }

    /// <summary>
    /// Unassigns the weapon in the weapon slot, allowing a new weapon to be assigned
    /// </summary>
    /// <param name="slot"></param>
    public void UnassignWeapon(string slot, GameObject item)
    {
        switch(slot)
        {
            case "Primary":
                primaryWeapon = null;
                break;
            case "Secondary":
                secondaryWeapon = null;
                break;
            case "Melee":
                meleeWeapon = null;
                break;
        }
        item.GetComponent<WeaponData>().setEquipStatus(false);
    }

    /// <summary>
    /// Updates the ammo when the player picks up an ammo box
    /// </summary>
    /// <param name="ammoType"></param>
    /// <param name="amount"></param>
    public void UpdateAmmo(string ammoType, int amount)
    {
        switch (ammoType)
        {
            case "AR":
                arAmmo += amount;
                if (arAmmo < 0) arAmmo = 0;
                break;
            case "SR":
                sniperAmmo += amount;
                if (sniperAmmo < 0) sniperAmmo = 0;
                break;
            case "SG":
                shotgunAmmo += amount;
                if (shotgunAmmo < 0) shotgunAmmo = 0;
                break;
            case "Pistol":
                pistolAmmo += amount;
                if (pistolAmmo < 0) pistolAmmo = 0;
                break;
        }
    }

    /// <summary>
    /// Updates the ammo for reloading specifically
    /// </summary>
    /// <param name="ammoType"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public int UpdateAmmoReload(string ammoType, int amount)
    {
        int ammoToGive = 0;
        switch (ammoType)
        {
            case "AR":
                if (amount > arAmmo)
                {
                    ammoToGive = arAmmo;
                    arAmmo = 0;
                }
                else
                {
                    ammoToGive = amount;
                    arAmmo -= amount;
                }
                break;

            case "SR":
                if (amount > sniperAmmo)
                {
                    ammoToGive = sniperAmmo;
                    sniperAmmo = 0;
                }
                else
                {
                    ammoToGive = amount;
                    sniperAmmo -= amount;
                }
                break;

            case "SG":
                if (amount > shotgunAmmo)
                {
                    ammoToGive = shotgunAmmo;
                    shotgunAmmo = 0;
                }
                else
                {
                    ammoToGive = amount;
                    shotgunAmmo -= amount;
                }
                break;

            case "Pistol":
                if (amount > pistolAmmo)
                {
                    ammoToGive = pistolAmmo;
                    pistolAmmo = 0;
                }
                else
                {
                    ammoToGive = amount;
                    pistolAmmo -= amount;
                }
                break;
        }
        return ammoToGive;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ammoType"></param>
    /// <returns></returns>
    public int GetAmmoString(string ammoType)
    {
        switch (ammoType)
        {
            case "AR":
                return arAmmo;
            case "SR":
                return sniperAmmo;
            case "SG":
                return shotgunAmmo;
            case "Pistol":
                return pistolAmmo;
        }
        return 0;
    }
}
