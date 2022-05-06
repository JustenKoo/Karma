// Author: Justen Koo
// Purpose: Performs the Soul Absorption raycast, soul management and damage.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/Absorption")]
public class Absorption : GameAbility
{
    [Header("Absorption Specific Variables")]
    private float weaponRange = 50f;
    private GameObject hands;
    private Transform gunEnd;
    private Camera fpsCam;
    private LineRenderer laserLine;
    private LineRenderer soulBeam;
    private SoulManager sm;

    public override void Initialize(GameObject player)
    {
        fpsCam = player.transform.Find("Camera").GetComponent<Camera>();
        hands = fpsCam.transform.Find("Hands_Without_Weapon").gameObject;
        gunEnd = hands.transform.Find("HandEnd");
        laserLine = gunEnd.GetComponent<LineRenderer>();
        soulBeam = hands.transform.Find("HandEndBeam").GetComponent<LineRenderer>();
        Debug.Log("Initialized: " + aName);

        sm = player.transform.GetComponent<SoulManager>();
    }

    public override void TriggerAbility()
    {
        if (activeTime > 0)
        {
            abState = AbilityState.ACTIVE;
        }
        else
        {
            abState = AbilityState.COOLDOWN;
        }
    }

    public override void UpdateAbility()
    {
        // Time handling
        if (abState == AbilityState.COOLDOWN)
        {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0)
            {
                abState = AbilityState.READY;
                activeTime = 2f;
            }
        }

        if (Input.GetButton("Fire2") && abState != AbilityState.COOLDOWN && activeTime > 0 && sm.GetCurrSoulAmount() > 0)
        {
            abState = AbilityState.ACTIVE;
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

                    EnemyHealthManager enemy = hit.transform.GetComponent<EnemyHealthManager>();
                    enemy.updateCurrHealth(-1);
                    sm.UpdateSoul(-1);

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
            abState = AbilityState.READY;
            if (activeTime <= 0)
            {
                abState = AbilityState.COOLDOWN;
            }

            soulBeam.enabled = false;
        }
    }

    public override void UpdateTimer()
    {
        throw new System.NotImplementedException();
    }

    public override void UnlockAbility()
    {
        
    }
}
