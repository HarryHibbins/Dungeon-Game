using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTorch : MonoBehaviour
{
    private PlayerStats playerStats;
    private GameObject playerObj;
    private PlayerController player;
    private Animator anim;
    private GameObject playerTorchHolderObj;
    private Transform playerTorchHolder;
    public bool canPickUpTorch;
    public bool hasTorch;
    //public float maxTimer = 30;
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
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
    }

    void Start()
    {
        torchTimer = playerStats.Torch_MaxTimer;
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
                torchTimer = playerStats.Torch_MaxTimer;

                transform.position = playerTorchHolder.transform.position;

                transform.parent = playerTorchHolder;
                //  transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

                held = true;
            }



            }

            if (hasTorch) 
        {
            torchTimer -= Time.deltaTime;
            flame.GetComponent<Light>().range = (torchTimer / playerStats.Torch_MaxTimer) * range;
            flame.GetComponent<Light>().intensity *= (torchTimer / playerStats.Torch_MaxTimer);
            sparks.GetComponent<ParticleSystem>().startSize = (torchTimer / playerStats.Torch_MaxTimer);
            sparks.GetComponent<ParticleSystem>().startLifetime = (torchTimer / playerStats.Torch_MaxTimer) * 1.5f;
            sparks.gameObject.transform.Find("Fire_Sparks").gameObject.GetComponent<ParticleSystem>().startSize = (torchTimer / playerStats.Torch_MaxTimer);
            sparks.gameObject.transform.Find("Fire_Sparks").gameObject.GetComponent<ParticleSystem>().startLifetime = (torchTimer / playerStats.Torch_MaxTimer) * 1.5f;

            if (torchTimer < 0) 
            {
                FindObjectOfType<AudioManager>().Play("Torchout");
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
