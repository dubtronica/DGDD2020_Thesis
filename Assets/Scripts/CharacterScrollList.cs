using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScrollList : MonoBehaviour
{
	private DataController dataController;
	public List<CharacterData> characterList;
	public Transform contentPanel;
	public SimpleObjectPool buttonObjectPool;
	
    // Start is called before the first frame update
    void Start()
    {
		dataController = FindObjectOfType<DataController>();
		characterList = dataController.GetPlayerCharacterData();
        RefreshDisplay();
    }
	
	private void RefreshDisplay()
	{
		RemoveButtons();
		AddButtons();
	}
	
	private void AddButtons()
	{
		for (int i = 0; i < characterList.Count; i++)
		{
			CharacterData chara = characterList[i];
			GameObject newButton = buttonObjectPool.GetObject();
			newButton.transform.SetParent(contentPanel, false);
			
			SampleCharacterButton sampleButton = newButton.GetComponent<SampleCharacterButton>();
			sampleButton.Setup(chara);
		}
	}
	
	// DOESNT WORK
	public void RemoveButtons()
	{
		while (contentPanel.childCount > 0)
		{
			GameObject toRemove = transform.GetChild(0).gameObject;
			buttonObjectPool.ReturnObject(toRemove);
		}
	}
}
