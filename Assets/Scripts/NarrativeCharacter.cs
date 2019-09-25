using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NarrativeCharacter 
{
    public string charname;
    [HideInInspector] public RectTransform root;
    NarrativeDialogue nd;

    public bool isMultiLayerCharacter { get { return renderers.singlayer == null; } }
    public bool enabled { get { return root.gameObject.activeInHierarchy; } set { root.gameObject.SetActive(value); } }

    public void Say(string s, bool add = false)
    {
        if (!enabled)
            enabled = true;

        if(!add)
            nd.Say(s, charname);
        else
            nd.SayAdd(s, charname);
    }

    public NarrativeCharacter(string n, bool enableOnStart = true)
    {
        NarrativeCharacterManager ncm = NarrativeCharacterManager.instance;
        GameObject ncprefab = Resources.Load("NarrativeDummyCharacters/Char[" + n + "]") as GameObject;
        GameObject gob = GameObject.Instantiate(ncprefab, ncm.charPanel);

        root = gob.GetComponent<RectTransform>();
        charname = n;

        renderers.singlayer = gob.GetComponentInChildren<RawImage>();

        if (isMultiLayerCharacter)
        {
            renderers.body = gob.transform.Find("BodyLayer").GetComponent<Image>();
            renderers.expression = gob.transform.Find("ExpressionLayer").GetComponent<Image>();
        }

        nd = NarrativeDialogue.instance;
        enabled = enableOnStart;
    }

    [System.Serializable]
    public class Renderers //image rendering
    {
        public RawImage singlayer;

        public Image body;
        public Image expression;
    }

    public Renderers renderers = new Renderers();
}
