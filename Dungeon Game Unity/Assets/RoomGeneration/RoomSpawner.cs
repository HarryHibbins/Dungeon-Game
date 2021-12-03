using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 -> need bottom door
    // 2 -> need top door
    // 3 -> need left door
    // 4 -> need right door

    private GameObject newRoom;
    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    public float waitTime = 4f;

    private void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    private void Start()
    {
        //Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.5f);
    }

    private void Update()
    {
        if (templates.spawnedBoss)
        {
            Destroy(gameObject);
        }
    }

    private void Spawn()
    {
        if (spawned == false)
        {
            
            if (openingDirection == 1)
            {
                // Need bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                newRoom = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                newRoom.name = templates.bottomRooms[rand].name;
            }
            else if (openingDirection == 2)
            {
                // Need top door
                rand = Random.Range(0, templates.topRooms.Length);
                newRoom = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                newRoom.name = templates.topRooms[rand].name;
            }
            else if (openingDirection == 3)
            {
                // Need left door
                rand = Random.Range(0, templates.leftRooms.Length);
                newRoom = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                newRoom.name = templates.leftRooms[rand].name;
            }
            else if (openingDirection == 4)
            {
                // Need right door
                rand = Random.Range(0, templates.rightRooms.Length);
                newRoom = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                newRoom.name = templates.rightRooms[rand].name;
            }
            spawned = true;
            templates.waitTime = templates.startWaitTime;

            if (transform.parent.name == "Entry Room")
            {
                newRoom.GetComponent<AddRoom>().nextToEntry = true;
            }
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

}
