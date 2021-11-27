using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private GameObject cameraObj;
    private Transform cameraTransform;
    
    Transform newCameraPoint;
    private Transform room;

    bool moveCamera;
    bool inRoom;
    private float lerpValue = 3;

    public GameObject fogobj;
    public ParticleSystem fog;

    void Start()
    {
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = cameraObj.GetComponent<Transform>();
        
        room = transform.parent.transform;

        if (room.name == "Entry Room")
        {
            inRoom = true;
        }

        foreach(Transform child in room)
        {
            if (child.tag == "Camera Position")
            {
                newCameraPoint = child;
            }
        }

        fog = room.GetComponentInChildren<ParticleSystem>();
    }
    
    
    void Update()
    {
        if (moveCamera)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position , newCameraPoint.position, lerpValue * Time.deltaTime);
      
        }
    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (!inRoom)
            {
                moveCamera= true;
                inRoom = true;
                Debug.Log("Enter room");
                var main = fog.main;
                var sz = fog.sizeOverLifetime;
                sz.enabled = true;
                fog.Stop();
            }
            else
            {
                moveCamera = false;
                inRoom = false;
                Debug.Log("Exit room");

            }
            

        }
    }
}