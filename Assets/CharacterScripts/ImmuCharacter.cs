using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuCharacter
{
    public int health, damage;
    public string name, skill;

    public ImmuCharacter()
    {
        health = 100;
        damage = 150;
        name = "dummy";
        skill = "basic attack";
    }

    public void introduceYourself()
    {
        Debug.Log("I am " + name + ". I have " + health + " HP and deal " + damage + " damage with my " + skill + ".");
    }


    public void takeDamage(ImmuCharacter ic)
    {
        health -= ic.damage;

        Debug.Log("Arrgh! I took " + ic.damage + " damage! My HP is now at " + health);
    }

    public void takeDamage(ImmuCharacter ic, double multiplier)
    {
        health -= (int) (ic.damage * multiplier);

        Debug.Log("Arrgh! I took " + (int)(ic.damage * multiplier) + " damage! My HP is now at " + health);
    }
}
