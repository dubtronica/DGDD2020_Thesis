using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestCharManager : MonoBehaviour
{
    public static List<string> getDialogues(string path)
    {
        List<string> dialogs = new List<string>();
        using(StreamReader sr = new StreamReader(path))
        {
            while(!sr.EndOfStream)
                dialogs.Add(sr.ReadLine());

            sr.Close();
        }


        return dialogs;
        
    }

    public NarrativeCharacter Spindellia, Jassaninn, DrMichael, DrArchie, DrEdward;

    // Start is called before the first frame update

    BGFGCineController controls;
    void Start()
    {
        Spindellia = NarrativeCharacterManager.instance.getCharacter("Spindella", characterEnabledOnStart: false);
        Jassaninn = NarrativeCharacterManager.instance.getCharacter("Jassaninn", characterEnabledOnStart: false);

        DrArchie = NarrativeCharacterManager.instance.getCharacter("DrArchie", characterEnabledOnStart: false);
        DrEdward = NarrativeCharacterManager.instance.getCharacter("DrEdward", characterEnabledOnStart: false);
        DrMichael = NarrativeCharacterManager.instance.getCharacter("DrMichael", characterEnabledOnStart: false);


        NarrativeDialogue.instance.speechPanel.SetActive(false);
        try
        {
            controls = BGFGCineController.instance;
            Debug.Log("BG controls initiated!");
        } catch (System.Exception e)
        {
            Debug.LogError("BG controls failed to initiate.");
        }
        

    }

   

    int i = 0;

    public Vector2 targetPos;
    public float speed;

    public Texture t1;

    public int jassabodyindex;

    List<string> quotes = getDialogues("Assets\\Resources\\op1.txt");

 

    // Update is called once per frame
    void Update()
    {
        DrMichael.setPos(new Vector2(0, 0));
        DrEdward.setPos(new Vector2(1, 0));
        DrArchie.setPos(new Vector2(0.5f, 0));

        BGFGCineController.Layer bground = controls.background;

        bground.setTexture(t1);

        if (Input.GetMouseButtonDown(0))
        {
            if (i < quotes.Count)
            {
                
                string[] dialog = quotes[i].Split(':');

                if (dialog[0] == "DrMichael")
                {
                    DrMichael.Say(dialog[1], bool.Parse(dialog[2]));
                }
                else if (dialog[0] == "DrEdward")
                {
                    DrEdward.Say(dialog[1], bool.Parse(dialog[2]));
                }
                else if (dialog[0] == "DrArchie")
                {
                    DrArchie.Say(dialog[1], bool.Parse(dialog[2]));
                }

                /*
                if (dialog[1].Contains("snow"))
                {
                    bground.setTexture(t1);
                } else if (dialog[1].Contains("STARS"))
                {
                    bground.setTexture(t2);
                }
                
            
                if (i > 1)
                {
                    Jassaninn.TransitionExpression(Jassaninn.GetExpressionSprite(1), 5f, true);
                }

                if (i > 2)
                {
                    Jassaninn.TransitionBody(Jassaninn.GetBodySprite(1), 5f, true);
                }

                if(i > 4)
                {
                    Spindellia.TransitionExpression(Spindellia.GetExpressionSprite(1), 5f, true);
                }
                if(i > 7)
                {
                    Spindellia.enabled = false;
                }*/

            }
            else
            {
                DrEdward.disable();
                DrArchie.disable();
                DrMichael.disable();
                NarrativeDialogue.instance.CloseDialogue();
            }

            i++;
            
        }
    }
}
