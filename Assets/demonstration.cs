using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonstration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ImmuCharacter ic = new ImmuCharacter();
        Hero hr = new Hero();
        Enemy en = new Enemy();

        ic.introduceYourself();
        ic.takeDamage(50);

        hr.introduceYourself();
        hr.takeDamage(50);

        en.introduceYourself();
        en.takeDamage(50);
    }
}
