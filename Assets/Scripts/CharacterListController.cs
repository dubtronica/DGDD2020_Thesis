using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterListController : MonoBehaviour
{
    public void ReturnToMenuScreen()
	{
		SceneManager.LoadScene(7);
	}
}
