// Author: Justen Koo
// Purpose: Manages the player's soul amount and allows other scripts to access the amount

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    private int currSoul = 0;
    private int maxSoul = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMaxSoul(int level)
    {
        maxSoul = 100 + (level * 10);
    }

    public void UpdateSoul(int amount)
    {
        if ((currSoul + amount) > maxSoul)
            currSoul = maxSoul;
        else if ((currSoul + amount) < 0)
            currSoul = 0;
        else
            currSoul += amount;
    }

    public bool UseSoulBlast()
    {
        if (currSoul >= 25)
            return true;
        else
            return false;
    }

    public int GetCurrSoulAmount() { return currSoul; }
    public void SetCurrSoulAmount(int amount) { currSoul = amount; }
}
