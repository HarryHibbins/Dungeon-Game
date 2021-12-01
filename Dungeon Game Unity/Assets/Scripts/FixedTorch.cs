using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTorch : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerController player;
    private Animator anim;
    private GameObject playerTorchHolderObj;
    private Transform playerTorchHolder;
    public bool canPickUpTorch;
    public bool hasTorch;
    public float maxTimer = 30;
    public float torchTimer;
    public GameObject flame;
    public GameObject sparks;
    public Vector3 originalPos;
    void Start()
    {
        torchTimer = maxTimer;
        originalPos = this.transform.position;
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerController>();
        anim = playerObj.GetComponent<Animator>();
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
            sparks = this.gameObject.transform.Find("Fire").gameObject;
            Debug.Log("Pick up torch");

            hasTorch = true;
            player.GetComponent<PlayerInventory>().holdingTorch = true;
            anim.SetBool("Torch", true);
            torchTimer = 30;
          
            transform.position = playerTorchHolder.transform.position;
            
            transform.parent = playerTorchHolder;
          //  transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        
        if (hasTorch) 
        {
            torchTimer -= Time.deltaTime;
            flame.GetComponent<Light>().range = (torchTimer / maxTimer) * 10;
            sparks.GetComponent<ParticleSystem>().startSize = (torchTimer / maxTimer);
            sparks.GetComponent<ParticleSystem>().startLifetime = (torchTimer / maxTimer) * 1.5f;
            sparks.gameObject.transform.Find("Fire_Sparks").gameObject.GetComponent<ParticleSystem>().startSize = (torchTimer / maxTimer);
            sparks.gameObject.transform.Find("Fire_Sparks").gameObject.GetComponent<ParticleSystem>().startLifetime = (torchTimer / maxTimer) * 1.5f;

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
