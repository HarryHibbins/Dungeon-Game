using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorsUI : MonoBehaviour
{
    public Text FloorText;
    public Text RoomsText;
    private PlayerStats playerStats;
    private RoomTemplates templates;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    private void Update()
    {
        FloorText.text = playerStats.GameFloors.ToString();
        RoomsText.text = playerStats.GameRooms.ToString() + "/" + templates.roomCount.ToString();
    }
}
