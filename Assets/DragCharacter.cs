using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DragCharacter : MonoBehaviour
{
	
	public Placements placement;
	
	public Image picture;
	
    private Vector3 start, end, offset, picLocation, move;
	public bool place = false;
	public bool collide = false;
	public int num = 0;
	public Image box;
	public Vector3 originalPos;
	
	public bool returned = false;
	
	void Start(){
		originalPos = gameObject.transform.position;
	}
		
	void OnMouseDown(){
		
		start = gameObject.transform.position;
		end = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		offset = start - end;
	}
	
	void Update(){
		
		if(Input.GetMouseButton(0) && place == false){
			picLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
			move = picLocation + offset;
			picture.transform.position = move;
		}
		if(Input.GetMouseButtonUp(0)){
			picture.transform.position = originalPos;
			returned = true;
		}
		
	}
	
}
