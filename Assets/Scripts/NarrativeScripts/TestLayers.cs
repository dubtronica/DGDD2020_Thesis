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
        BGFGCineController.Layer layer = null;

        if (Input.GetKey(KeyCode.Q))
        {
            layer = controls.background;
            Debug.Log("background");
        }
            
        if (Input.GetKey(KeyCode.W))
        {
            layer = controls.cinematics;
            Debug.Log("cinematic");
        }
            
        if (Input.GetKey(KeyCode.E))
        {
            layer = controls.foreground;
            Debug.Log("foreground");
        }

        if (Input.GetMouseButtonDown(1))
            layer.setTexture(tex2);
        if (Input.GetMouseButtonDown(0))
            layer.setTexture(tex1);
    }
}
