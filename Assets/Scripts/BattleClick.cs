using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class BattleClick : MonoBehaviour, IPointerClickHandler
{
    private BattleController bc;

    private DataController dataController;

    private int charPosition;

    public string name;
    public string passiveAbility;
    public string activeAbility;
    public string type;
    public int maxDamage;
    public int maxHealth;

    private bool selectable = false;

    public void Start()
    {
        bc = FindObjectOfType<BattleController>();
        dataController = FindObjectOfType<DataController>();

        string temp = gameObject.name;
        for (int x = 0; x < temp.Length; x++)
        {
            if (Char.IsDigit(temp[x]))
            {
                charPosition = temp[x] - '0';
                break;
            }
        }

        if (gameObject.name.Contains("Character"))
        {
            List<CharacterData> chars = dataController.GetPlayerData().ownedCharacters.characters;
            for (int x = 0; x < chars.Count; x++)
            {
                if(chars[x].index == bc.getIndex("Character", charPosition - 1))
                {
                    name = chars[x].name;
                    passiveAbility = chars[x].passiveAbility;
                    activeAbility = chars[x].passiveAbility;
                    type = chars[x].type;
                    maxDamage = chars[x].maxDamage;
                    maxHealth = chars[x].maxHealth;
                    selectable = true;
                    break;
                }
            }
        }
        else if (gameObject.name.Contains("Enemy"))
        {
            maxHealth = 100;
            selectable = true;
        }
        else if (gameObject.name.Contains("OTC"))
        {
            selectable = true;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectable)
        {
            bc.setState(gameObject);
        }
    }

    public void takeDamage(int dmg, string type)
    {
        maxHealth -= dmg;
    }
}
