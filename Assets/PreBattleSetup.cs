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
	public Button change, fight, otc1, otc2, back;
	public Text otc1name, otc2name;
	public Button otcBeingChanged;
	public Character[] listOfCharacters = new Character[numberOfCharacters];
	public Character[] listOfOTCs = new Character[2];
	
	public Image[] characterPics = new Image[5];
	public Character[] charactersOnTeam = new Character[5];
	public Text[] characterNames = new Text[5];
	
	public Image[] enemyPics = new Image[3];
	public Character[] enemyTypes = new Character[3];
	
	public Image clipboard, OTCSpawnBox, OTCBox, OTCPic, scrollView, viewport;
	public Button equip, cancel;
	public Text OTCname, OTCdetails, equipText;
	
	public OTCList otcList;
	
	string[] charactersInfo = new string[5];
	string[] charAndLocation;
	string[] otcInfo;
	
	public Button boxpic;
	public Image box;
	
    // Start is called before the first frame update
    void Start()
    {
        change.onClick.AddListener(Change);
		fight.onClick.AddListener(Fight);
		equip.onClick.AddListener(Equip);
		cancel.onClick.AddListener(Cancel);
		back.onClick.AddListener(Back);
		
		boxpic.GetComponent<Image>().enabled = false;
		box.enabled = false;
		
		string line = "";
		StreamReader r;
		
		try{
			string file = "Assets/Resources/SaveFile.txt";
			r = new StreamReader(file);
			int n = 0;
			
			using (r)
			{
				
				while (line != null){
					
					line = r.ReadLine();
	
					if (string.Equals(line,"NA") == false && line != null)
					{
						//charactersInfo = line.Split(';');
						charactersInfo[n] = line;
						n++;
						
					}
				}
				
				r.Close();
			}
			
			for(int i = 0; i < 5; i++){
				
				try{
					
					if(string.Equals(charactersInfo[i],"NA") == false && charactersInfo[i] != null){
					
						//charAndLocation = charactersInfo[i].Split(',');
						
						//for(int j = 0; j < numberOfCharacters; j++){
						
							/*if(charAndLocation[0] == listOfCharacters[j].name){
								charactersOnTeam[i] = listOfCharacters[j];
								characterPics[i].sprite = charactersOnTeam[i].characterHeadshot;
								characterNames[i].text = charactersOnTeam[i].name;
							}*/
							
							int m = Int32.Parse(charactersInfo[i]);
							
							//if(m == j){
								charactersOnTeam[i] = listOfCharacters[m];
								characterPics[i].sprite = charactersOnTeam[i].characterHeadshot;
								characterNames[i].text = charactersOnTeam[i].name;
							//}
							
						//}
					}
				}catch(IndexOutOfRangeException exception){}

			}
		}catch(FileNotFoundException exception){
			
		}
		
		for(int i = 0; i < 3; i++){
			enemyPics[i].sprite = enemyTypes[i].characterHeadshot;
		}
		
		string otcFile = "Assets/OTCFile.txt";
		
		try{
			StreamReader sr = new StreamReader(otcFile);
		

			using (sr)
			{
				do
				{
					line = sr.ReadLine();
					if (line != null)
					{
						otcInfo = line.Split(';');
						
					}
				}
				while (line != null);
				sr.Close();
			}
			for(int i = 0; i < 2; i++){
				
				try{
					Debug.Log(otcInfo[i]);
					if(otcInfo[i] != null){
						
						for(int j = 0; j < listOfOTCs.Length; j++){
							if(otcInfo[i] == listOfOTCs[j].name){
								if(i == 0){
									otc1.GetComponent<Image>().sprite = listOfOTCs[j].characterHeadshot;
									otc1name.text = listOfOTCs[j].name;
								}
								if(i == 1){
									otc2.GetComponent<Image>().sprite = listOfOTCs[j].characterHeadshot;
									otc2name.text = listOfOTCs[j].name;
								}
							}
						}
					}
				}catch(NullReferenceException exception){}

			}
		}catch(FileNotFoundException exception){
			
		}
		
		
		otc1.onClick.AddListener(delegate{AddOTC(otc1);});
		otc2.onClick.AddListener(delegate{AddOTC(otc2);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void Change(){
		SceneManager.LoadScene("Team Placement");
	}
	
	public void Fight(){
		//change to battle scene
		Debug.Log("move to battle map");
		SceneManager.LoadScene("Battle");
	}
	
	public void AddOTC(Button otc){
		
		otcBeingChanged = otc;
		
		clipboard.enabled = true;
		OTCSpawnBox.enabled = true;
		OTCBox.enabled = true;
		OTCPic.enabled = true;
		scrollView.enabled = true;
		equip.GetComponent<Image>().enabled = true;
		OTCname.enabled = true;
		OTCdetails.enabled = true;
		equipText.enabled = true;
		viewport.enabled = true;
		cancel.GetComponent<Image>().enabled = true;

		
		otcList.Spawn();
		
		//this.GetComponent<Image>().sprite = ;
	}
	
	public void Equip(){
		
		GameObject otcChar = new GameObject();
		
		for(int i = 0; i < 2; i++){
			if(otcList.SpawnedCharacters[i].GetComponent<CharacterDetails>().pic.sprite == OTCPic.sprite){
				otcChar = otcList.SpawnedCharacters[i];
			}
		}
		

		if(otcChar.GetComponent<CharacterDetails>().alreadyPlaced == false){
			
			clipboard.enabled = false;
			OTCSpawnBox.enabled = false;
			OTCBox.enabled = false;
			OTCPic.enabled = false;
			scrollView.enabled = false;
			equip.GetComponent<Image>().enabled = false;
			OTCname.enabled = false;
			OTCdetails.enabled = false;
			equipText.enabled = false;
			viewport.enabled = false;
			cancel.GetComponent<Image>().enabled = false;
			
			if(otcBeingChanged == otc1){
				otc1name.text = otcChar.GetComponent<CharacterDetails>().characterName.text;
			}
			if(otcBeingChanged == otc2){
				otc2name.text = otcChar.GetComponent<CharacterDetails>().characterName.text;
			}
			
			
			
			for(int i = 0; i < 2; i++){
				Destroy(otcList.SpawnedCharacters[i]);
			}
			otcBeingChanged.GetComponent<Image>().sprite = OTCPic.sprite;
			
			string file = "Assets/OTCFile.txt";
		
			string save = "";
			
			if(otc1name != null){
				save = save + otc1name.text;
			}
			if(otc2name != null){
				save = save + ";" + otc2name.text;
			}

			File.WriteAllText(file, save);
		}
		else{
			Debug.Log("Character already chosen, please pick another.");
		}

	}
	
	public void Cancel(){
		
		clipboard.enabled = false;
		OTCSpawnBox.enabled = false;
		OTCBox.enabled = false;
		OTCPic.enabled = false;
		scrollView.enabled = false;
		equip.GetComponent<Image>().enabled = false;
		OTCname.enabled = false;
		OTCdetails.enabled = false;
		equipText.enabled = false;
		viewport.enabled = false;
		cancel.GetComponent<Image>().enabled = false;
		
		for(int i = 0; i < 2; i++){
			Destroy(otcList.SpawnedCharacters[i]);
		}
		
	}
	
	public void Back(){
		SceneManager.LoadScene("Choose Story");
	}
}
