using System.Collections;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreBattleSetup : MonoBehaviour
{
	private static int numberOfCharacters = 3;
	public Button change, fight, otc1, otc2;
	public Character[] listOfCharacters = new Character[numberOfCharacters];
	
	public Image[] characterPics = new Image[5];
	public Character[] charactersOnTeam = new Character[5];
	
	string[] charactersInfo;
	string[] charAndLocation;
	
    // Start is called before the first frame update
    void Start()
    {
        change.onClick.AddListener(Change);
		fight.onClick.AddListener(Fight);
		
		string file = "Assets/SaveFile.txt";
		
		string line;
        StreamReader r = new StreamReader(file);

        using (r)
        {
            do
            {
                line = r.ReadLine();
                if (line != null)
                {
                    charactersInfo = line.Split(';');
                    
                }
            }
            while (line != null);
            r.Close();
        }
		
		for(int i = 0; i < 5; i++){
			
			try{
				Debug.Log(charactersInfo[i]);
				if(charactersInfo[i] != null){
				
					charAndLocation = charactersInfo[i].Split(',');
					
					for(int j = 0; j < numberOfCharacters; j++){
					
						if(charAndLocation[0] == listOfCharacters[j].name){
							charactersOnTeam[i] = listOfCharacters[j];
							characterPics[i].sprite = charactersOnTeam[i].characterHeadshot;
						}
						
					}
				}
			}catch(IndexOutOfRangeException exception){}

		}
		
		otc1.onClick.AddListener(AddOTC);
		otc2.onClick.AddListener(AddOTC);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void Change(){
		SceneManager.LoadScene(1);
	}
	
	public void Fight(){
		//change to battle scene
		Debug.Log("move to battle map");
	}
	
	public void AddOTC(){
		
		
		
		//this.GetComponent<Image>().sprite = ;
	}
}
