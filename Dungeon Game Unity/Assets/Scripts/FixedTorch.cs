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
          
            foreach (Transform child in transform)
            {
                //child.transform.position = new Vector3(0, 0, 0);
                child.transform.position = playerTorchHolder.transform.position;
                child.parent = playerTorchHolder;
                
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canPickUpTorch = true;
        }
    }

 
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canPickUpTorch = false;
        }
    }
}
