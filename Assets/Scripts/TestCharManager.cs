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
        Jassaninn = NarrativeCharacterManager.instance.getCharacter("Jassaninn", characterEnabledOnStart: false);
        NarrativeDialogue.instance.speechPanel.SetActive(false);

    }

    public string[] quotes;
    int i = 0;

    public Vector2 targetPos;
    public float speed;

    public int jassabodyindex;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(i < quotes.Length)
            {
                Jassaninn.enabled = true;
                Spindellia.Say(quotes[i]);
                Jassaninn.setPos(new Vector2(0, 0));

            }
            else
            {
                NarrativeDialogue.instance.CloseDialogue();
                Spindellia.MoveTo(targetPos, speed, false);
                Jassaninn.MoveTo(new Vector2(-2, 0), speed, false);
            }
            i++;
        }

        if(Input.GetMouseButtonDown(1))
        {
            Jassaninn.TransitionBody(Jassaninn.GetBodySprite(jassabodyindex), 5f, true);
            Jassaninn.TransitionExpression(Jassaninn.GetExpressionSprite(jassabodyindex), 5f, true);
        }
            



    }
}
