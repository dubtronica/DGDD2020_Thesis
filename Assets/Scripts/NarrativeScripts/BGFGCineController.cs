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
                } else
                {

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
