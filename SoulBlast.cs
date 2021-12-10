// Author: Justen Koo
// Soul Blast Ability Projectile
// Edited code from: https://learn.unity.com/tutorial/create-an-ability-system-with-scriptable-objects#5cf5ecededbc2a36a1bd53b7

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/SoulBlast")]
public class SoulBlast : GameAbility
{
    public GameObject projectile;
    public float launchVelocity = 700f;

    public void Trigger()
    {
        GameObject soulBlast = Instantiate(projectile, projectile.transform.position, projectile.transform.rotation);
        soulBlast.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity, 0));
    }
}