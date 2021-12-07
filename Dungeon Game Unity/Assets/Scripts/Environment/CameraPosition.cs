using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private RoomTemplates templates;
    private PlayerStats playerStats;
    private GameLoot gameLoot;

    private GameObject cameraObj;
    private Transform cameraTransform;
    
    Transform newCameraPoint;
    private Transform room;
    private GameObject roomobj;

    bool moveCamera;
    public bool inRoom;
    private float lerpValue = 3;

    public GameObject FOW_Ceiling;
    private bool fadeout = false;
    public bool hasVisited = false;

    //public GameObject fogobj;
    //public ParticleSystem fog;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();

        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();


    }

    void Start()
    {
        cameraTransform = cameraObj.GetComponent<Transform>();
        
        room = transform.parent.transform;
        roomobj = transform.parent.gameObject;

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

        foreach (Transform child in room)
        {
            if (child.tag == "Fog")
            {
                FOW_Ceiling = child.gameObject;
            }
        }

        //fog = room.GetComponentInChildren<ParticleSystem>();
    }
    
    
    void Update()
    {
        if (moveCamera)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position , newCameraPoint.position, lerpValue * Time.deltaTime);
      
        }
        if (fadeout)
        { 
            Color col = FOW_Ceiling.GetComponent<Renderer>().material.color;

            col.a -= (3 * Time.deltaTime);
            FOW_Ceiling.GetComponent<Renderer>().material.color = col;
            if (col.a < 0)
            {
                FOW_Ceiling.SetActive(false);
                fadeout = false;
            }
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
                /*var main = fog.main;
                var sz = fog.sizeOverLifetime;
                sz.enabled = true;
                fog.Stop();*/
                if (room.name == "Boss Room")
                {
                    Debug.Log("IN BOSS ROOM1");
                    templates.boss.SetActive(true);
                    
                }
                if (!room.name.Contains("Entry Room"))
                {
                    fadeout = true;
                }
                if (!hasVisited && room.name != "Entry Room")
                {
                    playerStats.GameRooms++;
                    hasVisited = true;

                    if (gameLoot.getLootByName(LootItems.Loot.ExplorerRelic).isActive)
                    {
                        gameLoot.explorerRelicRooms++;
                    }
                }
                foreach (Transform child in transform.parent)
                {
                    if (child.tag == ("Enemy"))
                    {
                        child.GetComponentInChildren<EnemyAI>().canSee = true;
                    }
                }
            }
            else
            {
                moveCamera = false;
                inRoom = false;
                Debug.Log("Exit room");

                foreach (Transform child in transform.parent)
                {
                    if (child.tag == ("Enemy"))
                    {
                        child.GetComponentInChildren<EnemyAI>().canSee = false;

                    }
                }

            }
            

        }
    }
}