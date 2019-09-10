using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanelScript : MonoBehaviour
{
    
	private DataController dataController;
	private PlayerData playerData;
	
	public Text playerXPDisplayText;
	public Text currency1DisplayText;
	public Text currency2DisplayText;
	
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
		playerData = dataController.GetPlayerData();
		UpdateScreenPlayerInfo();
    }
	
	public void UpdateScreenPlayerInfo()
	{
		playerXPDisplayText.text = "Player XP: " + playerData.experience.ToString();
		currency1DisplayText.text = "Currency 1: " + playerData.currency1.ToString();
		currency2DisplayText.text = "Currency 2: " + playerData.currency2.ToString();
		
	}
	
	// tester function
	public void AddXP()
	{
		playerData.experience += 50;
		UpdateScreenPlayerInfo();
		
		// write to JSON
		dataController.SavePlayerData(playerData);
	}
	
	// tester function
	public void AddCurrency1()
	{
		playerData.currency1 += 5;
		UpdateScreenPlayerInfo();
		
		// write to JSON
		dataController.SavePlayerData(playerData);
	}
	
	// tester function
	public void AddCurrency2()
	{
		playerData.currency2 += 1;
		UpdateScreenPlayerInfo();
		
		// write to JSON
		dataController.SavePlayerData(playerData);
	}
}
