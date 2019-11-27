using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BattleAbility : MonoBehaviour, IPointerClickHandler
{
    private BattleController bc;
    private DataController dataController;
    private GameObject charBG;
    private GameObject text;
    public BattleClick character;

    public string activeAbility;
    public string type;
    public int index;

    public int charge = 0;
    public int maxCharge = 3;
    public bool active = false;

    public void addBG()
    {
        Canvas cv = bc.GetComponent<Canvas>();
        charBG = new GameObject("BG");
        RectTransform tempRT = gameObject.GetComponent<RectTransform>();
        charBG.AddComponent<RectTransform>();
        RectTransform toadd = charBG.GetComponent<RectTransform>();
        toadd.anchorMin = tempRT.anchorMin;
        toadd.anchorMax = tempRT.anchorMax;
        toadd.anchoredPosition = tempRT.anchoredPosition;
        toadd.sizeDelta = new Vector2(82, 82);
        charBG.AddComponent<Image>();
        charBG.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        charBG.transform.SetParent(cv.transform, false);
        charBG.transform.SetSiblingIndex(1);
    }

    public void addText()
    {
        Canvas cv = bc.GetComponent<Canvas>();
        text = new GameObject("Text");
        RectTransform tempRT = gameObject.GetComponent<RectTransform>();
        text.AddComponent<RectTransform>();
        text.GetComponent<RectTransform>().anchorMin = tempRT.anchorMin;
        text.GetComponent<RectTransform>().anchorMax = tempRT.anchorMax;
        text.GetComponent<RectTransform>().anchoredPosition = tempRT.anchoredPosition;
        text.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
        text.AddComponent<Text>();
        text.transform.SetParent(cv.transform, false);
        text.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
        text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        text.GetComponent<Text>().fontSize = 8;
        text.transform.SetSiblingIndex(100);
    }

    public void Start()
    {
        bc = FindObjectOfType<BattleController>();
        dataController = FindObjectOfType<DataController>();

        gameObject.AddComponent<Image>();

        addBG();
        addText();

        List<CharacterData> chars = dataController.GetPlayerData().ownedCharacters.characters;
        for (int x = 0; x < chars.Count; x++)
        {
            if(index == chars[x].index && active)
            {
                type = chars[x].type;
                activeAbility = chars[x].activeAbility;
                break;
            }
        }

        text.GetComponent<Text>().text = activeAbility;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(charge >= 3 && character.canAttack())
        {
            bc.setState(gameObject);
        }
    }

    public void Update()
    {
        if (charge >= maxCharge)
        {
            gameObject.GetComponent<Image>().color = new Color32(0, 255, 255, 100);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }

    public void selected()
    {
        charBG.GetComponent<Image>().color = new Color32(0, 0, 255, 100);
    }

    public void unselected()
    {
        charBG.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
    }
}
