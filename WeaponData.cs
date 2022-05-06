// Author: Justen Koo
// Purpose: Editable modular code that can be applied to any weapon to support shooting and reloading mechanics

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    private GameObject bullet;
    private Camera playerCam;
    private PlayerInventory pInv;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private AudioManager aM;

    // Variables for Projectiles
    [SerializeField] private float shootForce;
    [SerializeField] private float upwardForce;
    [SerializeField] private float timeBetweenShooting;
    [SerializeField] private float spread;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int bulletsPerTap;
    [SerializeField] private bool allowButtonHold;
    [SerializeField] private bool isEquipped;
    [SerializeField] private float reloadTime;

    // Weapon Stats
    [SerializeField] private string weaponType = "AR";
    [SerializeField] private int magSize = 30;
    [SerializeField] private int currAmmo = 30;

    public enum weaponState
    {
        SHOOTING,
        READY,
        RELOADING
    }

    public weaponState wepState = weaponState.READY;

    private void Start()
    {
        aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        bullet = (GameObject)Resources.Load("Bullet", typeof(GameObject));
        playerCam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
        pInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    private void FixedUpdate()
    {
        MyInput();
    }

    private void MyInput()
    {
        UpdateTimers();

        if(allowButtonHold)
        {
            if (Input.GetMouseButton(0) && wepState != weaponState.RELOADING && isEquipped && currAmmo > 0)
                Shoot();
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && wepState != weaponState.RELOADING && isEquipped && currAmmo > 0)
                Shoot();
        }
        

        if (Input.GetKeyDown("r"))
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if(wepState != weaponState.RELOADING && timeBetweenShots <= 0) {
            aM.Play("AR");
            Debug.Log("Playing Sound");
            wepState = weaponState.SHOOTING;
            // Raycast to the middle of the screen
            Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                // instead of hardcoding a distance, take the WeaponData's max distance
                targetPoint = ray.GetPoint(75);
            }

            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

            // Calculate Spread
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

            currentBullet.transform.forward = directionWithoutSpread.normalized;
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(playerCam.transform.up * upwardForce, ForceMode.Impulse);

            // Use PlayerInventory to update ammo count
            currAmmo -= 1;

            timeBetweenShots = timeBetweenShooting;

            // Destroy the Bullet
            Destroy(currentBullet, 3f);
        }
    }

    private void UpdateTimers()
    {
        reloadTime -= Time.deltaTime;
        if (reloadTime <= 0) reloadTime = 0;
        if (timeBetweenShots > 0) timeBetweenShots -= Time.deltaTime;
        if (timeBetweenShots <= 0) timeBetweenShots = 0;
    }

    private void ResetTimers()
    {
        reloadTime = 2;
    }

    private void Reload()
    {
        if(isEquipped && currAmmo < magSize && reloadTime <= 0)
        {
            wepState = weaponState.RELOADING;
            int neededAmount = magSize - currAmmo;
            currAmmo += pInv.UpdateAmmoReload(weaponType, neededAmount);
            ResetTimers();
            wepState = weaponState.READY;
        }
        else { Debug.Log("Not Ready to Reload"); }
    }

    public void setEquipStatus(bool status){ isEquipped = status; }
    public string getWeaponType() { return weaponType; }
    public string getCurrAmmoString() { return currAmmo.ToString(); }
}
