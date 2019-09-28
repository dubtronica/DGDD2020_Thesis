using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialog : MonoBehaviour
{
    NarrativeDialogue nd;

    // Start is called before the first frame update
    void Start()
    {
        nd = NarrativeDialogue.instance;   
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Say(string s)
    {
        string[] parts = s.Split(':');
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";

        nd.Say(speech, speaker);
    }

    void SayAdd(string s)
    {
        string[] parts = s.Split(':');
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";

        nd.SayAdd(speech, speaker);
    }
}
