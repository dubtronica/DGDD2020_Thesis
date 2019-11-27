using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class BattleController : MonoBehaviour
{
    public static GameObject state = null;
    public static GameObject secondSet = null;
    public static int[] characterIndexes = new int[9];
    public static int[] OTCIndexes = new int[2];
    public static List<string> enemies = new List<String>();
    public static AbilityList abilityList = new AbilityList();

    public static int[] enemyColumn = {0,0,0};

    public void setState(GameObject set)
    {
        if (state != null && state.GetComponent<BattleAbility>() != null)
        {
            state.GetComponent<BattleAbility>().unselected();
        }
        else if(state != null && state.GetComponent<BattleClick>() != null)
        {
            state.GetComponent<BattleClick>().unselected();
        }

        if((set != null && set.GetComponent<BattleAbility>() != null) || (state != null && state.GetComponent<BattleAbility>() != null))
        {
            abilityHandler(set);
        }
        else if (state == null)
        {
            if (set.name.Contains("Character"))
            {
                BattleClick temp = set.GetComponent<BattleClick>();
                Debug.Log("Selected " + temp.name + ", HP: " + temp.currentHealth + ", DMG: " + temp.maxDamage + ", Type: " + temp.type);
                state = set;
            }
        }
        else if (state.GetComponent<BattleClick>() != null)
        {
            if (set.GetComponent<BattleClick>() != null)
            {
                if(state.name == set.name)
                {
                    state = null;
                    Debug.Log("Deselected Unit");
                }
                else
                {
                    autoHandler(set);
                }
            }
        }

        if (state != null && state.GetComponent<BattleAbility>() != null)
        {
            state.GetComponent<BattleAbility>().selected();
        }
        else if (state != null && state.GetComponent<BattleClick>() != null)
        {
            state.GetComponent<BattleClick>().selected();
        }
    }

    public bool inRange(int range, int charPosition)
    {
        if(range == 3)
        {
            return true;
        }
        else if(range == 2)
        {
            if (charPosition % 3 == 1 || charPosition % 3 == 2)
            {
                return true;
            }
            else if(charPosition % 3 == 0)
            {
                if (enemyColumn[0] == 0 || enemyColumn[1] == 0)
                {
                    return true;
                }
            }
        }
        else if(range == 1)
        {
            if (charPosition % 3 == 1)
            {
                return true;
            }
            else if (charPosition % 3 == 2)
            {
                if (enemyColumn[0] == 0)
                {
                    return true;
                }
            }
            else if (charPosition % 3 == 0)
            {
                if (enemyColumn[0] == 0 && enemyColumn[1] == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void autoHandler(GameObject set)
    {
        BattleClick setbcComponent = set.GetComponent<BattleClick>();
        BattleClick statebcComponent = null;
        if (state != null)
        {
            statebcComponent = state.GetComponent<BattleClick>();
        }
        if (state.name.Contains("Character"))
        {
            if (set.name.Contains("Character"))
            {
                Debug.Log("Selected " + setbcComponent.name + ", HP: " + setbcComponent.currentHealth + ", DMG: " + setbcComponent.maxDamage + ", Type: " + setbcComponent.type);
                state = set;
            }
            else if (set.name.Contains("Enemy"))
            {
                bool attackable = false;
                if (statebcComponent.type.Contains("Antibiotic"))
                {
                    attackable = inRange(2, setbcComponent.charPosition);
                }
                else if (statebcComponent.type.Contains("ImmuneCell"))
                {
                    attackable = inRange(1, setbcComponent.charPosition);
                }
                if (attackable)
                {
                    Debug.Log(statebcComponent.name + " Attacked " + set.name + " of type " + setbcComponent.type + " with " + setbcComponent.currentHealth + " hp");
                    setbcComponent.autoTakeDamage(statebcComponent);
                    Debug.Log("Enemy Health Left: " + setbcComponent.currentHealth);
                    statebcComponent.ability.charge++;
                    statebcComponent.selectable = false;
                    state = null;
                    tryAI();
                }
            }
        }
    }

    private void abilityHandler(GameObject set)
    {
        List<BattleClick> targets = new List<BattleClick>();
        bool abilityUsed = false;
        if (set.name.Contains("Ability"))
        {
            if (state == null)
            {
                Debug.Log("Selected " + set.GetComponent<BattleAbility>().activeAbility);
                state = set;
            }
            else if (state.name == set.name)
            {
                state = null;
                Debug.Log("Deselected " + set.GetComponent<BattleAbility>().activeAbility);
            }
            else
            {
                Debug.Log("Selected " + set.GetComponent<BattleAbility>().activeAbility);
                state = set;
            }
        }
        string targetType = "";
        string numberOfTargets = "";
        int range = 0;
        if(state != null)
        {
            for (int x = 0; x < abilityList.activeAbilityList.Count; x++)
            {
                if (state.GetComponent<BattleAbility>().activeAbility == abilityList.activeAbilityList[x].name)
                {
                    targetType = abilityList.activeAbilityList[x].targetType;
                    numberOfTargets = abilityList.activeAbilityList[x].numberOfTargets;
                    range = abilityList.activeAbilityList[x].range;
                }
            }
        }
        if (numberOfTargets == "All")
        {
            abilityUsed = true;
            for (int x = 1; x <= 9; x++)
            {
                targets.Add(GameObject.Find(targetType + " " + x).GetComponent<BattleClick>());
            }
        }
        else if (numberOfTargets == "Self")
        {
            abilityUsed = true;
            targets.Add(state.GetComponent<BattleAbility>().character);
        }
        else
        {
            if (set.GetComponent<BattleClick>() != null)
            {
                if ((set.name.Contains(targetType) && inRange(range, set.GetComponent<BattleClick>().charPosition)))
                {
                    if (numberOfTargets == "Column")
                    {
                        abilityUsed = true;
                        for (int x = 1; x <= 9; x++)
                        {
                            if (GameObject.Find(targetType + " " + x).GetComponent<BattleClick>().charPosition % 3 == set.GetComponent<BattleClick>().charPosition % 3)
                            {
                                targets.Add(GameObject.Find(targetType + " " + x).GetComponent<BattleClick>());
                            }
                        }
                    }
                    else if (numberOfTargets == "Single")
                    {
                        abilityUsed = true;
                        targets.Add(set.GetComponent<BattleClick>());
                    }
                    else if(numberOfTargets == "Double")
                    {
                        if(secondSet == null)
                        {
                            secondSet = set;
                        }
                        else
                        {
                            targets.Add(set.GetComponent<BattleClick>());
                            targets.Add(secondSet.GetComponent<BattleClick>());
                            secondSet = null;
                            abilityUsed = true;
                        }
                    }
                }
            }
        }
        if (abilityUsed)
        {
            abilityList.callAbility(state.GetComponent<BattleAbility>(), targets);
            state.GetComponent<BattleAbility>().charge = 0;
            state.GetComponent<BattleAbility>().character.selectable = false;
            state = null;
            tryAI();
        }
    }

    public void tryAI()
    {
        bool playerDone = true;
        for (int x = 1; x <= 9; x++)
        {
            BattleClick temp = GameObject.Find("Character " + x).GetComponent<BattleClick>();
            if (temp.active)
            {
                if (temp.canAttack())
                {
                    playerDone = false;
                }

            }

        }
        if (playerDone)
        {
            runAI();
        }
    }

    public void runAI()
    {
        for (int x = 1; x <= 9; x++)
        {
            BattleClick temp = GameObject.Find("Character " + x).GetComponent<BattleClick>();
            BattleClick temp2 = GameObject.Find("Enemy " + x).GetComponent<BattleClick>();
            if (temp.active)
            {
                temp.selectable = true;
                temp.applyDebuffs();
                temp.unselected();
                temp.updateDebuffs();
            }
            if (temp2.active)
            {
                temp2.applyDebuffs();
                temp2.updateDebuffs();
            }
        }

        List<string> symptoms = new List<string>();

        for (int x = 1; x <= 9; x++)
        {
            BattleClick temp = GameObject.Find("Enemy " + x).GetComponent<BattleClick>();
            if (temp.canAttack())
            {
                GameObject.Find("Player HP Bar").GetComponent<Slider>().value -= temp.maxDamage;
                if (temp.symptoms.Contains("Pain"))
                {
                    symptoms.Add("Pain");
                }
                if (temp.symptoms.Contains("Fever"))
                {
                    symptoms.Add("Fever");
                }
                if (temp.symptoms.Contains("Diarrhea"))
                {
                    symptoms.Add("Diarrhea");
                }
                if (temp.symptoms.Contains("Cold"))
                {
                    symptoms.Add("Cold");
                }
            }
        }

        for (int x = 0; x < symptoms.Count; x++)
        {
            int newRand;
            do {newRand = UnityEngine.Random.Range(1, 10);}
            while (!GameObject.Find("Character " + newRand).GetComponent<BattleClick>().active);

            if (symptoms[x] == "Pain")
            {
                GameObject.Find("Character " + newRand).GetComponent<BattleClick>().addDebuff("Pain",1);
            }
            else if (symptoms[x] == "Fever")
            {
                GameObject.Find("Character " + newRand).GetComponent<BattleClick>().addDebuff("Fever", 3);
            }
            else if (symptoms[x] == "Diarrhea")
            {
                GameObject.Find("Character " + newRand).GetComponent<BattleClick>().addDebuff("Diarrhea", 2);
            }
            else if (symptoms[x] == "Cold")
            {
                GameObject.Find("Character " + newRand).GetComponent<BattleClick>().addDebuff("Cold", 1);
            }
        }

        for (int x = 1; x <= 9; x++)
        {
            BattleClick temp = GameObject.Find("Character " + x).GetComponent<BattleClick>();
            if (temp.active)
            {
                temp.countdown = 60;
                temp.updateDisplayText();
            }
        }
    }

    public int getIndex(string type, int position)
    {
        if (type == "OTC")
        {
            return OTCIndexes[position];
        }
        else if (type == "Character")
        {
            return characterIndexes[position];
        }
        return 1000;
    }

    public int[] indexes(string text, int size)
    {
        int[] temp = new int[size];
        using (StringReader sr = new StringReader(text))
        {
            string line;
            for (int x = 0; x < size; x++)
            {
                line = sr.ReadLine();
                try{temp[x] = int.Parse(line);}catch (Exception e){ temp[x] = 1000;}
            }
        }
        return temp;
    }

    public void setEnemies()
    {
        TextAsset text = Resources.Load("Enemy") as TextAsset;
        string txt = text.text;
        using (StringReader sr = new StringReader(txt))
        {
            string line;
            for (int x = 0; x < 9; x++)
            {
                line = sr.ReadLine();
                try { enemies.Add(line); } catch (Exception e) { }
            }
        }
    }

    public string getEnemy(int index)
    {
        return enemies[index - 1];
    }

    public void addColumn(int col)
    {
        enemyColumn[col] += 1;
    }

    public void removeColumn(int col)
    {
        enemyColumn[col] -= 1;
    }

    void Awake()
    {
        setEnemies();
        string text = File.ReadAllText("Assets/Resources/SaveFile.txt");
        characterIndexes = indexes(text, 9);
        for (int x = 0, y = 1; x < 9; x++)
        {
            if (characterIndexes[x] != 1000)
            {
                BattleAbility temp = GameObject.Find("Ability " + y++).GetComponent<BattleAbility>();
                temp.index = characterIndexes[x];
                temp.active = true;
            }
        }
    }

    void Start()
    {
        GameObject.Find("Background").transform.SetSiblingIndex(0);
    }
}