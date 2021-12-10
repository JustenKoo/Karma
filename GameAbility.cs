// Author: Justen Koo
// GameAbility abstract class
// Edited code from: https://learn.unity.com/tutorial/create-an-ability-system-with-scriptable-objects#5cf5ecededbc2a36a1bd53b7

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAbility : ScriptableObject
{
    // Overview
    public string abilityName;
    public string abilityDesc;

    // Level
    public int currLevel;
    public int maxLevel;

    // In-Game Use
    public int usageCost;
    public float usageTime;
    public float cooldownTime;

    // Skill Tree
    public GameAbility prevAbility;
    public GameAbility nextAbility;

    public enum ability_type
    {
        UNINITIALIZED,
        ATTACK,
        DEFENSE,
        MOVEMENT,
        RECON,
        UTILITY
    }

    public ability_type abilityType = ability_type.UNINITIALIZED;

    public enum ability_class
    {
        UNITIALIZED,
        PASSIVE,
        TACTICAL,
        ULTIMATE
    }

    public ability_class abilityClass = ability_class.UNITIALIZED;
}