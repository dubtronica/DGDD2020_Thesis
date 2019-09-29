using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BGFGCineController : MonoBehaviour
{
    public static BGFGCineController instance;

    public Layer background = new Layer();
    public Layer foreground = new Layer();
    public Layer cinematics = new Layer();

    void Awake()
    {
        instance = this;
    }


    [System.Serializable]
    public class Layer
    {
        public GameObject root, newIMGObjectRef;
        public RawImage activeImage;
        public List<RawImage> allImages = new List<RawImage>();

        public void setTexture(Texture texture, bool loopMovie = true)
        {
           if(activeImage != null && activeImage.texture != null)
            {
                MovieTexture mv = texture as MovieTexture;

                if (mv != null)
                    mv.Stop();
            }

            if(texture != null)
            {
                if (activeImage == null)
                {
                    createNewActiveImage();
                    Debug.Log("Active image created");
                    Debug.Log("allImages count: " + allImages.Count);
                }
                

                activeImage.texture = texture;
                activeImage.color = GlobalFuncsDialog.setAlpha(activeImage.color, 1f);

                MovieTexture mv = texture as MovieTexture;

                if(mv != null)
                {
                    mv.loop = loopMovie;
                    mv.Play();
                } 
            }
            else
            {
                if (activeImage != null)
                {
                    allImages.Remove(activeImage);
                    GameObject.DestroyImmediate(activeImage.gameObject);
                    activeImage = null;
                }
            }
        }

        public void TransitionToTexture(Texture texture, float speed = 2f, bool smooth = false, bool loopIfMovie = true)
        {
            if (activeImage != null && activeImage.texture == texture)
                return;

            stopTransitioning();

            transitioning = BGFGCineController.instance.StartCoroutine(Transitioning(texture, speed, smooth, loopIfMovie));

        }

        void stopTransitioning()
        {
            if (isTransitioning)
            {
                BGFGCineController.instance.StopCoroutine(transitioning);
            }
            transitioning = null;


        }

        Coroutine transitioning = null;
        public bool isTransitioning { get { return transitioning != null; } }
        IEnumerator Transitioning(Texture texture, float speed, bool smooth, bool loopIfMovie)
        {
            if(texture != null)
            {
                for (int i = 0; i < allImages.Count; i++)
                {
                    RawImage img = allImages[i];
                    if (img.texture == texture)
                    {
                        activeImage = img;
                        break;
                    }
                }

                if (activeImage == null || activeImage.texture != texture)
                {
                    createNewActiveImage();
                    activeImage.texture = texture;
                    activeImage.color = GlobalFuncsDialog.setAlpha(activeImage.color, 0f);

                    MovieTexture mv = texture as MovieTexture;

                    if (mv != null)
                    {
                        mv.loop = loopIfMovie;
                        mv.Play();
                    }
                }
            } else
            {
                activeImage = null;
            }

            while (GlobalFuncsDialog.TransitionRawImages(ref activeImage, ref allImages, speed, smooth))
                yield return new WaitForEndOfFrame();

            stopTransitioning();

        }

        void createNewActiveImage()
        {
            GameObject gob = Instantiate(newIMGObjectRef, root.transform) as GameObject;
            gob.SetActive(true);
            RawImage raw = gob.GetComponent<RawImage>();
            activeImage = raw;
            allImages.Add(raw);

        }
    }
}
