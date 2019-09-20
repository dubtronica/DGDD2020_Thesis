using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceSprite : MonoBehaviour
{
    private Vector3 start, end, offset, pic, move;
	public Sprite highlighted, unselected;
	public bool place = false;
	public bool collide = false;
	public int num = 0;
	public Image box;
	public Vector3 originalPos;
		
	void OnMouseDown(){
		
		start = gameObject.transform.position;
		end = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		offset = start - end;
		if(originalPos == null){
			originalPos = start;
		}
	}
	
	void Update(){
		
		if(Input.GetMouseButtonUp(0) && num == 0 && collide == true){
			//Debug.Log("click");
			Instantiate(gameObject,gameObject.transform.position,gameObject.transform.rotation);
			place = true;
			num++;
		}
		if(Input.GetMouseButton(0) && place == false){
			pic = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
			move = pic + offset;
			transform.position = move;
		}
		

	}
	
	void OnTriggerEnter2D(Collider2D location)
    {
		box = location.gameObject.GetComponent<Image>();
		box.sprite = highlighted;
		collide = true;
		
	}
	
	void OnTriggerExit2D(Collider2D location){
		box = location.gameObject.GetComponent<Image>();
		box.sprite = unselected;
	}

}
