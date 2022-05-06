// Author: Justen Koo
// Purpose: Handles movement and input for the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Base Movement From: https://www.youtube.com/watch?v=E5zNi_SSP_w
// TODO: Reconsider visibility for variables. Most should have helper functions established instead

public class PlayerMovement : MonoBehaviour
{
    #region
    public GameObject player;
    private Rigidbody rb;
    private float playerHeight = 2f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    private float movementMultiplier = 10f;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 moveDirection;
    private float groundDrag = 6f;
    private float airDrag = 2f;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isSprinting = false;
    [SerializeField] private float jumpForce = 10f;

    public enum weapon_equip
    {
        PRIMARY,
        SECONDARY,
        MELEE,
        HOLSTERED
    }
    [Header("Weapons and Items")]
    public weapon_equip equipStatus = weapon_equip.HOLSTERED;
    public PlayerInventory pInv;

    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    public GameObject emptyHands;

    // Menus
    [Header("Menu")]
    public MenuManager screen;

    // Soul
    [Header("Soul")]
    private SkillTree skillTree;
    private List<GameAbility> unlockedSkills;
    private List<KeyCode> unlockedSkillKeycodes;

    // Camera
    [Header("Camera")]
    public GameObject playerCam;

    // Animator
    [Header("Animators")]
    public Animator playerAnimator;
    public Animator weaponAnimator;
    #endregion

    private void Awake()
    {
        //screen.HideAllScreens();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        pInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        emptyHands = GameObject.Find("Hands_Without_Weapon");
        weaponAnimator = emptyHands.GetComponent<Animator>();

        skillTree = GameObject.Find("SkillTree").GetComponent<SkillTree>();
        unlockedSkills = new List<GameAbility>();
        unlockedSkillKeycodes = new List<KeyCode>();

        unlockedSkills = skillTree.GetUnlockedAbilities();
        unlockedSkillKeycodes = skillTree.GetUnlockedAbilitiesKeys();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        // isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        MyInput();
        ControlDrag();
        UpdateAbilities();
    }

    private void Update()
    {
        ButtonPresses();
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

        if (horizontalMovement == 0 && verticalMovement == 0)
        {
            playerAnimator.SetTrigger("trIdle");
        }
    }

    void ButtonPresses()
    {
        // Movement
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;

            // Animations
            playerAnimator.SetTrigger("trRun");
            if(equipStatus != weapon_equip.HOLSTERED) weaponAnimator.SetTrigger("trRun");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;

            // Animations
            playerAnimator.SetTrigger("trWalk");
            if (equipStatus != weapon_equip.HOLSTERED) weaponAnimator.SetTrigger("trWalk");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown("w") || Input.GetKeyDown("a")
            || Input.GetKeyDown("s") || Input.GetKeyDown("d"))
        {

            // Animations
            playerAnimator.SetTrigger("trWalk");
            if (equipStatus != weapon_equip.HOLSTERED) weaponAnimator.SetTrigger("trRun");
        }



        if (Input.GetMouseButton(0) && equipStatus != weapon_equip.HOLSTERED)
        {

            // Animations
            weaponAnimator.SetTrigger("trShot");
        }

        if (Input.GetKeyDown("r") && equipStatus != weapon_equip.HOLSTERED)
        {
            weaponAnimator.SetTrigger("trReload");
        }



        // Abilities
        for (int i = 0; i < unlockedSkillKeycodes.Count; i++)
        {
            if (Input.GetKeyDown(unlockedSkillKeycodes[i]))
            {
                unlockedSkills[i].TriggerAbility();
            }
        }



        // Weapon Equip Status
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (pInv.GetWeapon("Primary") != null)
            {
                equipStatus = weapon_equip.PRIMARY;

                if (GameObject.Find(pInv.GetWeapon("Primary").name + "(Clone)") == null)
                {
                    primaryWeapon = Instantiate(pInv.GetWeapon("Primary"), emptyHands.transform.position, emptyHands.transform.rotation);
                }

                primaryWeapon.GetComponent<WeaponData>().setEquipStatus(true);
                primaryWeapon.transform.position = emptyHands.transform.position + new Vector3(0, 2f, 0);
                primaryWeapon.transform.SetParent(playerCam.transform);

                weaponAnimator = primaryWeapon.GetComponent<Animator>();
                weaponAnimator.SetTrigger("trGet");

                if (secondaryWeapon.activeInHierarchy == true) secondaryWeapon.SetActive(false);
                emptyHands.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pInv.GetWeapon("Secondary") != null)
            {
                equipStatus = weapon_equip.SECONDARY;

                if (GameObject.Find(pInv.GetWeapon("Secondary") + "(Clone)") == null)
                {
                    secondaryWeapon = Instantiate(pInv.GetWeapon("Secondary"), emptyHands.transform.position, emptyHands.transform.rotation);
                }

                pInv.GetWeapon("Secondary").GetComponent<WeaponData>().setEquipStatus(true);
                secondaryWeapon.transform.position = emptyHands.transform.position;
                secondaryWeapon.transform.SetParent(playerCam.transform);

                weaponAnimator = secondaryWeapon.GetComponent<Animator>();
                weaponAnimator.SetTrigger("trGet");

                if(primaryWeapon.activeInHierarchy == true) primaryWeapon.SetActive(false);
                emptyHands.SetActive(false);
            }
        }

        if(Input.GetKeyDown("x"))
        {
            switch(equipStatus)
            {
                case weapon_equip.PRIMARY:
                    primaryWeapon.SetActive(false);
                    pInv.GetWeapon("Primary").GetComponent<WeaponData>().setEquipStatus(false);
                    break;
                case weapon_equip.SECONDARY:
                    secondaryWeapon.SetActive(false);
                    pInv.GetWeapon("Secondary").GetComponent<WeaponData>().setEquipStatus(false);
                    break;
            }

            if (equipStatus != weapon_equip.HOLSTERED)
            {
                weaponAnimator.SetTrigger("trHide");
                equipStatus = weapon_equip.HOLSTERED;
                emptyHands.SetActive(true);
                playerAnimator.SetTrigger("trEquip");
            }
        }



        // Game Status and HUD
        if (Input.GetKeyDown("o"))
        {
            Save();
        }
    }

    private void UpdateAbilities()
    {
        for (int i = 0; i < unlockedSkills.Count; i++)
        {
            if (unlockedSkills[i].abState == GameAbility.AbilityState.ACTIVE)
            {
                unlockedSkills[i].UpdateAbility();
            }
            else if (unlockedSkills[i].abState == GameAbility.AbilityState.COOLDOWN)
            {
                unlockedSkills[i].UpdateTimer();
            }
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ControlDrag()
    {
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = airDrag;
    }

    void MovePlayer()
    {
        if(isGrounded)
        {
            if (isSprinting == true)
                rb.AddForce(moveDirection.normalized * moveSpeed * (movementMultiplier * 2), ForceMode.Acceleration);
            else
                rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * (movementMultiplier / 2), ForceMode.Acceleration);

    }

    private void Save()
    {
        // Matthew Cruz 11/3/2021
        Vector3 playerPosition = transform.position;
        PlayerPrefs.SetFloat("playerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("playerPositionY", playerPosition.y);
        PlayerPrefs.SetFloat("playerPositionZ", playerPosition.z);
        PlayerPrefs.Save();
        string saveSeparator = "\n";

        string[] contents = new string[]
        {
            ""+playerPosition.x,
            ""+playerPosition.y,
            ""+playerPosition.z,
        };

        string saveData = string.Join(saveSeparator, contents);
        File.WriteAllText(Application.dataPath + "/save.txt", saveData);
    }
}