using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NarrativeSceneManager : MonoBehaviour
{
    public static List<string> getDialogues(string path)
    {
        List<string> dialogs = new List<string>();
        using (StreamReader sr = new StreamReader(path))
        {
            while (!sr.EndOfStream)
                dialogs.Add(sr.ReadLine());

            sr.Close();
        }


        return dialogs;

    }

    public NarrativeCharacter Spindellia, Jassaninn, DrMichael, DrArchie, DrEdward, DrYvette;

    void disableAllCharacters()
    {
        DrMichael.disable();
        DrYvette.disable();
        DrArchie.disable();
        DrEdward.disable();
    }

    // Start is called before the first frame update

    BGFGCineController controls;
    void Start()
    {
        Spindellia = NarrativeCharacterManager.instance.getCharacter("Spindella", characterEnabledOnStart: false);
        Jassaninn = NarrativeCharacterManager.instance.getCharacter("Jassaninn", characterEnabledOnStart: false);

        DrArchie = NarrativeCharacterManager.instance.getCharacter("DrArchie", characterEnabledOnStart: false);
        DrEdward = NarrativeCharacterManager.instance.getCharacter("DrEdward", characterEnabledOnStart: false);
        DrMichael = NarrativeCharacterManager.instance.getCharacter("DrMichael", characterEnabledOnStart: false);
        DrYvette = NarrativeCharacterManager.instance.getCharacter("DrYvette", characterEnabledOnStart: false);


        NarrativeDialogue.instance.speechPanel.SetActive(false);
        NarrativeDialogue.instance.dboxpanel.SetActive(false);
        try
        {
            controls = BGFGCineController.instance;
            Debug.Log("BG controls initiated!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("BG controls failed to initiate.");
        }
    }

    int i = 0;

    public Texture t1;

    public int cutsceneChoice;

    public void playCutscene(int num)
    {
        List<string> quotes = getDialogues("Assets\\Resources\\Cutscene\\Cutscene_" + num + ".txt");
        if (Input.GetMouseButtonDown(0))
        {
            if (i < quotes.Count)
            {

                string[] dialog = quotes[i].Split(':');

                if (dialog[0] == "DrMichael")
                {
                    NarrativeDialogue.instance.CloseDBDialogue();
                    DrYvette.disable();
                    DrMichael.Say(dialog[1], bool.Parse(dialog[2]));
                }
                else if (dialog[0] == "DrEdward")
                {
                    NarrativeDialogue.instance.CloseDBDialogue();
                    DrEdward.Say(dialog[1], bool.Parse(dialog[2]));
                }
                else if (dialog[0] == "DrArchie")
                {
                    NarrativeDialogue.instance.CloseDBDialogue();
                    DrArchie.Say(dialog[1], bool.Parse(dialog[2]));
                }
                else if (dialog[0] == "DrYvette")
                {
                    NarrativeDialogue.instance.CloseDBDialogue();
                    DrMichael.disable();
                    DrYvette.Say(dialog[1], bool.Parse(dialog[2]));
                }
                else if (dialog[0] == "DBox")
                {
                    NarrativeDialogue.instance.DBdisplay(dialog[1]);
                    disableAllCharacters();
                    NarrativeDialogue.instance.CloseDialogue();
                }

                if (dialog[0] == "...")
                {
                    disableAllCharacters();
                    NarrativeDialogue.instance.CloseDialogue();
                    NarrativeDialogue.instance.CloseDBDialogue();

                    NarrativeDialogue.instance.isWaitingForUser = true;
                }
            }
            else
            {
                disableAllCharacters();
                NarrativeDialogue.instance.CloseDialogue();
                NarrativeDialogue.instance.CloseDBDialogue();
            }

            i++;

        }
    }



    // Update is called once per frame
    void Update()
    {
        DrMichael.setPos(new Vector2(0, 0));
        DrEdward.setPos(new Vector2(1, 0));
        DrArchie.setPos(new Vector2(0.5f, 0));
        DrYvette.setPos(new Vector2(0, 0));

        BGFGCineController.Layer bground = controls.background;

        bground.setTexture(t1);

        playCutscene(cutsceneChoice);

    }
}
