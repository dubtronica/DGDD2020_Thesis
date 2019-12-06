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
	public Character[] charactersInArena = new Character[tiles];
	public Image[] charactersInArenaPics = new Image[tiles];
	public Image fullBodyPicTemp;
	
	public Character selectedCharacter;
	public Image selectedCharacterFull;
	
	public Button finished;
	public Text currNumOfChars, maxNumofChars;
	
	string[] charactersIndex = new string[9];
	string line = "";
	StreamReader r;
	
    // Start is called before the first frame update
    void Start()
    {
        
		for(int i = 0; i < numberOfCharacters; i++){
			SpawnedCharacters[i] = characterList.SpawnedCharacters[i];
		}

		finished.onClick.AddListener(Finished);
		
		try{
			string file = "Assets/Resources/SaveFile.txt";
			r = new StreamReader(file);
			int n = 0;
			
			using (r)
			{
				
				while (line != null && n < 9){			
					line = r.ReadLine();
					charactersIndex[n] = line;
					Debug.Log(n);
					n++;
				}
				
				r.Close();
			}
			
			for(int i = 0; i < 9; i++){
				
				try{
					
					if(string.Equals(charactersIndex[i],"NA") == false && charactersIndex[i] != null){
					
						int m = Int32.Parse(charactersIndex[i]);
						
						charactersInArena[i] = characters[m];
						Debug.Log(i + " " + charactersInArena[i].name + " " + characters[m].characterFullBody);
						charactersInArenaPics[i] = Instantiate(fullBodyPicTemp, GameObject.FindGameObjectWithTag("Canvas").transform) as Image;
						charactersInArenaPics[i].sprite = characters[m].characterFullBody;
						
						
						switch(i){
							case 0:
								charactersInArenaPics[i].transform.position = new Vector3((float)320, (float)340,(float)0.0);
								break;
							case 1:
								charactersInArenaPics[i].transform.position = new Vector3((float)390, (float)340,(float)0.0);
								break;
							case 2:
								charactersInArenaPics[i].transform.position = new Vector3((float)460, (float)340,(float)0.0);
								break;
							case 3:
								charactersInArenaPics[i].transform.position = new Vector3((float)285, (float)270,(float)0.0);
								break;
							case 4:
								charactersInArenaPics[i].transform.position = new Vector3((float)360, (float)270,(float)0.0);
								break;
							case 5:
								charactersInArenaPics[i].transform.position = new Vector3((float)420, (float)270,(float)0.0);
								break;
							case 6:
								charactersInArenaPics[i].transform.position = new Vector3((float)250, (float)200,(float)0.0);
								break;
							case 7:
								charactersInArenaPics[i].transform.position = new Vector3((float)330, (float)200,(float)0.0);
								break;
							case 8:
								charactersInArenaPics[i].transform.position = new Vector3((float)390, (float)200,(float)0.0);
								break;
						}
						SpawnedCharacters[m].GetComponent<CharacterDetails>().alreadyPlaced = true;
						SpawnedCharacters[m].GetComponent<CharacterDetails>().tileNum = i;
						SpawnedCharacters[m].GetComponent<CharacterDetails>().charOnTile = charactersInArenaPics[i];
						tilesTaken++;
					}
				}catch(IndexOutOfRangeException exception){}
			}
			//Debug.Log(currCharCount);
		}catch(FileNotFoundException exception){
			
		}
    }
	
    // Update is called once per frame
    void Update()
    {

		currNumOfChars.text = tilesTaken.ToString();
		maxNumofChars.text = maxTilesTaken.ToString();
		
    }
	
	public void Finished(){
		//change scene here
		string save = "";
		Character chara;
		
		for(int i = 0; i < tiles; i++){
			chara = charactersInArena[i];
			
			if(chara != null){
				
				for(int k = 0; k < numberOfCharacters; k++){
					
					Debug.Log(characters[k].name + " " + charactersInArena[i].name);
				
					if(string.Equals(characters[k].name,charactersInArena[i].name)){
						Debug.Log(k);
						save = save + k + Environment.NewLine;
					}
					
				}
				
			}
			else{
				save = save + "NA" + Environment.NewLine;
			}
			
		}
		string file = "Assets/Resources/SaveFile.txt";
		//File.WriteAllText(@"C:\Users\Nicole\DGDD Thesis\Assets\SaveFile.txt", save);
		File.WriteAllText(file, save);
		SceneManager.LoadScene("Pre-Battle Scene");
	}
	
	
	public Image getCharacter(int tileNum){
		return charactersInArenaPics[tileNum];
	}
	
	public void setCharacter(int tileNum, Character character, Image pic){
		charactersInArenaPics[tileNum] = pic;
		charactersInArena[tileNum] = character;
	}
	
}
