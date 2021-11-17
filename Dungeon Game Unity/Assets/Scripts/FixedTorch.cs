using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTorch : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerController player;
    private GameObject playerTorchHolderObj;
    private Transform playerTorchHolder;
    private bool canPickUpTorch;
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerController>();
        playerTorchHolderObj = GameObject.FindWithTag("Flame Holder");
        playerTorchHolder = playerTorchHolderObj.transform;
    }

    void Update()
    {
        if (canPickUpTorch && Input.GetButtonDown("Interact"))
        {
            Debug.Log("Pick up torch");
          
            transform.position = playerTorchHolder.transform.position;
            transform.parent = playerTorchHolder;
                
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUpTorch = true;
        }
    }

 
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))     
        {
            canPickUpTorch = false;
        }
    }
}
