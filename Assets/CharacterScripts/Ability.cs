using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability 
{
    public string name, description;
    public double dmult;
    public ImmuCharacter owner;

    public Ability()
    {
        name = "Punch";
        description = "ONE PUUUUNCH!";
        dmult = 2.2;
    }

    public Ability(string n, string d, double m, ImmuCharacter o)
    {
        name = n;
        description = d;
        dmult = m;
        owner = o;
    }

    void perform()
    {
        Debug.Log("Yeeeet");
    }
}
