// Author: Justen Koo
// Purpose: Keeps track of the player's experience points and levels

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currExp, level, reqExp, skillPoints;

    private void Start()
    {
        currExp = 0;
        level = 1;
        reqExp = 1000;
        skillPoints = 0;
    }

    // DELETE ENTIRE UPDATE FUNCTION WHEN DONE TESTING
    private void Update()
    {
        if(Input.GetKeyDown("i"))
        {
            UpdateExp(250);
            Debug.Log("Level: " + level + " | " + currExp + "/" + reqExp);
        }
    }

    public void UpdateExp(int amt)
    {
        currExp += amt;
        if (currExp >= reqExp)
        {
            int excessExp = currExp - reqExp;
            currExp = excessExp;
            LevelUp();
        }
    }

    public void LevelUp() { 
        level++;
        reqExp += 250;
        skillPoints++;
    }

    public void UpdateSkillPoints(int amt)
    {
        skillPoints += amt;
    }

    public int GetCurrExp() { return currExp; }
    public int GetCurrLvl() { return level; }
    public int GetSkillPoints() { return skillPoints; }
}
