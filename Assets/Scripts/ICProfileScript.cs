using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ICProfileScript : MonoBehaviour
{

    public ProfileElements prelems;

    public string name, description;
    public int hp, damage;
    // Start is called before the first frame update
    void Start()
    {
        upgrader.onClick.AddListener(upgrade);
    }

    // Update is called once per frame
    void Update()
    {
        charactername.text = name;
        HPValue.text = hp.ToString();
        DMGValue.text = damage.ToString();
        Description.text = description;
    }

    void upgrade()
    {
        hp += 30;
        damage += 15;
    }

    [System.Serializable]
    public class ProfileElements
    {
        public Text charactername, HPvalue, DMGvalue, Description;
        public Button upgrader;
    }

    public Text charactername { get { return prelems.charactername; } }
    public Text HPValue { get { return prelems.HPvalue; } }
    public Text DMGValue { get { return prelems.DMGvalue; } }

    public Text Description { get { return prelems.Description; } }

    public Button upgrader { get { return prelems.upgrader; } }
}
