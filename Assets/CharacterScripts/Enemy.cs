using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ImmuCharacter
{
    public string type;

    public Enemy()
    {
        health = 150;
        damage = 125;
        name = "Enemy";
        skill = "Burn";
        type = "Fire";
    }

    public void introduceYourself()
    {
        Debug.Log("I am " + name + ". I am of type " + type + ". I have " + health + " HP and deal " + damage + " damage with my " + skill + ".");
    }
}
