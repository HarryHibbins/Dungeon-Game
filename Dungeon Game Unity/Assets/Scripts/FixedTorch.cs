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
    public bool canPickUpTorch;
    public bool hasTorch;
    public float torchTimer = 30;
    public GameObject flame;
    public Vector3 originalPos;
    void Start()
    {
        originalPos = this.transform.position;
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerController>();
        playerTorchHolderObj = GameObject.FindWithTag("Flame Holder");
        playerTorchHolder = playerTorchHolderObj.transform;
//        flame = this.gameObject.transform.Find("Torch (1)").gameObject;
    }

    void Update()
    {
        if (canPickUpTorch && Input.GetButtonDown("Interact"))
        {
            
            //if (playerTorchHolder.Find("CurrentTorch").gameObject.GetComponent<FixedTorch>().hasTorch) 
            if(player.GetComponent<PlayerInventory>().holdingTorch)
            {
                Destroy(playerTorchHolder.Find("CurrentTorch").gameObject);            
            }

            this.name = "CurrentTorch";
            flame = this.gameObject.transform.Find("Torch (1)").gameObject;
            Debug.Log("Pick up torch");

            hasTorch = true;
            player.GetComponent<PlayerInventory>().holdingTorch = true;
            torchTimer = 30;
          
            transform.position = playerTorchHolder.transform.position;
            
            transform.parent = playerTorchHolder;
          //  transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        if (hasTorch) 
        {
            torchTimer -= Time.deltaTime;
            flame.GetComponent<Light>().range = (torchTimer / 30) * 10;

            if (torchTimer < 0) 
            {
                Destroy(this.gameObject);
                hasTorch = false;
                player.GetComponent<PlayerInventory>().holdingTorch = false;
            }
                    
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
