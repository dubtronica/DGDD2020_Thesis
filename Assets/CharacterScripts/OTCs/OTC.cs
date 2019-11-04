using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTC : Hero
{
    public OTC()
    {
        health = 0;
        damage = 0;
        name = "OTC";
        skill = "Heal";
        rarity = 3;
        type = new string[2];
    }
}
