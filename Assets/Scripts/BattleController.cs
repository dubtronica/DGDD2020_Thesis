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
    public static int[] enemyIndexes = new int[9];
    public static int[] OTCIndexes = new int[2];
    public static int[] enemyColumn = {0,0,0};
    public void setState(GameObject set)
    {
        BattleClick setbcComponent = set.GetComponent<BattleClick>();
        BattleClick statebcComponent = null;
        if (state != null)
        {
            statebcComponent = state.GetComponent<BattleClick>();
        }

        if (state == null)
        {
            if (set.name.Contains("Character"))
            {
                Debug.Log("Selected " + setbcComponent.name + ", HP: " + setbcComponent.maxHealth + ", DMG: " + setbcComponent.maxDamage + ", Type: " + setbcComponent.type);
                state = set;
            }
        }
        else if (state.name == set.name)
        {
            Debug.Log("Deselected " + statebcComponent.name);
            state = null;
        }
        else if(state.name.Contains("Character"))
        {
            if (set.name.Contains("Enemy"))
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
                        if (enemyColumn[0] == 0)
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
                    else if(setbcComponent.charPosition % 3 == 2)
                    {
                        if(enemyColumn[0] == 0)
                        {
                            attackable = true;
                        }
                    }
                    else if(setbcComponent.charPosition % 3 == 0)
                    {
                        if (enemyColumn[0] == 0 && enemyColumn[1] == 0)
                        {
                            attackable = true;
                        }
                    }
                }
                if (attackable)
                {
                    Debug.Log(statebcComponent.name + " Attacked " + set.name + " of type " + setbcComponent.type);
                    setbcComponent.takeDamage(statebcComponent.maxDamage, statebcComponent.type);
                    Debug.Log("Enemy Health Left: " + setbcComponent.maxHealth);
                    for (int x = 1; x <= 5; x++)
                    {
                        BattleAbility temp = GameObject.Find("Ability " + x).GetComponent<BattleAbility>();
                        if(temp.index == statebcComponent.index)
                        {
                            temp.charge++;
                        }
                        
                    }
                    state = null;
                }
                else
                {
                    Debug.Log("Unit Not In Range");
                }
            }
            else if (set.name.Contains("Character"))
            {
                Debug.Log("Selected " + setbcComponent.name + ", HP: " + setbcComponent.maxHealth + ", DMG: " + setbcComponent.maxDamage + ", Type: " + setbcComponent.type);
                state = set;
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
        else if (type == "Enemy")
        {
            return enemyIndexes[position];
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
        TextAsset text1 = Resources.Load("Character") as TextAsset;
        characterIndexes = indexes(text1, 9);
        for(int x = 0, y = 1; x < 9; x++)
        {
            if(characterIndexes[x] != 1000)
            {
                BattleAbility temp = GameObject.Find("Ability " + y++).GetComponent<BattleAbility>();
                temp.index = characterIndexes[x];
            }
        }
    }
    void Start()
    {

    }
    void Update()
    {

    }
}