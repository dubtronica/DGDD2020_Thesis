using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharManager : MonoBehaviour
{
    public NarrativeCharacter Spindellia, Jassaninn;

    // Start is called before the first frame update
    void Start()
    {
        Spindellia = NarrativeCharacterManager.instance.getCharacter("Spindella", characterEnabledOnStart: false);
        Jassaninn = NarrativeCharacterManager.instance.getCharacter("Jassaninn", characterEnabledOnStart: true);

    }

    public string[] quotes;
    int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(i < quotes.Length)
            {
                Spindellia.Say(quotes[i]);
                Jassaninn.enabled = false;
            }
            else
            {
                NarrativeDialogue.instance.CloseDialogue();
                Spindellia.enabled = false;
                Jassaninn.enabled = true;
            }
            i++;
        }
            
    }
}
