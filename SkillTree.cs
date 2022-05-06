// Author: Justen Koo

using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public LevelManager lm;

    // Abilities
    [SerializeField] private GameAbility absorption;
    [SerializeField] private GameAbility blast;
    [SerializeField] private GameAbility slash;
    [SerializeField] private GameAbility amp;
    [SerializeField] private GameAbility slam;

    [SerializeField] private List<GameAbility> allAbilities;
    private List<GameAbility> unlockedAbilities;
    private List<GameAbility> lockedAbilities;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        allAbilities = new List<GameAbility>();
        unlockedAbilities = new List<GameAbility>();
        lockedAbilities = new List<GameAbility>();

        allAbilities.Add(absorption);
        allAbilities.Add(blast);
        allAbilities.Add(slash);
        allAbilities.Add(amp);
        allAbilities.Add(slam);

        for (int i = 0; i < allAbilities.Count; i++)
        {
            allAbilities[i].Initialize(player);

            if (allAbilities[i].unlocked == true)
                unlockedAbilities.Add(allAbilities[i]);
            else
                lockedAbilities.Add(allAbilities[i]);
        }
    }

    private void Start()
    {
        //lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // TODO: Fix null reference error with LevelManager
    public void UnlockSkill(GameAbility ability)
    {
        Debug.Log(lm.GetSkillPoints());
        // Check if the player has enough skill points
        if (lm.GetSkillPoints() >= 1)
        {
            List<GameAbility> reqs = new List<GameAbility>();
            reqs = ability.requirements;
            for (int i = 0; i < reqs.Count; i++)
            {
                if (reqs[i].unlocked)
                {
                    lockedAbilities.Remove(reqs[i]);
                    unlockedAbilities.Add(reqs[i]);
                    ability.unlocked = true;
                    ability.Initialize(player);
                }
            }
        }
    }

    public string DisplayDesc(string abilityName)
    {
        string description = "";
        /*for(int i = 0; i < allAbilities.Count; i++)
        {
            if (allAbilities[i].name == abilityName)
            {
                description = allAbilities[i].GetDesc(allAbilities[i]);
            }
        }*/
        return description;
    }

    // Helper Functions
    public List<GameAbility> GetAllAbilities() { return allAbilities; }
    public List<GameAbility> GetLockedAbilities() { return lockedAbilities; }
    public List<GameAbility> GetUnlockedAbilities() { return unlockedAbilities; }
    public List<KeyCode> GetUnlockedAbilitiesKeys()
    {
        List<KeyCode> keycodes = new List<KeyCode>();
        for (int i = 0; i < unlockedAbilities.Count; i++)
        {
            keycodes.Add(unlockedAbilities[i].key);
        }
        return keycodes;
    }
}
