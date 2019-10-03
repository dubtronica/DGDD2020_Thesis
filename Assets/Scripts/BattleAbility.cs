using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BattleAbility : MonoBehaviour, IPointerClickHandler
{
    private DataController dataController;

    public string activeAbility;
    public string type;
    public int index;

    public int abilityNumber;
    public int charge = 0;
    public int maxCharge = 3;

    public void Start()
    {
        gameObject.AddComponent<Image>();

        string temp = gameObject.name;
        for (int x = 0; x < temp.Length; x++)
        {
            if (Char.IsDigit(temp[x]))
            {
                abilityNumber = temp[x] - '0';
                break;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
    }

    public void Update()
    {
        if(charge >= maxCharge)
        {
            gameObject.GetComponent<Image>().color = new Color32(0, 255, 255, 100);
        }
    }
}
