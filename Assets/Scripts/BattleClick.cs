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
    private GameObject charBG;
    public BattleAbility ability;

    #pragma warning disable CS0108
    public string name;
    public string passiveAbility;
    public string activeAbility;
    public string type;
    public int maxDamage;
    public int maxHealth;
    public int index;
    public string symptoms;
    public List<string> debuffs = new List<string>();

    public int charPosition;
    public bool active = false;
    public bool selectable = false;

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
        originalPosBG = charBG.transform.position;
    }

    public void Start()
    {
        originalPos = gameObject.transform.position;
        bc = FindObjectOfType<BattleController>();
        dataController = FindObjectOfType<DataController>();

        addBG();

        gameObject.AddComponent<Image>();
        gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 100);

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

                    GameObject.Find("Player HP Bar").GetComponent<Slider>().maxValue += maxHealth;
                    GameObject.Find("Player HP Bar").GetComponent<Slider>().value += maxHealth;

                    if (type.Contains("Antibiotic"))
                    {
                        gameObject.GetComponent<Image>().color = new Color32(0, 0, 255, 100);
                    }
                    else if (type.Contains("ImmuneCell"))
                    {
                        gameObject.GetComponent<Image>().color = new Color32(0, 255, 0, 100);
                    }
                    else
                    {
                        gameObject.GetComponent<Image>().color = new Color32(105, 105, 105, 100);
                    }

                    bc.addMaxPlayerActionCount();
                    active = true;
                    selectable = true;
                    break;
                }
            }
            for (int x = 1; x <= 5; x++)
            {
                BattleAbility tmp = GameObject.Find("Ability " + x).GetComponent<BattleAbility>();
                if (!(index == 1000) && tmp.index == index)
                {
                    ability = tmp;
                    break;
                }
            }
        }
        else if (gameObject.name.Contains("Enemy"))
        {
            string enemy = bc.getEnemy(charPosition);
            string[] stats = enemy.Split();
            name = stats[0];
            if (name != "NA")
            {
                maxHealth = Int32.Parse(stats[1]);
                maxDamage = Int32.Parse(stats[2]);
                type = stats[3];
                symptoms = stats[4];
                setModifiers();
                selectable = true;
                if (charPosition % 3 == 1) { bc.addColumn(0); }
                else if (charPosition % 3 == 2) { bc.addColumn(1); }
                else if (charPosition % 3 == 0) { bc.addColumn(2); } 
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectable && bc.getPlayerTurn())
        {
            bc.setState(gameObject);
        }
    }

    public double weaknessMultiplier = 2;
    public double resistanceMultiplier = 0.5;
    public string weakness;
    public string resistance;

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
        if(type.Contains("Positive"))
        {
            gameObject.GetComponent<Image>().color = new Color32(0, 255, 255, 100);
            weakness = "Positive";
            resistance = "Negative";
        }
        else if (type.Contains("Negative"))
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 0, 255, 100);
            weakness = "Negative";
            resistance = "Positive";
        }
        else if (type.Contains("Virus"))
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            weakness = "N/A";
            resistance = "Antibiotic";
            resistanceMultiplier = 0;
        }
    }

    float speed = 5.0f;
    float amount = 0.05f;
    Vector3 originalPos;
    Vector3 originalPosBG;
    public int countdown;

    void Update()
    {
        if (countdown > 0)
        { 
            Vector3 pos = gameObject.transform.position;
            Quaternion q = gameObject.transform.rotation;
            pos.x += Mathf.Sin(Time.time * speed) * amount;
            pos.y += Mathf.Sin(Time.time * speed) * amount;
            gameObject.transform.SetPositionAndRotation(pos, q);

            pos = charBG.transform.position;
            q = charBG.transform.rotation;
            pos.x += Mathf.Sin(Time.time * speed) * amount;
            pos.y += Mathf.Sin(Time.time * speed) * amount;
            charBG.transform.SetPositionAndRotation(pos, q);

            countdown--;
        }
        else
        {
            Quaternion q = gameObject.transform.rotation;
            gameObject.transform.SetPositionAndRotation(originalPos, q);
            q = charBG.transform.rotation;
            charBG.transform.SetPositionAndRotation(originalPosBG, q);
        }

        if (!selectable)
        {
            charBG.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
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
