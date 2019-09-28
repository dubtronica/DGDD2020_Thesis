using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLayers : MonoBehaviour
{

    BGFGCineController controls;
    public Texture tex1, tex2;

    public bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        controls = BGFGCineController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        BGFGCineController.Layer layer = controls.background;



        if (flag)
            layer.setTexture(tex1);
        else
            layer.setTexture(tex2);
    }
}
