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
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerController>();
        playerTorchHolderObj = GameObject.FindWithTag("Flame Holder");
        playerTorchHolder = playerTorchHolderObj.transform;
//        flame = this.gameObject.transform.Find("Torch (1)").gameObject;
    }

    void Update()
    {
        if (canPickUpTorch && Input.GetButtonDown("Interact") && !hasTorch)
        {
            flame = this.gameObject.transform.Find("Torch (1)").gameObject;
            Debug.Log("Pick up torch");

            hasTorch = true;
            torchTimer = 30;
          
            transform.position = playerTorchHolder.transform.position;
            
            transform.parent = playerTorchHolder;
          //  transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        if (hasTorch) 
        {
            torchTimer -= Time.deltaTime;
            flame.GetComponent<Light>().range = (torchTimer / 30) * 10;
                    
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
