using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraFollow : MonoBehaviour
{
    private GameObject PlayerObj;
    private Transform PlayerPos;

    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] [Range(0, 1)] private float lerpValue;
    void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = PlayerObj.GetComponent<Transform>();
            
        cameraOffset = transform.position - PlayerPos.position;
    }

    private void Update()
    {
        PlayerPos = PlayerObj.GetComponent<Transform>();

    }

    void FixedUpdate()
    {

        transform.position = Vector3.Lerp(transform.position , PlayerPos.transform.position + cameraOffset, lerpValue);
       
    }
}
