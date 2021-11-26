using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float startWaitTime;
    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    private void Start()
    {
        startWaitTime = waitTime;
    }

    private void Update()
    {
        if (waitTime <= 0 && !spawnedBoss)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count-1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
    }
}