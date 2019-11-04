using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPtest : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterProfileScript cps;
    
    void Start()
    {
        cps.name = "Amoxicillin";
        cps.hp = 120;
        cps.damage = 70;
        cps.description = "Common version of penicillin";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
