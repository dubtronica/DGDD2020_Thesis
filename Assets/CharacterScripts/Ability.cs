using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability 
{
    string name, description;
    double dmult;

    public Ability()
    {
        name = "Punch";
        description = "ONE PUUUUNCH!";
        dmult = 2.2;
    }

    public Ability(string n, string d, double m)
    {
        name = n;
        description = d;
        dmult = m;
    }

    void perform()
    {
        Debug.Log("Yeeeet");
    }
}
