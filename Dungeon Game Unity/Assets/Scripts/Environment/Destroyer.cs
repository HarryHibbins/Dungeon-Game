using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private RoomTemplates roomTemplates;

    private void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="RoomPrefab")
        {
            roomTemplates.rooms.Remove(other.gameObject);
            Destroy(other.gameObject);
            Debug.Log("DESTROYED");
        }
        
    }
}
