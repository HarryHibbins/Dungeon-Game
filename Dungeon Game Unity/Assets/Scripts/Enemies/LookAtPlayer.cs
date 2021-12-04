using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerObj;
    private Transform lookat;

    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        lookat = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(lookat.position.x, transform.position.y, lookat.position.z));
    }
}
