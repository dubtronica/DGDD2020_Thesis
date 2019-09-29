using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxDialog : MonoBehaviour
{
    public static BoxDialog instance = null;
    public BoxElements elems;

    void Awake()
    {
        instance = this;
    }

    public void DBdisplay(string speech)
    {
        silence();
        displaying = StartCoroutine(Displaying(speech));
    }

    public void silence()
    {
        if (isDisplaying)
        {
            StopCoroutine(displaying);
        }
        displaying = null;
    }

    public void CloseDialogue()
    {
        silence();
        dboxpanel.SetActive(false);
    }

    public bool isDisplaying { get { return displaying != null; } } //bool check for speaking characters
    public bool isWaitingForUser = false;

    public string targetSpeech = "";
    Coroutine displaying = null;

    IEnumerator Displaying(string speech)
    {
        dboxpanel.SetActive(true);

        dboxtext.text = speech;

        isWaitingForUser = true;
        while (isWaitingForUser)
            yield return new WaitForEndOfFrame();

        silence();
    }

    [System.Serializable]
    public class BoxElements
    {
        public GameObject dboxpanel;
        public Text dboxtext;
    }

    public GameObject dboxpanel { get { return elems.dboxpanel; } }
    public Text dboxtext { get { return elems.dboxtext; } }
}
