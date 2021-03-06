// Edited Code From: https://www.youtube.com/watch?v=AGd16aspnPA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAbsorptionTest : MonoBehaviour
{
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    public Camera fpsCam;
    public LineRenderer laserLine;

    public LineRenderer soulBeam;

    // DELETE: THIS IS ONLY FOR DEV TESTING
    public bool devMode = false;

    // Timers
    public bool inUse = true;
    public float useTime = 5f;
    public float activeTime = 2f;
    public bool inCooldown = false;
    public float cooldownTime = 10f;

    void Update()
    {
        // Time handling
        if (inCooldown)
        {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0)
            {
                inCooldown = false;
                useTime = 5f;
                activeTime = 2f;
            }
        }

        // DELETE: THIS IS ONLY FOR DEV TESTING
        if (Input.GetKeyDown("p"))
        {
            if (devMode == true)
            {
                devMode = false;
                Debug.Log("DevMode is Off");
            }
            else
            {
                devMode = true;
                Debug.Log("DevMode is On");
            }
        }

        // Normal Raycast (how the mouse is supposed to function
        //if (Input.GetButton("Fire2") && inCooldown == false && useTime > 0 && activeTime > 0)
        
        // DELETE: THIS IS ONLY FOR DEV TESTING
        if (Input.GetButton("Fire2") && devMode == true)
        {
            laserLine.enabled = true;
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                // The mouse
                laserLine.SetPosition(1, hit.point);

                if (hit.rigidbody.tag == "Enemy")
                {
                    GameObject ss = hit.transform.Find("SoulSource").gameObject;

                    // Four Points to Calculate the Bezier Curve
                    float midpointX = (rayOrigin.x + hit.transform.position.x) / 2;
                    float midpointY = (rayOrigin.y + hit.transform.position.y) / 2;
                    float midpointZ = (rayOrigin.z + hit.transform.position.z) / 2;
                    Vector3 midpoint = new Vector3(midpointX, midpointY, midpointZ);

                    float point2X = (rayOrigin.x + midpointX) / 2;
                    float point2Y = (rayOrigin.y + midpointY) / 2;
                    float point2Z = (rayOrigin.z + midpointZ) / 2;
                    Vector3 point2 = new Vector3(point2X, point2Y, point2Z);

                    float point3X = (midpointX + hit.transform.position.x) / 2;
                    float point3Y = (midpointY + hit.transform.position.y) / 2;
                    float point3Z = (midpointZ + hit.transform.position.z) / 2;
                    Vector3 point3 = new Vector3(point3X, point3Y, point3Z);

                    // Add the Points to an Array to feed to soulBeam
                    var points = new Vector3[4];
                    points[0] = gunEnd.position;
                    points[1] = point2;
                    points[2] = point3;
                    points[3] = ss.transform.position;

                    // Enable the LineRenderer and set the points equal to the Array
                    soulBeam.enabled = true;
                    soulBeam.positionCount = 4;
                    soulBeam.SetPositions(points);
                }
            }

            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
        else
        {
            laserLine.enabled = false;
            activeTime -= Time.deltaTime;
            inUse = false;
            if (activeTime <= 0)
            {
                inCooldown = true;
            }

            // soulBeam.enabled = false;
        }
    }
}