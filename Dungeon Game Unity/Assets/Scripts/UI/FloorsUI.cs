using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorsUI : MonoBehaviour
{
    public Text FloorText;
    public Text RoomsText;
    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        FloorText.text = playerStats.GameFloors.ToString();
        RoomsText.text = playerStats.GameRooms.ToString();
    }
}
