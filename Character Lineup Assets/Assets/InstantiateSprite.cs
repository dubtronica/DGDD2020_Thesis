using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateSprite : MonoBehaviour
{
	public int tileNum;
	public bool isTaken = false;
	public Character character; 
	private Image fullPic;
	public BoxCollider2D boxCollider;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D pic)
    {
		character = pic.gameObject.GetComponent<Character>();
		fullPic.sprite = character.characterFullBody;
		
		if(Input.GetMouseButtonUp(0) && isTaken == false){
			
			Instantiate(fullPic,fullPic.transform.position,fullPic.transform.rotation);

		}
		
	}
	
	void OnTriggerExit2D(Collider2D location){

	}
}
