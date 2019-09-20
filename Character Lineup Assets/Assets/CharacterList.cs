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

    //public string[] charNames = new string[numberOfCharacters];
    //public Sprite[] charPics = new Sprite[numberOfCharacters];
	//public Sprite[] fullBodyPics = new Sprite[numberOfCharacters];
	//public Button[] charButtons = new Button[numberOfCharacters];
	
	public Character[] characters = new Character[numberOfCharacters];
	
	public Character chosenCharacter;
	
	public string chosenCharacterName;
	public Image chosenCharacterSprite;
	public Image box;

	// Spawns the "list of characters"
	void Start () {
		
		/*for(int j = 0; j < numberOfCharacters; j++){
			
			Character levi = ScriptableObject.CreateInstance<Character>();
			Character zoro = ScriptableObject.CreateInstance<Character>();
			Character natsu = ScriptableObject.CreateInstance<Character>();

			levi.character = CharacterType.LEVI;
			zoro.character = CharacterType.ZORO;
			natsu.character = CharacterType.NATSU;
		
		}*/
		
		/*for(int j = 0; j < numberOfCharacters; j++){
			charNames[j] = charArray[j].name;
			charPics[j] = charArray[j].characterHeadshot;
			fullBodyPics[j] = charArray[j].characterFullBody;
		}*/

        content.sizeDelta = new Vector2(numberOfCharacters * 65, 0);     

        for (int i = 0; i < numberOfCharacters; i++)
        {
            // 60 width of item
            float nextPosition = i * 65;

			Vector3 nextChar = new Vector3(nextPosition, 29.035f, SpawnPoint.position.z);

            GameObject SpawnedCharacter = Instantiate(characterBox, nextChar, SpawnPoint.rotation);            
            SpawnedCharacter.transform.SetParent(SpawnPoint, false);
			characterDetails = SpawnedCharacter.GetComponent<CharacterDetails>();
            characterDetails.characterName.text = characters[i].name;
			Image pic = characterDetails.characterPic.GetComponent<Image>();
            pic.sprite = characters[i].characterHeadshot;
			characterDetails.fullChar.sprite = characters[i].characterFullBody;
			characterDetails.characterBox.sprite = box.sprite;
			characterDetails.character = characters[i];
			//characterDetails.characterButton = charButtons[i];
			//characterDetails.characterButton = SpawnedCharacter.GetComponent<Button>();
			//characterDetails.characterButton.onClick.AddListener(delegate{chooseThisChar();});
			//charButtons[i] = SpawnedCharacter.GetComponent<Button>();
			//charButtons[i].onClick.AddListener(delegate{chooseThisChar(i);});
			
			//if same sprite, 
		}
    }
	
	void Update(){
		
		
		if(Input.touchCount > 0){
			
			Vector3 arena = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 place = new Vector2(arena.x, arena.y);
			
			if (chosenCharacter != null && boxCollider == Physics2D.OverlapPoint(place)){
				
				//chosenCharacterImage.transform.position = new Vector3(arena.x, arena.y, 0);
				//fullBodyChar.sprite = 
			}
		}
	}
	
	public void chooseThisChar(){
		//chosenCharacterName = charNames[num];
		//characterDetails.fullChar.sprite = fullBodyPics[0];
		//chosenCharacterSprite.sprite = fullBodyPics[0];
		//characterDetails.fullChar.enabled = true;
		
		//characterDetails.chooseCharacter();
		
	}
}
