using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveFile : MonoBehaviour
{
	
	private static int numOfChars = 21;
	private static string[] characterNames = new string[numOfChars];
	private static Sprite[] characterPics = new Sprite[numOfChars];
	
	private static int levi = 1;
	private static int zoro = 2;
	private static int natsu = 3;
	
	
	/*
	FORMAT FOR TEXT FILE:
	charactername
	attack power (?)
	*/
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
