using System.Collections;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
	public AudioSource music;
	public GameObject gm;
	public Button start, story, characters, summon, inventory, back;
	public Text currLife, currency1, currency2;
	
	static int stageCount = 2;
	public Button[] stages = new Button[stageCount];
	
	bool play = true;
	bool loop = true;
	
    // Start is called before the first frame update
    void Start()
    {
		try{
			if(start!=null){
				start.onClick.AddListener(Homescreen);
			}
			if(story!=null){
				story.onClick.AddListener(Story);
			}
			if(characters!=null){
				characters.onClick.AddListener(Characters);
			}
			if(summon!=null){
				summon.onClick.AddListener(Summon);
			}
			if(inventory!=null){
				inventory.onClick.AddListener(Inventory);
			}
			if(back!=null){
				back.onClick.AddListener(Homescreen);
			}
			
			for(int i = 0; i < stageCount; i++){
				stages[i].onClick.AddListener(PreBattleScene);
			}
			
		}catch(NullReferenceException exception){}
		
		music.Play();
		
    }

    // Update is called once per frame
    void Update()
    {
        //if(play == true && loop == true){
			//music.Play();
			
		//}
    }
	
	public void Homescreen(){
		SceneManager.LoadScene(7);
	}
	public void Story(){
		SceneManager.LoadScene(6);
	}
	public void Characters(){
		SceneManager.LoadScene(2);
	}
	public void Summon(){
		SceneManager.LoadScene(3);
	}
	public void Inventory(){
		SceneManager.LoadScene(0);
	}
	public void PreBattleScene(){
		SceneManager.LoadScene(8);
	}
}
