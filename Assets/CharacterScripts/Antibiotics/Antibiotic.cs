using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibiotic : Hero
{
    public string staintype;
    public Antibiotic()
    {
        health = 150;
        damage = 100;
        name = "AntiB";
        skill = "PewPew";
        rarity = 2;
        staintype = "Broad Spectrum";
    }

    public Antibiotic(string st)
    {
        health = 150;
        damage = 100;
        name = "AntiB";
        skill = "PewPew";
        rarity = 2;
        staintype = st;
    }

    public void introduceYourself()
    {
        Debug.Log("I am " + name + ". I am a " + type + " antibiotic of rarity " + rarity + ". I have " + health + " HP and deal " + damage + " damage with my " + skill + ".");
    }

    public void normalAttack(Enemy en)
    { 
        if(en.GetType().IsSubclassOf(typeof(Virus)) || en.GetType().IsEquivalentTo(typeof(Virus)))
        {
            en.takeDamage(this, 0.0);
        }
        else if (en.GetType().IsSubclassOf(typeof(Bacteria)) || en.GetType().IsEquivalentTo(typeof(Bacteria)) && en.stain.Equals(this.staintype) && !(this.staintype.Equals("Broad Spectrum")))
        {
            en.takeDamage(this, 2.0);
        }
        else
        {
            en.takeDamage(this);
        }
    }

    public void specialAttack(Enemy en, double mult)
    {
        if (en.GetType().IsSubclassOf(typeof(Virus)) || en.GetType().IsEquivalentTo(typeof(Virus)))
        {
            en.takeDamage(this, 0.0);
        }
        else if (en.GetType().IsSubclassOf(typeof(Bacteria)) || en.GetType().IsEquivalentTo(typeof(Bacteria)) && en.stain.Equals(this.staintype) && !(this.staintype.Equals("Broad Spectrum"))) // for G+ and G-
        {
            en.takeDamage(this, 2.0 * mult);
        }
        else
        {
            en.takeDamage(this);
        }
    }
}
