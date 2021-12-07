using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TorchSpawning : MonoBehaviour
{
    private GameObject flame;
    [SerializeField] CameraPosition cameraPosition;
    [SerializeField] private GameObject room;
    private bool torchesSpawned;

    private int numOfFlames;
    
    //Chance of spawning enemies per spawn point between 0 and 1
    [SerializeField] private float spawnChance;
    [SerializeField] private int minFlamesInRoom;
    [SerializeField] private int maxFlamesInRoom;

    private void Awake()
    {
        flame = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Flame>().flame;
        room = transform.parent.gameObject;
        cameraPosition = room.GetComponentInChildren<CameraPosition>();
        if (transform.parent.name == "Entry Room")
        {
            spawnTorches();
        }
    }
    
    private void Update()
    {
        if (cameraPosition.hasVisited && !torchesSpawned && transform.parent.name != "Entry Room" /*|| transform.parent.name != "Closed"*/)
        {
            spawnTorches();
        }



    }

    private void spawnTorches()
    {
        foreach (Transform child in transform)
        {
            
            if (child.tag == "Torch Spawn Point")
            {
                float randomValue = Random.value;
              
                if ( randomValue <= spawnChance && numOfFlames < maxFlamesInRoom) {
                    Instantiate(flame, child.position, child.rotation);
                    numOfFlames++;

                }
                else {
                    //minimum of 2 enemies per room
                    if (numOfFlames < minFlamesInRoom  && numOfFlames < maxFlamesInRoom)
                    {
                        Instantiate(flame, child.position, child.rotation);

                        numOfFlames++;
                    }
                }
            }

        }

        torchesSpawned = true;
    }

    void Start()
    {
        
    }

}
