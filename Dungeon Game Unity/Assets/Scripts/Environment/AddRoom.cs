using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;
    public bool nextToEntry = false;
    public bool isBossRoom = false;

    

    private void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    private void Start()
    {
        templates.rooms.Add(this.gameObject);
    }
}
