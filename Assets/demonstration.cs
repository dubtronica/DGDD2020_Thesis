using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonstration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Antibiotic ab = new Antibiotic("Gram Negative");
        Bacteria ba = new Bacteria();
        Bacteria bb = new Bacteria("Gram Negative");
        Virus vi = new Virus();

        ab.normalAttack(ba);
        ab.normalAttack(bb);
        ab.normalAttack(vi);
    }
}
