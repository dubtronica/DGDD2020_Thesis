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
    public static int[] characterIndexes = new int[9];
    public static int[] OTCIndexes = new int[2];
    public static List<string> enemies = new List<String>();

    public static int[] enemyColumn = {0,0,0};

    public static bool playerTurn = true;
    public static int maxPlayerActions = 0;
    public static int playerActionCount = 0;

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

        if (state == null)
        {
            if (set.name.Contains("Character"))
            {
                BattleClick temp = set.GetComponent<BattleClick>();
                Debug.Log("Selected " + temp.name + ", HP: " + temp.maxHealth + ", DMG: " + temp.maxDamage + ", Type: " + temp.type);
                state = set;
            }
            else if (set.name.Contains("Ability"))
            {
                Debug.Log("Selected " + set.name);
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
            else if (set.GetComponent<BattleAbility>() != null)
            {
                BattleAbility temp = set.GetComponent<BattleAbility>();
                Debug.Log("Selected " + temp.name);
                state = set;
            }
        }
        else if (state.GetComponent<BattleAbility>() != null)
        {
            if (set.GetComponent<BattleClick>() != null)
            {

            }
            else if (set.GetComponent<BattleAbility>() != null)
            {
                if(state.name == set.name)
                {
                    state = null;
                    Debug.Log("Deselected Ability");
                }
                else
                {
                    BattleAbility temp = set.GetComponent<BattleAbility>();
                    Debug.Log("Selected " + temp.name);
                    state = set;
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
                Debug.Log("Selected " + setbcComponent.name + ", HP: " + setbcComponent.maxHealth + ", DMG: " + setbcComponent.maxDamage + ", Type: " + setbcComponent.type);
                state = set;
            }
            else if (set.name.Contains("Enemy"))
            {
                bool attackable = false;
                if (statebcComponent.type.Contains("Antibiotic"))
                {
                    if (setbcComponent.charPosition % 3 == 1 || setbcComponent.charPosition % 3 == 2)
                    {
                        attackable = true;
                    }
                    else if (setbcComponent.charPosition % 3 == 0)
                    {
                        if (enemyColumn[0] == 0 || enemyColumn[1] == 0)
                        {
                            attackable = true;
                        }
                    }
                }
                else if (statebcComponent.type.Contains("ImmuneCell"))
                {
                    if (setbcComponent.charPosition % 3 == 1)
                    {
                        attackable = true;
                    }
                    else if (setbcComponent.charPosition % 3 == 2)
                    {
                        if (enemyColumn[0] == 0)
                        {
                            attackable = true;
                        }
                    }
                    else if (setbcComponent.charPosition % 3 == 0)
                    {
                        if (enemyColumn[0] == 0 && enemyColumn[1] == 0)
                        {
                            attackable = true;
                        }
                    }
                }
                if (attackable && playerActionCount != maxPlayerActions)
                {
                    Debug.Log(statebcComponent.name + " Attacked " + set.name + " of type " + setbcComponent.type + " with " + setbcComponent.maxHealth + " hp");
                    setbcComponent.takeDamage(statebcComponent.maxDamage, statebcComponent.type);
                    Debug.Log("Enemy Health Left: " + setbcComponent.maxHealth);
                    setbcComponent.countdown = 60;
                    statebcComponent.ability.charge++;
                    statebcComponent.selectable = false;
                    playerActionCount++;
                    state = null;
                    if (playerActionCount == maxPlayerActions)
                    {
                        runAI();
                        playerActionCount = 0;
                        for (int x = 1; x <= 9; x++)
                        {
                            BattleClick temp = GameObject.Find("Character " + x).GetComponent<BattleClick>();
                            if (temp.active)
                            {
                                temp.selectable = true;
                                temp.unselected();
                            }

                        }
                    }
                }
                else
                {
                    Debug.Log("Unit Not In Range");
                }
            }
        }
    }

    private void abilityHandler(GameObject set)
    {

    }

    public void runAI()
    {
        List<string> symptoms = new List<string>();

        for (int x = 1; x <= 9; x++)
        {
            BattleClick temp = GameObject.Find("Enemy " + x).GetComponent<BattleClick>();
            if (temp.selectable)
            {
                GameObject.Find("Player HP Bar").GetComponent<Slider>().value -= temp.maxDamage;
                if (temp.symptoms.Contains("Pain"))
                {
                    symptoms.Add("Pain");
                }
                else if (temp.symptoms.Contains("Fever"))
                {
                    symptoms.Add("Fever");
                }
                else if (temp.symptoms.Contains("Diarrhea"))
                {
                    symptoms.Add("Diarrhea");
                }
                else if (temp.symptoms.Contains("Cold"))
                {
                    symptoms.Add("Cold");
                }
            }
        }

        for (int x = 1; x <= 9; x++)
        {
            BattleClick temp = GameObject.Find("Character " + x).GetComponent<BattleClick>();
            if (temp.active)
            {
                temp.countdown = 60;
            }
        }
    }

    public bool getPlayerTurn()
    {
        return playerTurn;
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

    public int[] indexes(TextAsset text, int size)
    {
        string txt = text.text;

        int[] temp = new int[size];
        using (StringReader sr = new StringReader(txt))
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

    public int getPlayerActionCount()
    {
        return playerActionCount;
    }

    public void addMaxPlayerActionCount()
    {
        maxPlayerActions++;
    }

    void Awake()
    {
        setEnemies();
        TextAsset text1 = Resources.Load("Character") as TextAsset;
        characterIndexes = indexes(text1, 9);
        for (int x = 0, y = 1; x < 9; x++)
        {
            if (characterIndexes[x] != 1000)
            {
                BattleAbility temp = GameObject.Find("Ability " + y++).GetComponent<BattleAbility>();
                temp.index = characterIndexes[x];
            }
        }
    }
    void Start()
    {
        GameObject.Find("Background").transform.SetSiblingIndex(0);
    }
}