using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Characters
{
	public CharacterData[] characters;
}


[System.Serializable]
public class CharacterData
{
	public string name;
	public string type;
    public int maxHealth;
	public int maxDamage;
}
