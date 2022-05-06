// Author: Justen Koo
// GameAbility abstract class
// Inspired code from: https://learn.unity.com/tutorial/create-an-ability-system-with-scriptable-objects#5cf5ecededbc2a36a1bd53b7 and https://www.youtube.com/watch?v=ry4I6QyPw4E

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAbility : ScriptableObject
{
    // Overview
    // public SoulManager soulManager;
    public string aName;
    public string aDesc;
    public bool unlocked;

    // In-Game Use
    public int usageCost;
    public float startUpTime;
    public float activeTime;
    public float cooldownTime;

    public KeyCode key;

    public enum AbilityState
    {
        UNITIALIZED,
        READY,
        ACTIVE,
        COOLDOWN
    }
    public AbilityState abState = AbilityState.UNITIALIZED;

    // Skill Tree
    public List<GameAbility> requirements = new List<GameAbility>();
    public List<GameAbility> nextAbilities = new List<GameAbility>();

    // Important Functions
    public abstract void Initialize(GameObject player);
    public abstract void TriggerAbility();  // activates the ability on button press
    public abstract void UpdateAbility();   // carries on operation of ability based on activeTime
    public abstract void UpdateTimer();
    public abstract void UnlockAbility();
}