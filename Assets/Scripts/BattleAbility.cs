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

    public string activeAbility;
    public string type;
    public int index;

    public int abilityNumber;
    public int charge = 0;
    public int maxCharge = 3;

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

    public void Start()
    {
        bc = FindObjectOfType<BattleController>();
        dataController = FindObjectOfType<DataController>();

        gameObject.AddComponent<Image>();

        addBG();

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
        bc.setState(gameObject);
    }

    public void Update()
    {
        if (charge >= maxCharge)
        {
            gameObject.GetComponent<Image>().color = new Color32(0, 255, 255, 100);
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
