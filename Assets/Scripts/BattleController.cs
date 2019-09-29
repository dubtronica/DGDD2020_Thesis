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

    public int getIndex(string type, int position)
    {
        if(type == "OTC")
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
    public void setState(GameObject set)
    {
        if (state == null)
        {
            if (set.name.Contains("Character"))
            {
                Debug.Log("Selected " + set.GetComponent<BattleClick>().name + ", HP: " + set.GetComponent<BattleClick>().maxHealth + ", DMG: " + set.GetComponent<BattleClick>().maxDamage);
                state = set;
            }
            else if (set.name.Contains("OTC"))
            {
                Debug.Log("Selected " + set.name);
                state = set;
            }
        }
        else if (state.name == set.name)
        {
            Debug.Log("Deselected " + state);
            state = null;
        }
        else if(state.name.Contains("Character"))
        {
            if (set.name.Contains("Enemy"))
            {
                Debug.Log(state.name + " Attacked " + set.name);
                set.GetComponent<BattleClick>().takeDamage(state.GetComponent<BattleClick>().maxDamage, state.GetComponent<BattleClick>().type);
                Debug.Log("Enemy Health Left: " + set.GetComponent<BattleClick>().maxHealth);

                state = null;
            }
            else
            {
                Debug.Log("Selected " + set.name);
                state = set;
            }
        }
        else if(state.name.Contains("OTC"))
        {
            if(set.name.Contains("Character"))
            {
                Debug.Log("OTC Applied To " + set.name);
                state = null;
            }
            else if(set.name.Contains("OTC"))
            {
                Debug.Log("Selected " + set.name);
                state = set;
            }
        }
    }

    public void printState()
    {
        Debug.Log(state.name);
    }

    void Awake()
    {
        TextAsset text1 = Resources.Load("Character") as TextAsset;
        characterIndexes = indexes(text1, 9);
    }
    void Start()
    {
        
    }

    void Update()
    {

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
}