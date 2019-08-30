using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : ImmuCharacter
{
    public int rarity; 
    public Hero()
    {
        health = 150;
        damage = 125;
        name = "Hero";
        skill = "Triple Shot";
        rarity = 3;
    }

    public void introduceYourself()
    {
        Debug.Log("I am " + name + ". I am of rarity " + rarity + ". I have " + health + " HP and deal " + damage + " damage with my " + skill + ".");
    }
}
