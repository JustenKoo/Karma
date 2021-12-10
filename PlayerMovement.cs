using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Base Movement From: https://www.youtube.com/watch?v=E5zNi_SSP_w

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    Rigidbody rb;
    public float groundDrag = 6f;
    public float airDrag = 2f;

    public bool isGrounded;
    public bool isSprinting = false;

    public float jumpForce = 10f;

    // Menus
    public MenuManager screen;

    // Soul
    public SoulManager soulManager;
    public GameObject projectile;
    public float launchVelocity = 700f;
    public Vector3 origin;

    private void Awake()
    {
        screen.HideAllScreens();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        MyInput();
        ButtonPresses();
        ControlDrag();
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void ButtonPresses()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            isSprinting = true;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            isSprinting = false;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("TAB HAS BEEN CLICKED");
            screen.EscMenu();
        }

        if (Input.GetMouseButton(1))
        {
            soulManager.UpdateSoul(1);
        }

        if(Input.GetKeyDown("q"))
        {
            if(soulManager.UseSoulBlast())
            {
                soulManager.UpdateSoul(-25);
                GameObject soulBlast = Instantiate(projectile, transform.position, transform.rotation);
                soulBlast.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(1500, launchVelocity, 1500));
                // Instead of hardcoding when it destroys, have it take in a value for the SoulBlast lastPeriod 
                Destroy(soulBlast, 2f);
            }
        }

        if (Input.GetKeyDown("o"))
        {
            Save();
        }
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

    private void FixedUpdate()
    {
        MovePlayer();
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
}