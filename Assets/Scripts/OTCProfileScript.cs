using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OTCProfileScript : MonoBehaviour
{
    public ProfileElements prelems;

    public string name, description;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        charactername.text = name;
        Description.text = description;
    }

    [System.Serializable]
    public class ProfileElements
    {
        public Text charactername, Description;

    }

    public Text charactername { get { return prelems.charactername; } }
    public Text Description { get { return prelems.Description; } }

}
