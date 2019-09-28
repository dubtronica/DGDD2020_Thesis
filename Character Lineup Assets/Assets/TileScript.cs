using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TileScript : MonoBehaviour
{
	public Placements placement;
	
	public Sprite selected, unselected;
	public bool onTile = false;
	public bool isTaken = false;
	public int tileNum;
	public Character characterOnTile;
	
	public Image tile;
	Image setup;
	public Image character;
	
	int numberOfCharacters = 3;
	
    // Start is called before the first frame update
    void Start()
    {
        tile = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && isTaken == false && onTile == true && placement.tilesTaken < 5){
			
			InstantiateCharacterHere();
			placement.tilesTaken++;
			
			for(int i = 0; i < numberOfCharacters; i++){
				if(character.sprite == placement.characters[i].characterFullBody){
					characterOnTile = placement.characters[i];
					placement.SpawnedCharacters[i].GetComponent<CharacterDetails>().alreadyPlaced = true;
					placement.SpawnedCharacters[i].GetComponent<CharacterDetails>().tileNum = tileNum;
					placement.setCharacter(tileNum, characterOnTile, setup);
				}
			}
			
		}
    }
	
	public void InstantiateCharacterHere(){
		character = placement.selectedCharacterFull;
		setup = Instantiate(character, new Vector3 (Input.mousePosition.x, Input.mousePosition.y + 46, 0),tile.transform.rotation, GameObject.FindGameObjectWithTag("Canvas").transform);
		Debug.Log(Input.mousePosition.y);
		setup.enabled = true;
		isTaken = true;
	}
	
	void OnTriggerEnter2D(Collider2D pic)
    {
		tile.sprite = selected;
		onTile = true;
	}
	
	void OnTriggerExit2D(Collider2D pic){
		tile.sprite = unselected;
		onTile = false;
	}
}
