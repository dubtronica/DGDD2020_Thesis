using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacterOnTile : MonoBehaviour
{
	
	public Sprite unselectedTile;
	public Sprite selectedTile;
	public GameObject character;
	public Collider2D boxCollider;
	public Sprite levi;
	
	private SpriteRenderer tile;
	
    // Start is called before the first frame update
    void Start()
    {
        tile = GetComponent<SpriteRenderer>(); 
		if (tile.sprite == null){ 
			tile.sprite = unselectedTile;
		}
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.touchCount > 0){
			
			Vector3 arena = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 place = new Vector2(arena.x, arena.y);
			
			if (boxCollider == Physics2D.OverlapPoint(place)){
				
				if (tile.sprite == unselectedTile){
					tile.sprite = selectedTile;
				}
				else{
					tile.sprite = unselectedTile;
				}
			}
		}
		
    }
}
