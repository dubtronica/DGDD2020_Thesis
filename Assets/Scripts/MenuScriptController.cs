using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScriptController : MonoBehaviour
{
    public void ViewCharacterList()
	{
		SceneManager.LoadScene("CharacterList");
	}
}
