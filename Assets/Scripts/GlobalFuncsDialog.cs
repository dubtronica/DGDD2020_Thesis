using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalFuncsDialog : MonoBehaviour
{
    public static bool TransitionImages(ref Image activeImage, ref List<Image> allImages, float speed, bool smooth = true)
    {
        bool valueChanged = false;

        speed *= Time.deltaTime;
        for(int i = allImages.Count - 1; i > 0; i--)
        {
            Image img = allImages[i];

            if(img == activeImage)
            {
                if(img.color.a < 1f)
                {
                    img.color = setAlpha(img.color, smooth ? Mathf.Lerp(img.color.a, 1f, speed) : Mathf.MoveTowards(img.color.a, 1f, speed));//transition
                    valueChanged = true;
                }
            }
            else
            {
                Debug.Log(i + "Alpha value: " + img.color.a);
                if(img.color.a > 0)
                {
                    img.color = setAlpha(img.color, smooth ? Mathf.Lerp(img.color.a, 0f, speed) : Mathf.MoveTowards(img.color.a, 0f, speed));
                    valueChanged = true;
                }
                else
                {
                    allImages.RemoveAt(i);
                    DestroyImmediate(img.gameObject);
                    continue;
                }
            }
        }

        return valueChanged;
    } 

    public static Color setAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
