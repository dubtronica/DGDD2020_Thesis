using System.Collections;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Placements : MonoBehaviour
{
	//tiles stuff
	private static int tiles = 9;
	public Image[] allyTile = new Image[tiles];
	public Image[] enemyTile = new Image[tiles];
	
	//charater stuff
	private static int numberOfCharacters = 3;
	public Character[] characters = new Character[numberOfCharacters];
	public CharacterList characterList;
	public int tilesTaken = 0;
	public static int maxTilesTaken = 5;
	
	public GameObject[] SpawnedCharacters = new GameObject[numberOfCharacters];
	public Character[] charactersInArena = new Character[tiles + 1];
	public Image[] charactersInArenaPics = new Image[tiles + 1];
	
	public Character selectedCharacter;
	public Image selectedCharacterFull;
	
	public Button finished;
	public Text currNumOfChars, maxNumofChars;
	
    // Start is called before the first frame update
    void Start()
    {
        
		for(int i = 0; i < numberOfCharacters; i++){
			SpawnedCharacters[i] = characterList.SpawnedCharacters[i];
		}

		finished.onClick.AddListener(Finished);

    }
	
    // Update is called once per frame
    void Update()
    {
		
		currNumOfChars.text = tilesTaken.ToString();
		maxNumofChars.text = maxTilesTaken.ToString();
		
        if(tilesTaken < maxTilesTaken){
			
			
			
		}
    }
	
	public void Finished(){
		//change scene here
		string save = "";
		Character chara;
		
		for(int i = 0; i < tiles; i++){
			chara = charactersInArena[i];
			
			if(chara != null){
				save = save + charactersInArena[i].name + "," + i + ";";
			}
			
		}
		string file = "Assets/SaveFile.txt";
		//File.WriteAllText(@"C:\Users\Nicole\DGDD Thesis\Assets\SaveFile.txt", save);
		File.WriteAllText(file, save);
		SceneManager.LoadScene(0);
	}
	
	public Image getCharacter(int tileNum){
		return charactersInArenaPics[tileNum];
	}
	
	public void setCharacter(int tileNum, Character character, Image pic){
		charactersInArenaPics[tileNum] = pic;
		charactersInArena[tileNum] = character;
	}
	
}
