using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterList : MonoBehaviour
{
	[SerializeField]
    private Transform SpawnPoint;

    [SerializeField]
    private GameObject characterBox;

    [SerializeField]
    private RectTransform content;

    [SerializeField]
    private static int numberOfCharacters = 3;
	
	private BoxCollider2D boxCollider;
	
	private CharacterDetails characterDetails;
	
	public Character[] characters = new Character[numberOfCharacters];
	public GameObject[] SpawnedCharacters = new GameObject[numberOfCharacters];
	
	public Character chosenCharacter;
	
	public string chosenCharacterName;
	public Image chosenCharacterSprite;
	public Image box;
	
	public Placements placement;

	// Spawns the "list of characters"
	void Start () {

        content.sizeDelta = new Vector2(numberOfCharacters * 65, 0);     

        for (int i = 0; i < numberOfCharacters; i++)
        {
            // 60 width of item
            float nextPosition = i * 65;

			Vector3 nextChar = new Vector3(nextPosition, 29.035f, SpawnPoint.position.z);

            SpawnedCharacters[i] = Instantiate(characterBox, nextChar, SpawnPoint.rotation);            
            SpawnedCharacters[i].transform.SetParent(SpawnPoint, false);
			
			characterDetails = SpawnedCharacters[i].GetComponent<CharacterDetails>();
            characterDetails.characterName.text = characters[i].name;
			
			Image pic = characterDetails.characterPic.GetComponent<Image>();
            pic.sprite = characters[i].characterHeadshot;
			
			characterDetails.fullChar.sprite = characters[i].characterFullBody;
			characterDetails.characterBox.sprite = box.sprite;
			characterDetails.character = characters[i];
		}
    }
	
	void Update(){
		
	}
	
}
