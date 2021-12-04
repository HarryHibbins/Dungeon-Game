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
    public float intensity = 20;
    public float range = 10;
    public float torchTimer;
    public GameObject flame;
    public GameObject sparks;
    public GameObject textBubble;
    public Vector3 originalPos;
    public bool held = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerTorchHolderObj = GameObject.FindWithTag("Flame Holder");
        //playerObj = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        torchTimer = maxTimer;
        originalPos = this.transform.position;
        playerTorchHolder = playerTorchHolderObj.transform;
        textBubble = GameObject.FindWithTag("textBubble");
        //flame = this.gameObject.transform.Find("Torch (1)").gameObject;
    }

    void Update()
    {
        if (canPickUpTorch && Input.GetButtonDown("Interact"))
        {
            if (!held)
            {
                //if (playerTorchHolder.Find("CurrentTorch").gameObject.GetComponent<FixedTorch>().hasTorch) 
                if (player.GetComponent<PlayerInventory>().holdingTorch)
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

                textBubble.GetComponent<TextMesh>().text = "";
                held = true;
            }



            }

            if (hasTorch) 
        {
            torchTimer -= Time.deltaTime;
            flame.GetComponent<Light>().range = (torchTimer / maxTimer) * range;
            flame.GetComponent<Light>().intensity *= (torchTimer / maxTimer);
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
            if (!held) 
            {
                textBubble.GetComponent<TextMesh>().text = "E";    
            }
        }
    }

 
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))     
        {
            canPickUpTorch = false;
            textBubble.GetComponent<TextMesh>().text = "";
        }
    }
}
