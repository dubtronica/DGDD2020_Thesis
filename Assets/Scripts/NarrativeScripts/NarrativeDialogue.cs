using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeDialogue : MonoBehaviour
{
    public static NarrativeDialogue instance;
    public DialogueElements elems;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Say(string speech, string speaker = "") // controls how characters speak
    {
        silence();
        speaking = StartCoroutine(Speaking(speech, false, speaker));
    }

    public void SayAdd(string speech, string speaker = "") // controls how characters speak
    {
        silence();
        speechText.text = targetSpeech;
        speaking = StartCoroutine(Speaking(speech, true, speaker));
    }

    //to stop the character from speaking
    public void silence()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }

    public bool isSpeaking { get { return speaking != null; } } //bool check for speaking characters
    [HideInInspector] public bool isWaitingForUser = false;

    public string targetSpeech = "";
    Coroutine speaking = null;
    IEnumerator Speaking(string speech, bool additive, string speaker = "")
    {
        speechPanel.SetActive(true);
        targetSpeech = speech;

        if (!additive)
            speechText.text = "";
        else
            targetSpeech = speechText.text + targetSpeech;
        charactername.text = DetermineSpeaker(speaker);
        isWaitingForUser = false;

        while(speechText.text != targetSpeech)
        {
            speechText.text += targetSpeech[speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        isWaitingForUser = true;
        while (isWaitingForUser)
            yield return new WaitForEndOfFrame();

        silence();
    }

    string DetermineSpeaker(string s)
    {
        string ret = charactername.text;
        if (s != charactername.text && s != "")
            ret = (s.ToLower().Contains("narrator")) ? "" : s;

        return ret;
    }

    public void CloseDialogue()
    {
        silence();
        speechPanel.SetActive(false);
    }

    public void DBdisplay(string speech)
    {
        DBsilence();
        displaying = StartCoroutine(Displaying(speech));
    }

    public void DBsilence()
    {
        if (isDisplaying)
        {
            StopCoroutine(displaying);
        }
        displaying = null;
    }

    public bool isDisplaying { get { return displaying != null; } } //bool check for speaking characters
    Coroutine displaying = null;

    IEnumerator Displaying(string speech)
    {
        dboxpanel.SetActive(true);

        isWaitingForUser = false;
        dboxtext.text = speech;

        isWaitingForUser = true;
        while (isWaitingForUser)
            yield return new WaitForEndOfFrame();

        silence();
    }

    public void CloseDBDialogue()
    {
        DBsilence();
        dboxpanel.SetActive(false);
    }

    [System.Serializable]
    public class DialogueElements
    {
        public GameObject speechpanel, dboxpanel;
        public Text charactername, speechText, dboxtext;

    }

    public GameObject speechPanel { get { return elems.speechpanel; } }
    public Text charactername { get { return elems.charactername; } }
    public Text speechText { get { return elems.speechText; } }

    public GameObject dboxpanel { get { return elems.dboxpanel; } }
    public Text dboxtext { get { return elems.dboxtext; } }
}
