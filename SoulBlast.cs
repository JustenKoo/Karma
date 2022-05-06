using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBlast : MonoBehaviour
{
    private GameObject projectile;
    private GameObject playerCam;
    public float launchVelocity = 700f;

    void Start()
    {
        projectile = (GameObject)Resources.Load("Prefabs/SoulBlastProjectile", typeof(GameObject));
        // playerCam = GameObject.Find
    }

    public void Use(GameObject cam)
    {
        GameObject soulBlast = Instantiate(projectile, new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z), cam.transform.rotation);
        soulBlast.GetComponent<Rigidbody>().AddRelativeForce(cam.transform.forward * launchVelocity);
        Destroy(soulBlast, 2f);
    }
}