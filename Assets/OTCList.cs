using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OTCList : MonoBehaviour
{
	[SerializeField]
    private Transform SpawnPoint;

    [SerializeField]
    private GameObject characterBox;

    [SerializeField]
    private RectTransform content;

    [SerializeField]
    private static int numberOfCharacters = 2;
	
	private CharacterDetails characterDetails;
	
	public Character[] characters = new Character[numberOfCharacters];
	public GameObject[] SpawnedCharacters = new GameObject[numberOfCharacters];
	
	public Character chosenCharacter;
	
	public string chosenCharacterName;
	public Image chosenCharacterSprite;
	public Image box;
	
	public Button picButton;

	// Spawns the "list of characters"
	
	void Start () {
    }
	
	void Update(){
		
		//Debug.Log("false");
	}
	
	public void Spawn(){
		content.sizeDelta = new Vector2((numberOfCharacters / 3) * 65, (numberOfCharacters / 3) * 65);     

        for (int i = 0; i < numberOfCharacters; i++)
        {
            // 60 width of item
            float nextPosition = i * 65;

			Vector3 nextChar = new Vector3(nextPosition, -149, SpawnPoint.position.z);

            SpawnedCharacters[i] = Instantiate(characterBox, nextChar, SpawnPoint.rotation);            
            SpawnedCharacters[i].transform.SetParent(SpawnPoint, false);
			
			characterDetails = SpawnedCharacters[i].GetComponent<CharacterDetails>();
			characterDetails.characterBox.sprite = box.sprite;
			characterDetails.character = characters[i];
            characterDetails.pic.sprite = characters[i].characterHeadshot;
			
			
		}
	}
	
}
