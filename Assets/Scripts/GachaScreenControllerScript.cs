using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GachaScreenControllerScript : MonoBehaviour
{
    private List<CharacterData> characterList;
	private DataController dataController;
	private PlayerData playerData;
	
	public Text characterPullDisplay;
	public Text warningDisplay;
	public GameObject OKButton;
	public GameObject rewardPanel;
	public GameObject confirmationPanel;
	public GameObject playerInfo;
	
	void Start()
	{
		dataController = FindObjectOfType<DataController>();
		characterList = dataController.GetAllCharacterData();
		playerData = dataController.GetPlayerData();
	}

    	private CharacterData GetRandomCharacter(bool premium) 
	{
		int num = 0;
		if(premium){num = Random.Range(51,100);}
		else{num = Random.Range(1,100);}
		
		int rarity = 0;
		if(num > 50){rarity++;}
		if(num > 85){rarity++;}
		
		List<CharacterData> tempList = new List<CharacterData>();
		for(int x = 0; x < characterList.Count; x++){
			if(characterList[x].rarity == rarity){
				tempList.Add(characterList[x]);
			}
		}
		
		int charNum = Random.Range(0, tempList.Count);
		return tempList[charNum];
	}
	
	private bool IsCharacterOwned(CharacterData character)
	{
		for (int i = 0; i < playerData.ownedCharacters.characters.Count; i++)
		{
			if (playerData.ownedCharacters.characters[i].index == character.index)
			{
				return true;
			}
		}
		return false;
	}
	
	private void addShard(CharacterData character)
	{
		for (int i = 0; i < playerData.ownedCharacters.characters.Count; i++)
		{
			if (playerData.ownedCharacters.characters[i].index == character.index)
			{
				playerData.ownedCharacters.characters[i].shardCount++;
			}
		}
	}
	
	public void BuyCharacter()
	{
		int amount = 10;
		if (playerData.currency1 >= amount)
		{
			CharacterData character = GetRandomCharacter(false);
			bool characterOwned = IsCharacterOwned(character);
			string text = characterOwned ? " Shard" : "";
			characterPullDisplay.text = "You received " + character.name + text;
			
			playerData.currency1 -= amount;
			
			if (!characterOwned) {
				playerData.ownedCharacters.characters.Add(character);
			}
			else{
				addShard(character);
			}
			
			dataController.SavePlayerData(playerData);
			playerInfo.GetComponent<PlayerInfoPanelScript>().UpdateScreenPlayerInfo();
			rewardPanel.SetActive(true);
			confirmationPanel.SetActive(false);
		}
		else
		{
			warningDisplay.text = "Not enough money!";
			OKButton.SetActive(false);
		}
		
	}
	
	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene(1);
	}
}
