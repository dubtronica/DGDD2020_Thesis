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
            while(sr.Peek() >= 0)
                dialogs.Add(sr.ReadLine());

        }

        return dialogs;
        
    }

    public NarrativeCharacter Spindellia, Jassaninn;

    // Start is called before the first frame update
    void Start()
    {
        Spindellia = NarrativeCharacterManager.instance.getCharacter("Spindella", characterEnabledOnStart: false);
        Jassaninn = NarrativeCharacterManager.instance.getCharacter("Jassaninn", characterEnabledOnStart: false);
        NarrativeDialogue.instance.speechPanel.SetActive(false);

    }

   

    int i = 0;

    public Vector2 targetPos;
    public float speed;

    public int jassabodyindex;

    public List<string> quotes = getDialogues("Resources\\sampledialogue.txt");


    // Update is called once per frame
    void Update()
    {
        Jassaninn.setPos(new Vector2(0, 0));
        Spindellia.setPos(new Vector2(1, 0));

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
            }
            else
            {
                NarrativeDialogue.instance.CloseDialogue();
                Jassaninn.enabled = false;
                Spindellia.enabled = false;
            }

            i++;
        }
    }
}
