using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private GameObject cameraObj;
    private Transform cameraTransform;
    
    private Transform newCameraPoint;

    [SerializeField] bool moveCamera;
    [SerializeField] bool CurrentRoomCollidersEnabled;
     private float lerpValue = 2;




    void Start()
    {
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = cameraObj.GetComponent<Transform>();

        foreach(Transform child in transform)
        {
            if (child.tag == "Camera Position")
            {
                newCameraPoint = child;
            }
        }
    }
    
    
    
    void Update()
    {
        if (moveCamera)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position , newCameraPoint.position, lerpValue * Time.deltaTime);
      
        }

        //This is never happening because the new camera point is parented and the local pos != the camera's world pos
        //If i unparent it i could make this conidtion be met but idk how to get each camera position for each room - maybe just loop through whole object?
        if (cameraTransform.position == newCameraPoint.position) 
        {
            CurrentRoomCollidersEnabled = false;
            moveCamera = false;
        }
        else
        {

            CurrentRoomCollidersEnabled = true;
        }

    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CurrentRoomCollidersEnabled)
        {
            moveCamera= true;

        }
    }
}