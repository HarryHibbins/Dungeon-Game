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

    public GameObject B_TreasureRoom;
    public GameObject L_TreasureRoom;
    public GameObject LB_TreasureRoom;
    public GameObject LR_TreasureRoom;
    public GameObject R_TreasureRoom;
    public GameObject RB_TreasureRoom;
    public GameObject T_TreasureRoom;
    public GameObject TB_TreasureRoom;
    public GameObject TL_TreasureRoom;
    public GameObject TR_TreasureRoom;

    public List<GameObject> rooms;
    public List<GameObject> possibleTreasureRooms;
    public int roomCount;

    public float startWaitTime;
    public float waitTime;
    public bool spawnedBoss;
    private bool spawnedTreasureRoom;
    public GameObject boss;
    private GameObject treasureRoom;

    private PlayerStats playerStats;

    public int SpawnNumber = 0;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
    }

    private void Start()
    {
        startWaitTime = waitTime;
    }

    private void Update()
    {
        if (waitTime <= 0 && !spawnedBoss)
        {
            Instantiate(boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
            rooms[rooms.Count-1].GetComponent<AddRoom>().isBossRoom = true;
            spawnedBoss = true;
        }
        else if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }

        if (spawnedBoss && !spawnedTreasureRoom)
        {
            int rand = UnityEngine.Random.Range(0, 101);
            if (rand <= playerStats.TreasureRoomChance)
            {
                SpawnTreasureRoom();
            }
            else
            {
                spawnedTreasureRoom = true;
            }
        }
    }

    void SpawnTreasureRoom()
    {
        foreach (GameObject room in rooms)
        {
            if (!room.GetComponent<AddRoom>().isBossRoom && !room.GetComponent<AddRoom>().nextToEntry && room.name != "Entry Room")
            {
                possibleTreasureRooms.Add(room);
            }
        }
        if (possibleTreasureRooms.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, possibleTreasureRooms.Count);
            string roomName = possibleTreasureRooms[rand].name;

            switch (roomName)
            {
                case "B":
                    treasureRoom = Instantiate(B_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                case "L":
                    treasureRoom = Instantiate(L_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                case "LB":
                    treasureRoom = Instantiate(LB_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                case "LR":
                    treasureRoom = Instantiate(LR_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                case "R":
                    treasureRoom = Instantiate(R_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                case "RB":
                    treasureRoom = Instantiate(RB_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                case "T":
                    treasureRoom = Instantiate(T_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                case "TB":
                    treasureRoom = Instantiate(TB_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                case "TL":
                    treasureRoom = Instantiate(TL_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                case "TR":
                    treasureRoom = Instantiate(TR_TreasureRoom, possibleTreasureRooms[rand].transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
            int roomIndex = rooms.IndexOf(possibleTreasureRooms[rand]);
            treasureRoom.name = "*****TreasureRoom";
            Destroy(possibleTreasureRooms[rand].gameObject);
            possibleTreasureRooms.RemoveAt(rand);
            rooms.RemoveAt(roomIndex);
            possibleTreasureRooms.Clear();
        }
        spawnedTreasureRoom = true;
    }
}
