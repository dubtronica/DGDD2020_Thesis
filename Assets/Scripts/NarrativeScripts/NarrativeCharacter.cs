using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NarrativeCharacter 
{
    public string charname;
    public RectTransform root;
    NarrativeDialogue nd;

    public bool isMultiLayerCharacter { get { return renderers.singlayer == null; } }
    public bool enabled { get { return root.gameObject.activeInHierarchy; } set { root.gameObject.SetActive(value); } }
    //padding
    public Vector2 anchorPadding { get { return root.anchorMax - root.anchorMin; } }

    public void disable()
    {
        enabled = false;
    }

    public void Say(string s, bool add = false)
    {
        if (!enabled)
            enabled = true;

        if(!add)
            nd.Say(s, charname);
        else
            nd.SayAdd(s, charname);
    }

    /// <summary>
    /// MOVEMENT OF CHARACTER
    /// </summary>

    Vector2 targetPos;
    Coroutine moving;
    bool isMoving { get { return moving != null; } }
    public void MoveTo(Vector2 targetPos, float speed, bool smooth = true)
    {
        moving = NarrativeCharacterManager.instance.StartCoroutine(Moving(targetPos, speed, smooth));
    }

    public void Stop(bool arriveAtTargetPosImmediately = false) // stop movement
    {
        if (isMoving)
        {
            NarrativeCharacterManager.instance.StopCoroutine(moving);
            if (arriveAtTargetPosImmediately)
                setPos(targetPos);
        }
        moving = null;
    }

    public void setPos(Vector2 pos)
    {
        targetPos = pos;

        Vector2 padding = anchorPadding; //accounts character size

        //% target
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTargetPos = new Vector2(maxX * targetPos.x, maxY * targetPos.y);

        root.anchorMin = minAnchorTargetPos;
        root.anchorMax = root.anchorMin + padding;
    }

    IEnumerator Moving(Vector2 target, float speed, bool smooth)
    {
        targetPos = target;

        Vector2 padding = anchorPadding; //accounts character size

        //% target
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTargetPos = new Vector2(maxX * targetPos.x, maxY * targetPos.y);

        speed *= Time.deltaTime;

        while(root.anchorMin != minAnchorTargetPos)
        {
            root.anchorMin = (!smooth) ? Vector2.MoveTowards(root.anchorMin, minAnchorTargetPos, speed) : Vector2.Lerp(root.anchorMin, minAnchorTargetPos, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }

        Stop();
    }

    ///

    /// <summary>
    /// SPRITE TRANSITIONS (if any)
    /// </summary>
    public Sprite GetBodySprite(int index = 0)
    {
        //Sprite[] Sprites = Resources.LoadAll<Sprite>("NarrativeDummyCharacters/" + charname + "/Body");

        //return Sprites[index];

        Sprite sprite = Resources.Load<Sprite>("NarrativeDummyCharacters/" + charname + "/Body/" + charname.ToLower() + "body_" + index);

        return sprite;
    }

    public Sprite GetExpressionSprite(int index = 0)
    {
        //Sprite[] Sprites = Resources.LoadAll<Sprite>("NarrativeDummyCharacters/" + charname + "/Expressions");

        //return Sprites[index];

        Sprite sprite = Resources.Load<Sprite>("NarrativeDummyCharacters/" + charname + "/Expressions/" + charname.ToLower() + "_" + index);

        return sprite;
    }


    public void setBody(int index)
    {
        renderers.body.sprite = GetBodySprite(index);
    }

    public void setBody(Sprite sprite)
    {
        renderers.body.sprite = sprite;
    }

    public void setExpression(int index)
    {
        renderers.expression.sprite = GetExpressionSprite(index);
    }

    public void setExpression(Sprite sprite)
    {
        renderers.expression.sprite = sprite;
    }

    bool isTransitioningBody { get { return transitionBody != null; } }
    Coroutine transitionBody = null;

    public void TransitionBody(Sprite sprite, float speed, bool smooth)
    {
        if (renderers.body.sprite == sprite)
            return;

        StopTransitionBody();
        transitionBody = NarrativeCharacterManager.instance.StartCoroutine(transBody(sprite, speed, smooth));
    }

    public void StopTransitionBody()
    {
        if (isTransitioningBody)
            NarrativeCharacterManager.instance.StopCoroutine(transitionBody);
        transitionBody = null;
    }

    public IEnumerator transBody(Sprite sprite, float speed, bool smooth)
    {
        for(int i = 0; i < renderers.allBodies.Count; i++)
        {
            Image img = renderers.allBodies[i];
            if (img.sprite == sprite)
            {
                renderers.body.sprite = sprite;
                break;
            }
        }

        if (renderers.body.sprite != sprite)
        { 
            Image image = GameObject.Instantiate(renderers.body.gameObject, renderers.body.transform.parent).GetComponent<Image>();
            image.sprite = sprite;
            renderers.allBodies.Add(image);
            renderers.body = image;
            image.color = GlobalFuncsDialog.setAlpha(image.color, 0f);
        }

        while (GlobalFuncsDialog.TransitionImages(ref renderers.body, ref renderers.allBodies, speed, smooth))
        {
            yield return new WaitForEndOfFrame();
        }

        StopTransitionBody();
    }

    bool isTransitioningExpression { get { return transitionExpression != null; } }
    Coroutine transitionExpression = null;

    public void TransitionExpression(Sprite sprite, float speed, bool smooth)
    {
        if (renderers.expression.sprite == sprite)
            return;

        StopTransitionExpression();
        transitionExpression = NarrativeCharacterManager.instance.StartCoroutine(transExpression(sprite, speed, smooth));
    }

    public void StopTransitionExpression()
    {
        if (isTransitioningExpression)
            NarrativeCharacterManager.instance.StopCoroutine(transitionExpression);
        transitionExpression = null;
    }

    public IEnumerator transExpression(Sprite sprite, float speed, bool smooth)
    {
        for (int i = 0; i < renderers.allExpressions.Count; i++)
        {
            Image img = renderers.allExpressions[i];
            if (img.sprite == sprite)
            {
                renderers.expression.sprite = sprite;
                break;
            }
        }

        if (renderers.expression.sprite != sprite)
        {
            Image image = GameObject.Instantiate(renderers.expression.gameObject, renderers.expression.transform.parent).GetComponent<Image>();
            image.sprite = sprite;
            renderers.allExpressions.Add(image);
            renderers.expression = image;
            image.color = GlobalFuncsDialog.setAlpha(image.color, 0f);
        }

        while (GlobalFuncsDialog.TransitionImages(ref renderers.expression, ref renderers.allExpressions, speed, smooth))
        {
            yield return new WaitForEndOfFrame();
        }

        StopTransitionExpression();
    }


    ///

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
            renderers.body = gob.transform.Find("BodyLayer").GetComponentInChildren<Image>();
            renderers.expression = gob.transform.Find("ExpressionLayer").GetComponentInChildren<Image>();

            renderers.allBodies.Add(renderers.body);
            renderers.allExpressions.Add(renderers.expression);
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

        public List<Image> allBodies = new List<Image>();
        public List<Image> allExpressions = new List<Image>();
    }

    public Renderers renderers = new Renderers();
}
