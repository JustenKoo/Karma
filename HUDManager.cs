// Author: Justen Koo
// Purpose: Manages all of the screens in the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    // Screens
    [Header("Screens")]
    public GameObject InventoryMenu;
    public Canvas SkillTree;

    // Managers
    [Header("Managers")]
    private SoulManager soulManager;
    private PlayerHealth healthManager;
    private PlayerInventory pInv;

    public GameObject HUD;
    private WeaponData wepData;

    // Player
    private PlayerMovement pM;

    public TextMeshProUGUI soulMeter;
    public TextMeshProUGUI healthMeter;

    // HUD FOR SOUL

    // HUD FOR WEAPONS
    [Header("Weapon HUD")]
    public TextMeshProUGUI ammoDisplay;

    // HUD FOR ABILITIES
    // [Header("Ability HUD")]

    // Start is called before the first frame update
    void Start()
    {
        soulMeter.text = "100";
        soulManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulManager>();
        // pInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        pM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        healthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        SkillTree.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        soulMeter.text = "Soul: " + soulManager.GetCurrSoulAmount();
        healthMeter.text = "Health: " + healthManager.getCurrHealth();

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(SkillTree.enabled == true)
            {
                SkillTree.enabled = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SkillTree.enabled = true;
            }
        }

        /*switch (pM.equipStatus)
        {
            case PlayerMovement.weapon_equip.PRIMARY:
                wepData = pInv.primaryWeapon.GetComponent<WeaponData>();

                switch (wepData.getWeaponType())
                {
                    case "AR":
                        ammoDisplay.text = wepData.getCurrAmmoString() + " / " + pInv.GetAmmoString("AR");
                        break;
                    case "SR":
                        ammoDisplay.text = wepData.getCurrAmmoString() + " / " + pInv.GetAmmoString("SR");
                        break;
                }
                break;

            case PlayerMovement.weapon_equip.SECONDARY:
                wepData = pInv.secondaryWeapon.GetComponent<WeaponData>();

                switch (wepData.getWeaponType())
                {
                    case "SG":
                        ammoDisplay.text = wepData.getCurrAmmoString() + " / " + pInv.GetAmmoString("SG");
                        break;
                    case "Pistol":
                        ammoDisplay.text = wepData.getCurrAmmoString() + " / " + pInv.GetAmmoString("Pistol");
                        break;
                }
                break;

            case PlayerMovement.weapon_equip.MELEE:
                ammoDisplay.text = "";
                break;

            case PlayerMovement.weapon_equip.HOLSTERED:
                ammoDisplay.text = "";
                break;
        }

        if(pM.equipStatus != PlayerMovement.weapon_equip.HOLSTERED)
        {
            ammoDisplay.text = "";
        }*/
    }
}
