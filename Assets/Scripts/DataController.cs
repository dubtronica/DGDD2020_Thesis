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
	
	public List<CharacterData> GetAllCharacterData()
	{
		return allCharacterData.characters;
	}
	
	public List<CharacterData> GetPlayerCharacterData()
	{
		return playerData.ownedCharacters.characters;
	}
	
	public PlayerData GetPlayerData() 
	{
		return playerData;
	}
	
	public void SavePlayerData(PlayerData data) 
	{
		string filePath;

		#if UNITY_EDITOR
			filePath = Path.Combine(Application.streamingAssetsPath, playerDataFilename);
		#elif !UNITY_EDITOR
			// retrieve from persistent data path
			filePath = Path.Combine(Application.persistentDataPath, playerDataFilename);
		#endif
		
		playerData = data;	
		
		string dataAsJson = JsonUtility.ToJson (playerData);
		File.WriteAllText(filePath, dataAsJson);
	}
	
	private void LoadPlayerData()
	{
		string filePath;

		#if UNITY_EDITOR
			filePath = Path.Combine(Application.streamingAssetsPath, playerDataFilename);
		#elif !UNITY_EDITOR
			// retrieve from persistent data path
			filePath = Path.Combine(Application.persistentDataPath, playerDataFilename);
		#endif
		
		if (File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			Debug.Log(dataAsJson);
			playerData = JsonUtility.FromJson<PlayerData>(dataAsJson);
			Debug.Log(playerData.ownedCharacters.characters[0].name);
		}
		else
		{
			PlayerData newPlayerData = new PlayerData();
			newPlayerData.experience = 0;
			newPlayerData.currency1 = 0;
			newPlayerData.currency2 = 0;
			newPlayerData.ownedCharacters = new Characters();
			
			string dataAsJson = JsonUtility.ToJson (newPlayerData);
			File.WriteAllText(filePath, dataAsJson);
		}
	}
	
	private void LoadCharacterData()
	{
		string filePath = characterDataFilename.Replace(".json", "");
		TextAsset characterFile = Resources.Load<TextAsset>(filePath);

		allCharacterData = JsonUtility.FromJson<Characters>(characterFile.ToString());
	}
}
