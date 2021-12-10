using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Edited Code From: https://youtu.be/_QajrabyTJc

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public PlayerInventory playerInv;
    public SoulManager soulManager;
    public GameObject player;

    [SerializeField]
    private float speed = 12f;

    [SerializeField]
    private float gravity = -9.8f;

    [SerializeField]
    private float jumpHeight = 3f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    public MenuManager screen;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        // player = GameObject.Find("Player").GetComponent<Player>();
        playerInv = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
        soulManager = GameObject.Find("SoulManager").GetComponent<SoulManager>();
        screen = new MenuManager();
        screen.HideAllScreens();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;
        }
        //Save Player Data
        if (Input.GetKeyDown(KeyCode.O))
        {
            Save();
            Debug.Log("Pressed!!!! SAVED");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("TAB HAS BEEN CLICKED");
            screen.EscMenu();
        }

        // Soul Absorption
        if (Input.GetMouseButtonDown(1))
        {
            soulManager.UpdateSoul(1);
        }

        if (Input.GetKeyDown("q"))
        {
            soulManager.UpdateSoul(-25);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Save()
    {
        // Matthew Cruz 11/3/2021
        Vector3 playerPosition = player.transform.position;
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

        string saveData = string.Join(saveSeparator,contents);
        File.WriteAllText(Application.dataPath + "/save.txt", saveData);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Test");
        GameItem item = new GameItem();
        if (collision.collider.CompareTag("Ammo") == true)
        {
            item.itemType = GameItem.item_category.AMMO;
            if (collision.collider.name == "Sniper Ammo")
            {
                item.ammoType = GameItem.ammo_type.SNIPER;
                item.itemName = "Sniper Ammo";
            }
            else if (collision.collider.name == "Shotgun Ammo")
            {
                item.ammoType = GameItem.ammo_type.SHOTGUN;
                item.itemName = "Shotgun Ammo";
            }
            else if (collision.collider.name == "Pistol Ammo")
            {
                item.ammoType = GameItem.ammo_type.PISTOL;
                item.itemName = "Shotgun Ammo";
            }
        }

        else if (collision.collider.CompareTag("Consumable") == true)
        {
            item.itemType = GameItem.item_category.CONSUMABLE;
        }

        else if (collision.collider.CompareTag("Primary Weapon") == true)
        {
            item.itemType = GameItem.item_category.PRIMARY_WEAPON;
        }

        else if (collision.collider.CompareTag("Secondary Weapon") == true)
        {
            item.itemType = GameItem.item_category.SECONDARY_WEAPON;
        }

        playerInv.UpdateInventory(item, 1);
        Destroy(collision.collider.gameObject);
        // FindObjectOfType<AudioManager>().Play("Pickup");
    }
}