using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BattleClick : MonoBehaviour, IPointerClickHandler
{
    private BattleController bc;
    private DataController dataController;

    #pragma warning disable CS0108
    public string name;
    public string passiveAbility;
    public string activeAbility;
    public string type;
    public int maxDamage;
    public int maxHealth;
    public int index;

    public double weaknessMultiplier = 2;
    public double resistanceMultiplier = 0.5;
    public string weakness;
    public string resistance;

    public int charPosition;
    private bool selectable = false;

    public void Start()
    {
        bc = FindObjectOfType<BattleController>();
        dataController = FindObjectOfType<DataController>();

        gameObject.AddComponent<Image>();

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
                    index = chars[x].index;
                    passiveAbility = chars[x].passiveAbility;
                    activeAbility = chars[x].passiveAbility;
                    type = chars[x].type;
                    maxDamage = chars[x].maxDamage;
                    maxHealth = chars[x].maxHealth;

                    if (type.Contains("Antibiotic"))
                    {
                        gameObject.GetComponent<Image>().color = new Color32(0, 0, 255, 100);
                    }
                    else if (type.Contains("ImmuneCell"))
                    {
                        gameObject.GetComponent<Image>().color = new Color32(0, 255, 0, 100);
                    }

                    selectable = true;
                    break;
                }
            }
        }
        else if (gameObject.name.Contains("Enemy"))
        {
            if (charPosition % 3 == 1)
            {
                type = "Virus";
                gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
                bc.addColumn(0);
            }
            else if(charPosition % 3 == 2)
            {
                type = "Bacteria Gram Negative";
                gameObject.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                bc.addColumn(1);
            }
            else if (charPosition % 3 == 0)
            {
                type = "Bacteria Gram Positive";
                gameObject.GetComponent<Image>().color = new Color32(255, 0, 255, 100);
                bc.addColumn(2);
            }
            setModifiers();
            maxHealth = 100;
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
        double newDmg = dmg;
        if (type.Contains(weakness))
        {
            newDmg *= weaknessMultiplier;
        }
        if (type.Contains(resistance))
        {
            newDmg *= resistanceMultiplier;
        }
        Debug.Log("Damage Taken: " + (int) newDmg);
        maxHealth -= (int) newDmg;

        if (maxHealth <= 0)
        {
            if (charPosition % 3 == 1)
            {
                bc.removeColumn(0);
            }
            else if (charPosition % 3 == 2)
            {
                bc.removeColumn(1);
            }
            else if (charPosition % 3 == 0)
            {
                bc.removeColumn(2);
            }
            maxHealth = 0;
            selectable = false;
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
        }
    }

    private void setModifiers()
    {
        if(type.Contains("Gram Positive"))
        {
            weakness = "Gram Positive";
            resistance = "Gram Negative";
        }
        else if (type.Contains("Gram Negative"))
        {
            weakness = "Gram Negative";
            resistance = "Gram Positive";
        }
        else if (type.Contains("Virus"))
        {
            weakness = "N/A";
            resistance = "Antibiotic";
            resistanceMultiplier = 0;
        }

    }
}
