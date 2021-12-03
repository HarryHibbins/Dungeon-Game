using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWallCheck : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Door;

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag == "DoorTag" && other.tag == "WallTag")
        {
            Debug.Log("ERROR: " + other.transform.parent.name);

            other.GetComponent<RoomWallCheck>().Wall.SetActive(false);
            other.GetComponent<RoomWallCheck>().Door.SetActive(true);

        }
    }
}
