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

    private CharacterData GetRandomCharacter() 
	{
		int num = Random.Range(0, characterList.Count);
		return characterList[num];
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
	
	public void BuyCharacter()
	{
		int amount = 10;
		if (playerData.currency1 >= amount)
		{
			CharacterData character = GetRandomCharacter();
			bool characterOwned = IsCharacterOwned(character);
			string text = characterOwned ? " Shard" : "";
			characterPullDisplay.text = "You received " + character.name + text;
			
			playerData.currency1 -= amount;
			
			if (!characterOwned) {
				playerData.ownedCharacters.characters.Add(character);
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
