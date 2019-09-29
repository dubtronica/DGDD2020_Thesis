using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalFuncsDialog : MonoBehaviour
{
    //transition effects
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
                if(img.color.a > 0)
                {
                    img.color = setAlpha(img.color, smooth ? Mathf.Lerp(img.color.a, 0f, speed) : Mathf.MoveTowards(img.color.a, 0f, speed));
                    valueChanged = true;
                    
                } else
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

    public static bool TransitionRawImages(ref RawImage activeImage, ref List<RawImage> allImages, float speed, bool smooth = true)
    {
        bool valueChanged = false;

        speed *= Time.deltaTime;
        for (int i = allImages.Count - 1; i > 0; i--)
        {
            RawImage img = allImages[i];

            if (img == activeImage)
            {
                if (img.color.a < 1f)
                {
                    img.color = setAlpha(img.color, smooth ? Mathf.Lerp(img.color.a, 1f, speed) : Mathf.MoveTowards(img.color.a, 1f, speed));//transition
                    valueChanged = true;
                }
            }
            else
            {
                if (img.color.a > 0)
                {
                    img.color = setAlpha(img.color, smooth ? Mathf.Lerp(img.color.a, 0f, speed) : Mathf.MoveTowards(img.color.a, 0f, speed));
                    valueChanged = true;
                } else
                {
                    MovieTexture mv = img.texture as MovieTexture;

                    if (mv != null)
                        mv.Stop();
                    allImages.RemoveAt(i);
                    DestroyImmediate(img.gameObject);
                    continue;
                }
            }
        }

        return valueChanged;
    }
}
