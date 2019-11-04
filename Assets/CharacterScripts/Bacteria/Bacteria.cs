using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Enemy
{
   
    public Bacteria()
    {
        health = 150;
        damage = 125;
        name = "E coli";
        skill = "Burn";
        type = new string[3];
        type[0] = symptoms[0];
        stain = "Gram Positive";
    }

    public Bacteria(string st)
    {
        health = 150;
        damage = 125;
        name = "E coli";
        skill = "Burn";
        type = new string[3];
        type[0] = symptoms[0];
        stain = st;
    }

}
