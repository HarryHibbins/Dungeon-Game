using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private bool inRange = false;
    private bool opened = false;
    private GameLoot gameLoot;
    public GameObject lootpos;

    private void Awake()
    {
        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
    }

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetButtonDown("Interact") && !opened)
            {
                OpenLootChest();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }

    void OpenLootChest()
    {
        //Play chest animation
        Vector3 chest_location = this.transform.position;
        gameLoot.SpawnLoot(lootpos.transform.position, gameLoot.getLootByRarityToSpawn(gameLoot.RandomRarity()));
        opened = true;
    }
}
