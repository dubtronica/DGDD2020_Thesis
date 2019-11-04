using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ImmuCharacter
{
    public string[] symptoms = { "Fever", "Cold", "Pain", "Diarrhea" };
    public string[] type;
    public string stain;

    public Enemy()
    {
        health = 150;
        damage = 125;
        name = "Enemy";
        skill = "Burn";
        type = new string[3];
        type[0] = symptoms[0];
        stain = "None";
    }

    public void introduceYourself()
    {
        Debug.Log("I am " + name + ". I am of type " + type[0] + ". I have " + health + " HP and deal " + damage + " damage with my " + skill + ".");
    }

    public void normalAttack(Hero hr)
    {
        hr.takeDamage(this);
    }
}
