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
    [SerializeField] Transform cameraTransform;
    
    //Transform[] newCameraPoints;
    private Transform room;
    private GameObject roomobj;

    [SerializeField] private bool moveCamera;
    public bool inRoom;
    private float lerpValue = 3;

    public GameObject FOW_Ceiling;
    private bool fadeout = false;
    public bool hasVisited = false;

    //public GameObject fogobj;
    //public ParticleSystem fog;
    public Transform[] cameraPositions;
    private PlayerController playerController;
    

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();

        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        /*for (int i = 0; i < 3; i++)
        {
            foreach (Transform child in transform.parent)
            {
                if (child.name == "Camera Positions")
                {
                    cameraPositions[i] = transform.GetChild(i);

                }
            }
        }*/




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

        /*foreach(Transform child in room)
        {
            if (child.tag == "Camera Position")
            {
                newCameraPoint = child;
            }
        }*/
        
        /*for (int i = 0; i < 3; i++)
        {
            if (transform.parent.GetChild(i).name == "Camera Position")
            {
                newCameraPoints[i] = transform.GetChild(i);

            }
        }*/

        foreach (Transform child in room)
        {
            if (child.tag == "Fog")
            {
                FOW_Ceiling = child.gameObject;
            }
        }

        //fog = room.GetComponentInChildren<ParticleSystem>();
    }
    

    IEnumerator stopRotating()
    {
        yield return new WaitForSeconds(1.5f);
        moveCamera = false;

    }
    void Update()
    {
        if (moveCamera)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position , cameraPositions[playerController.CameraPos].position, lerpValue * Time.deltaTime);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, cameraPositions[playerController.CameraPos].localRotation, lerpValue * Time.deltaTime);

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


        if (Input.GetKeyDown(KeyCode.E) && inRoom && !moveCamera)
        {
            Debug.Log("Rotate camera");
            if (playerController.CameraPos <= 2)
            {
                playerController.CameraPos++;
            }
            else
            {
                playerController.CameraPos = 0;
            }

            moveCamera = true;
            //rotateCamera();
            StartCoroutine(stopRotating());

        }

        else if (Input.GetKeyDown(KeyCode.Q) && inRoom && !moveCamera)
        {
            Debug.Log("Rotate camera");
            if (playerController.CameraPos >= 1)
            {
                playerController.CameraPos--;
            }
            else
            {
                playerController.CameraPos = 3;
            }

            moveCamera = true;
            //rotateCamera();
            StartCoroutine(stopRotating());

        }

    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (!inRoom)
            {
                moveCamera= true;
                StartCoroutine(stopRotating());
                inRoom = true;
                /*var main = fog.main;
                var sz = fog.sizeOverLifetime;
                sz.enabled = true;
                fog.Stop();*/
                if (room.name == "Boss Room")
                {
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