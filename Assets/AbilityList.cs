using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility
{
    public string name;
    public string targetType;
    public string numberOfTargets;
    public int range;

    public ActiveAbility(string n, string tt, string not, int r)
    {
        name = n;
        targetType = tt;
        numberOfTargets = not;
        range = r;
    }
}

public class AbilityList : MonoBehaviour
{
    public List<ActiveAbility> activeAbilityList;

    public AbilityList()
    {
        activeAbilityList = new List<ActiveAbility>();
        activeAbilityList.Add(new ActiveAbility("Phagocytosis Slash", "Enemy", "Column", 1));
        activeAbilityList.Add(new ActiveAbility("Devour", "Enemy", "Single", 2));
        activeAbilityList.Add(new ActiveAbility("Apoptosis", "Enemy", "Column", 3));
        activeAbilityList.Add(new ActiveAbility("Strategic Encore", "Character", "All", 3));
        activeAbilityList.Add(new ActiveAbility("Phagocytosis Bite", "Enemy", "Single", 3));
        activeAbilityList.Add(new ActiveAbility("Cytotoxic Slash", "Enemy", "Column", 3));
        activeAbilityList.Add(new ActiveAbility("Antibody Job", "Enemy", "Double", 3));
    }

    public void callAbility(BattleAbility ability, List<BattleClick> targets)
    {
        if (ability.activeAbility.Equals("Phagocytosis Slash"))
        {
            ability7(ability.character, targets);
        }
        else if (ability.activeAbility.Equals("Devour"))
        {
            ability8(ability.character, targets);
        }
        else if (ability.activeAbility.Equals("Apoptosis"))
        {
            ability9(ability.character, targets);
        }
    }

    public void ability7(BattleClick unit, List<BattleClick> targets)
    {
        for (int x = 0; x < targets.Count; x++)
        {
            if (targets[x].currentHealth > 0)
            {
                targets[x].takeDamage(100, unit);
            }
        }
    }

    public void ability8(BattleClick unit, List<BattleClick> targets)
    {
        targets[0].takeDamage(250, unit);
    }

    public void ability9(BattleClick unit, List<BattleClick> targets)
    {
        for (int x = 0; x < targets.Count; x++)
        {
            if (targets[x].currentHealth > 0)
            {
                targets[x].addDebuff("Poison", 3);
            }
        }
    }
}
