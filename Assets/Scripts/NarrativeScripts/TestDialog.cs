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

    public string[] s = new string[]
    {
        "from the speechbox!:Halbard",
        "from the Dialog box!"
    };
    // Update is called once per frame

    int index = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index < s.Length)
            { 
               if(index == 0)
               {
                    if (!nd.isSpeaking || nd.isWaitingForUser)
                    {
                        Say(s[index]);
                    }
               }

               if(index == 1)
               {
                    if (!nd.isDisplaying || nd.isWaitingForUser)
                    {
                        Display(s[index]);
                    }
               }
                
            }

            index++;

        }
       
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

    void Display(string s)
    {
        nd.DBdisplay(s);
    }
}
