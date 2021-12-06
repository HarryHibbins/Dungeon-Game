using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public Material testmat;
    public GameObject chestplate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Material[] mats = chestplate.GetComponent<Renderer>().materials;
            mats[0] = testmat;
            chestplate.GetComponent<MeshRenderer>().materials = mats;
        }
    }
}
