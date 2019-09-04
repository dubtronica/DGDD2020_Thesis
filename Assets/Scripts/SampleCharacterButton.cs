using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleCharacterButton : MonoBehaviour
{
	public Button button;
	public Text nameLabel;
	public Text typeLabel;
	private CharacterData character;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void Setup(CharacterData currentCharacter)
	{
		character = currentCharacter;
		nameLabel.text = character.name;
		typeLabel.text = character.type;
	}
}
