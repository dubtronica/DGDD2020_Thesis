using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupremePunch : Ability
{
    public SupremePunch()
    {
        name = "SupremePunch";
        description = "A punch that can beat Saitama";
        dmult = 1.5;
    }

    void perform(ImmuCharacter ic)
    {
        ic.takeDamage(owner, 1.5);
    }

    
}
