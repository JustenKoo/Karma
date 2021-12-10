// Author: Justen Koo
// Purpose: Manages all of the screens in the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    // Screens
    public GameObject InventoryMenu;

    // Managers
    public SoulManager soulManager;

    public GameObject HUD;
    public TextMeshProUGUI soulMeter;
    public TextMeshProUGUI healthMeter;

    // HUD FOR SOUL

    // HUD FOR WEAPONS

    // HUD FOR ABILITIES

    // Start is called before the first frame update
    void Start()
    {
        soulMeter.text = "100";
        soulManager = GameObject.Find("SoulManager").GetComponent<SoulManager>();
    }

    // Update is called once per frame
    void Update()
    {
        soulMeter.text = soulManager.GetCurrSoulAmount().ToString();
    }
}
