using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Enemy
{
    public Virus()
    {
        health = 250;
        damage = 300;
        name = "Virus";
        skill = "Freeze";
        type = new string[3];
        type[0] = symptoms[2];
    }
}
