using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterDetails : MonoBehaviour
{
	
	public Button characterPic;
	public Text characterName;
	//public Button characterButton;
	public Image fullChar;
	public Character character;
	public Image characterBox;
	private PlaceSprite placeSprite;
	public Sprite unselected;
	int timesClicked = 0;
	
	Vector3 start, end;
	public Transform fullpic;
	
    // Start is called before the first frame update
    void Start()
    {
		characterBox.enabled = true;
		Image pic = characterPic.GetComponent<Image>();
		pic.enabled = true;
		//characterPic.image.enabled = true;
		placeSprite = fullChar.GetComponent<PlaceSprite>();
        fullChar.enabled = false;
		characterPic.onClick.AddListener(chooseCharacter);
		//rb = fullChar.GetComponent<Rigidbody2D>();
		fullpic = fullChar.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timesClicked % 2 == 0){
			fullChar.enabled = false;
		}
		
		if(placeSprite.place == true && timesClicked % 2 == 0){
			placeSprite.box.sprite = unselected;
			//fullChar.transform.position = placeSprite.originalPos;
			placeSprite.place = false;
			placeSprite.num = 0;
			placeSprite.collide = false;
		}
		
    }
	
	public void chooseCharacter(){
		fullChar.enabled = true;
		timesClicked++;
	}
	
	//so after placing it, make another object on that exact area, that you can stack info on
	//make it so users drag the headshot to the area then only once you collide finger with tile and release, then make the full pic "appear"
	//so that avoid making feet "match" the tile
	//add script to tile upon collision so that if it senses the "object" dragged by the mouse, it "creates" / instantiates an object on it.
}
