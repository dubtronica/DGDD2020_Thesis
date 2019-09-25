using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScriptController : MonoBehaviour
{
    public void ViewCharacterList()
	{
		SceneManager.LoadScene("CharacterList");
	}
	
	public void GoToGachaScene()
	{
		SceneManager.LoadScene("GachaScreen");
	}

    public void GoToNarrativeCutscene()
    {
        SceneManager.LoadScene("Narrative");
    }
}
