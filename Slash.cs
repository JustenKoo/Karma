using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Slash")]
public class Slash : GameAbility
{
    public override void Initialize(GameObject player)
    {
        Debug.Log("Initialized: " + aName);
    }

    public override void TriggerAbility()
    {

    }

    public override void UpdateAbility()
    {

    }

    public override void UpdateTimer()
    {
        throw new System.NotImplementedException();
    }

    public override void UnlockAbility()
    {

    }
}
