using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerObj;
    private Transform lookat;
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        lookat = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(lookat.position.x, transform.position.y, lookat.position.z));
    }
}
