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
    private GameObject text;
    public BattleAbility ability;

    public new string name;
    public string type;
    public int maxDamage;
    public int currentHealth;
    public int maxHealth;
    public int index;
    public int rarity;
    public string symptoms;

    public int charPosition;
    public bool active = false;
    public bool selectable = false;

    public bool canAttack()
    {
        if(selectable && !hasDebuff("Cold"))
        {
            return true;
        }
        return false;
    }

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

    public void addText()
    {
        Canvas cv = bc.GetComponent<Canvas>();
        text = new GameObject("Text");
        RectTransform tempRT = gameObject.GetComponent<RectTransform>();
        text.AddComponent<RectTransform>();
        text.GetComponent<RectTransform>().anchorMin = tempRT.anchorMin;
        text.GetComponent<RectTransform>().anchorMax = tempRT.anchorMax;
        text.GetComponent<RectTransform>().anchoredPosition = tempRT.anchoredPosition;
        text.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 60);
        text.AddComponent<Text>();
        text.transform.SetParent(cv.transform, false);
        text.GetComponent<Text>().color = new Color32(0, 0, 0, 255);
        text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        text.GetComponent<Text>().fontSize = 9;
        text.transform.SetSiblingIndex(100);
    }

    public void Start()
    {
        originalPos = gameObject.transform.position;
        bc = FindObjectOfType<BattleController>();
        dataController = FindObjectOfType<DataController>();

        addBG();
        addText();

        gameObject.AddComponent<Image>();
        gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 100);

        setUpUnit();
        updateDisplayText();
    }

    public void setUpUnit()
    {
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
                if (chars[x].index == bc.getIndex("Character", charPosition - 1))
                {
                    name = chars[x].name;
                    index = chars[x].index;
                    type = chars[x].type;
                    maxDamage = chars[x].maxDamage;
                    currentHealth = chars[x].maxHealth;
                    rarity = chars[x].rarity;

                    GameObject.Find("Player HP Bar").GetComponent<Slider>().maxValue += currentHealth;
                    GameObject.Find("Player HP Bar").GetComponent<Slider>().value += currentHealth;

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

                    active = true;
                    selectable = true;

                    for (int y = 1; y <= 5; y++)
                    {
                        BattleAbility tmp = GameObject.Find("Ability " + y).GetComponent<BattleAbility>();
                        if (!(index == 1000) && tmp.index == index)
                        {
                            ability = tmp;
                            tmp.character = gameObject.GetComponent<BattleClick>();
                            break;
                        }
                    }

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
                currentHealth = Int32.Parse(stats[1]);
                maxHealth = currentHealth;
                maxDamage = Int32.Parse(stats[2]);
                type = stats[3];
                symptoms = stats[4];
                setModifiers();
                selectable = true;
                active = true;
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
        if (canAttack())
        {
            bc.setState(gameObject);
        }
    }

    public double weaknessMultiplier = 2;
    public double resistanceMultiplier = 0.5;
    public string weakness;
    public string resistance;

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

        if (!canAttack())
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

    public void setAsDead()
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
        currentHealth = 0;
        selectable = false;
        active = false;
        gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
    }

    public int getDmg()
    {
        double tempDmg = maxDamage;
        if (hasDebuff("Diarrhea"))
        {
            tempDmg /= 2;
        }
        if (hasDebuff("Double Damage"))
        {
            tempDmg *= 2;
        }
        if (hasDebuff("Enhanced"))
        {
            tempDmg *= 1.5;
        }
        return (int) tempDmg;
    }

    public void autoTakeDamage(BattleClick unit)
    {
        takeDamage(unit.getDmg(), unit);
    }

    public void takeDamage(int dmg, BattleClick unit)
    {
        countdown = 60;
        double newDmg = dmg;

        if (unit.type.Contains(weakness))
        {
            newDmg *= weaknessMultiplier;
        }
        if (unit.type.Contains(resistance))
        {
            newDmg *= resistanceMultiplier;
        }
        if (hasDebuff("Marked"))
        {
            newDmg *= 1.5;
        }
        if (hasDebuff("Vulnerable"))
        {
            if((float) maxHealth / (float) currentHealth > 2)
            {
                newDmg *= 2;
            }
        }

        Debug.Log("Damage Taken: " + (int)newDmg);
        int healthBefore = currentHealth;
        currentHealth -= (int)newDmg;

        if (hasDebuff("Heals"))
        {
            if (currentHealth < 0)
            {
                GameObject.Find("Player HP Bar").GetComponent<Slider>().value += (float)(healthBefore / 2);
            }
            else
            {
                GameObject.Find("Player HP Bar").GetComponent<Slider>().value += (float)(newDmg / 2);
            }
        }

        if (currentHealth <= 0)
        {
            setAsDead();
        }

        updateDisplayText();
    }

    public class Debuff
    {
        public string name;
        public int duration;

        public Debuff(string n, int d)
        {
            name = n;
            duration = d;
        }
    }

    public List<Debuff> debuffs = new List<Debuff>();

    public void updateDebuffs()
    {
        Debug.Log("got here " + gameObject.name);
        for (int x = 0; x < debuffs.Count;)
        {
            debuffs[x].duration--;
            if (debuffs[x].duration == 0)
            {
                debuffs.RemoveAt(x);
            }
            else
            {
                x++;
            }
        }
        updateDisplayText();
    }

    public bool hasDebuff(string check)
    {
        for (int x = 0; x < debuffs.Count; x++)
        {
            if (debuffs[x].name == check)
            {
                return true;
            }
        }
        return false;
    }

    public void applyDebuffs()
    {
        if (gameObject.name.Contains("Enemy"))
        {
            if (hasDebuff("Warning"))
            {
                int rand = UnityEngine.Random.Range(1, 1001);
                if (rand <= 75)
                {
                    setAsDead();
                }
            }
            if (hasDebuff("Poison"))
            {
                currentHealth -= maxHealth / 5;
                if(currentHealth <= 0)
                {
                    setAsDead();
                }
            }
        }
        else if (gameObject.name.Contains("Character"))
        {
            if (hasDebuff("Pain"))
            {
                GameObject.Find("Player HP Bar").GetComponent<Slider>().value -= currentHealth / 3;
            }
            if (hasDebuff("Fever"))
            {
                GameObject.Find("Player HP Bar").GetComponent<Slider>().value -= currentHealth / 5;
            }
        }
    }
    
    public void updateDisplayText()
    {
        if(gameObject.name.Contains("Character"))
        {
            Text temp = text.GetComponent<Text>();
            if (active)
            {
                temp.text = name + "\nDmg: " + maxDamage + "\n" + type + "\n";
                for (int x = 0; x < debuffs.Count; x++)
                {
                    temp.text += debuffs[x].name + " ";
                }
            }
            else
            {
                temp.text = "";
            }
        }
        else if (gameObject.name.Contains("Enemy"))
        {
            Text temp = text.GetComponent<Text>();
            if (active)
            {
                temp.text = name + "\nDmg: " + maxDamage + "\nHP: " + currentHealth + "\n" + type + "\n";
                for (int x = 0; x < debuffs.Count; x++)
                {
                    temp.text += debuffs[x].name + " ";
                }
            }
            else
            {
                temp.text = "";
            }
        }
    }

    public void addDebuff(string name, int duration)
    {
        Debuff debuff = new Debuff(name, duration);
        bool found = false;
        for(int x = 0; x < debuffs.Count; x++)
        {
            if(debuffs[x].name == debuff.name)
            {
                if(debuffs[x].duration < debuff.duration)
                {
                    debuffs[x].duration = debuff.duration;
                }
                found = true;
                break;
            }
        }
        if (!found)
        {
            debuffs.Add(debuff);
        }
        updateDisplayText();
    }
}
