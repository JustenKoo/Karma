// File Name: KarmaManager.cs
// Author: Justen Koo
// Created: January 11, 2022
// Last Updated January 11, 2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaManager : MonoBehaviour
{
    private int karma = 25;

    /*
       Pure Villain: 0 - 5
       Public Enemy: 6 - 15
       Vigilante: 16 - 34
       Heroic Vigilante: 35 - 44
       Hero: 45 - 50
    */
    public enum karmaRating
    {
        UNITIALIZED,
        PURE_VILLAIN,
        PUBLIC_ENEMY,
        VIGILANTE,
        HEROIC_VIGILANTE,
        HERO
    }

    public karmaRating karma_rating = karmaRating.UNITIALIZED;

    private void updateKarmaRating()
    {
        if (karma >= 0 && karma <= 5)
        {
            karma_rating = karmaRating.PURE_VILLAIN;
        }
        else if (karma >= 6 && karma <= 15)
        {
            karma_rating = karmaRating.PUBLIC_ENEMY;
        }
        else if (karma >= 16 && karma <= 34)
        {
            karma_rating = karmaRating.VIGILANTE;
        }
        else if (karma >= 35 && karma <= 44)
        {
            karma_rating = karmaRating.HEROIC_VIGILANTE;
        }
        else if (karma >= 45 && karma <= 50)
        {
            karma_rating = karmaRating.HERO;
        }
    }

    public int getKarmaPoints() { return karma; }

    public void updateKarmaPoints(int amt) { 
        karma += amt;
        updateKarmaRating();
    }
}
