using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeCharacterManager : MonoBehaviour
{
    public static NarrativeCharacterManager instance;

    //attaching chars to the panel
    public RectTransform charPanel;

    //for characters in scene
    public List<NarrativeCharacter> activechars = new List<NarrativeCharacter>();

    //Dictionary for all narrative characters
    public Dictionary<string, int> narrativeCharDictionary = new Dictionary<string, int>();

    void Awake()
    {
        instance = this;    
    }

    //find char in dictionary. if is, return char, else create one
    public NarrativeCharacter getCharacter(string name, bool createNonExistentCharacter = true, bool characterEnabledOnStart = true)
    {
        int index = -1;

        if (narrativeCharDictionary.TryGetValue(name, out index))
            return activechars[index];
        else if(createNonExistentCharacter)
            return createCharacter(name, characterEnabledOnStart);

        return null;

    }

    //creates a character
    public NarrativeCharacter createCharacter(string name, bool enableOnStart = true)
    {
        NarrativeCharacter newNC = new NarrativeCharacter(name, enableOnStart);

        narrativeCharDictionary.Add(name, activechars.Count);
        activechars.Add(newNC);

        return newNC;

    }

    public class PredefinedPositions
    {
        Vector2 left = new Vector2(0, 0);
        Vector2 right = new Vector2(0, 1f);
        Vector2 mid = new Vector2(0, 0.5f);
    }

    public static PredefinedPositions predefpos;
}
