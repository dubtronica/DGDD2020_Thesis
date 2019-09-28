﻿using System.Collections;
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
            string line;
            while(!sr.EndOfStream)
                dialogs.Add(sr.ReadLine());

            sr.Close();
        }


        return dialogs;
        
    }

    public NarrativeCharacter Spindellia, Jassaninn;

    // Start is called before the first frame update

    BGFGCineController controls;
    void Start()
    {
        Spindellia = NarrativeCharacterManager.instance.getCharacter("Spindella", characterEnabledOnStart: false);
        Jassaninn = NarrativeCharacterManager.instance.getCharacter("Jassaninn", characterEnabledOnStart: false);
        NarrativeDialogue.instance.speechPanel.SetActive(false);
        controls = BGFGCineController.instance;

    }

   

    int i = 0;

    public Vector2 targetPos;
    public float speed;

    public Texture t1, t2;

    public int jassabodyindex;

    List<string> quotes = getDialogues("Assets\\Resources\\sampledialogue2.txt");

 

    // Update is called once per frame
    void Update()
    {
        Jassaninn.setPos(new Vector2(0, 0));
        Spindellia.setPos(new Vector2(1, 0));

        BGFGCineController.Layer bground = controls.background;

        if (Input.GetMouseButtonDown(0))
        {
            if (i < quotes.Count)
            {
                string[] dialog = quotes[i].Split(':');

                if (dialog[0] == "Spindella")
                {
                    Spindellia.Say(dialog[1]);
                }
                else if (dialog[0] == "Jassaninn")
                {
                    Jassaninn.Say(dialog[1]);
                }

                if(dialog[1].Contains("snow"))
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
                }
            }
            else
            {
                NarrativeDialogue.instance.CloseDialogue();
                Jassaninn.enabled = false;
            }

            i++;
        }
    }
}