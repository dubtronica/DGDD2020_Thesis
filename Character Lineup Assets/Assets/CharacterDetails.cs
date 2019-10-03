using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterDetails : MonoBehaviour
{
	
	public Button characterPic;
	public Text characterName, details;
	//public Button characterButton;
	public Image pic, fullChar, OTCpic;
	public Character character;
	public Image characterBox;
	public Sprite unselected;
	
	Vector3 start, end;
	public Transform fullpic;
	
	//temporary stuff
	private DragCharacter dragScript;
	private TileScript tileScript;
	
	public Placements placement;
	
	public bool alreadyPlaced = false;
	public int tileNum;
	
	int count = 0;
	TileScript  ts;
	
	public Image charOnTile;
	
    // Start is called before the first frame update
    void Start()
    {
		characterBox.enabled = true;
		pic = characterPic.GetComponent<Image>();
		pic.enabled = true;
		characterPic.onClick.AddListener(chooseCharacter);
		
		if(fullChar != null){
			fullChar.enabled = false;
			fullpic = fullChar.GetComponent<Transform>();
		}
		
    }

    // Update is called once per frame
    void Update()
    {
		
		if(fullChar != null){
			try
			{
				
				if(dragScript!= null && dragScript.returned == true && alreadyPlaced == true){
					Destroy(pic.gameObject.GetComponent<DragCharacter>());
					//Debug.Log("destroy");
					count = 0;
				}
				
				charOnTile = placement.getCharacter(tileNum);
				
			}
			catch(NullReferenceException exception)
			{
			   
			}
		}
		
		
    }
	
	public void chooseCharacter(){
		
		if(fullChar != null){
			if(alreadyPlaced == true){
			
				Destroy(charOnTile);
				Debug.Log("destroy");
				placement.setCharacter(tileNum, null, null);
				alreadyPlaced = false;
				placement.tilesTaken--;
				ts = placement.allyTile[tileNum].GetComponent<TileScript>();
				ts.isTaken = false;
				ts.character = null;
				ts.onTile = false;
			}
			else{
				
				count++;
			
				placement.selectedCharacter = character;
				placement.selectedCharacterFull = fullChar;
				Debug.Log(character.name + " selected");
				
				dragScript = pic.gameObject.AddComponent<DragCharacter>();
				dragScript.picture = pic;
			}
		}
		else{
			
			OTCpic.sprite = character.characterHeadshot;
			characterName.text = character.name;
			details.text = character.description;
						
		}
		
		

		
	}
	
	//so after placing it, make another object on that exact area, that you can stack info on
	//make it so users drag the headshot to the area then only once you collide finger with tile and release, then make the full pic "appear"
	//so that avoid making feet "match" the tile
	//add script to tile upon collision so that if it senses the "object" dragged by the mouse, it "creates" / instantiates an object on it.
}
