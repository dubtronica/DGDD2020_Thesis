using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneCell : Hero
{
    public ImmuneCell()
    {
        health = 300;
        damage = 125;
        name = "Hero";
        skill = "Triple Shot";
        rarity = 3;
        type = new string[3];
    }


}
