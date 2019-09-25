using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Characters
{
	public List<CharacterData> characters;
}


[System.Serializable]
public class CharacterData
{
	public int index;
	public string name;
	public string type;
   	public int maxHealth;
	public int maxDamage;
	public int rarity;
	public string passiveAbility;
	public string activeAbility;
	public int shardCount;
	public int xp;
}
