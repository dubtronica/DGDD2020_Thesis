using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType{LEVI, ZORO, NATSU};
[CreateAssetMenu]

public class Character : ScriptableObject{

	public CharacterType character;
	public string name;
	//public string itemName;
	public string description;
	public Sprite characterHeadshot;
	public Sprite characterFullBody;
	
	public float attackPower = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
