using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour
{
	private Characters allCharacterData;
	private PlayerData playerData;
	
	private string characterDataFilename = "CharacterSheet.json";
	private string playerDataFilename = "PlayerData.json";
	
    void Start()
    {
        DontDestroyOnLoad (gameObject);
		LoadCharacterData();
		LoadPlayerData();
		SceneManager.LoadScene ("MenuScreen");
    }
	
	public CharacterData[] GetCharacterData()
	{
		return allCharacterData.characters;
	}
	
	public PlayerData GetPlayerData() 
	{
		return playerData;
	}
	
	public void SavePlayerData(PlayerData data) 
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, playerDataFilename);
		playerData = data;	
		
		string dataAsJson = JsonUtility.ToJson (playerData);
		Debug.Log(dataAsJson);
		File.WriteAllText(filePath, dataAsJson);
	}
	
	private void LoadPlayerData()
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, playerDataFilename);
		
		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			playerData = JsonUtility.FromJson<PlayerData>(dataAsJson);
		}
		else
		{
			Debug.LogError("Cannot load player data");
		}
	}
	
	private void LoadCharacterData()
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, characterDataFilename);
		
		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			allCharacterData = JsonUtility.FromJson<Characters>(dataAsJson);
		}
		else
		{
			Debug.LogError("Cannot load character data");
		}
	}
}
