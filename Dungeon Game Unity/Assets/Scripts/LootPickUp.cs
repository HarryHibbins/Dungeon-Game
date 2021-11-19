using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerInventory playerInventory;
    private PlayerController playerController;
    private GameObject gameManager;
    private PlayerStats playerStats;
    private GameLoot lootScript;

    public LootItems.Loot loot_type;

    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerInventory = playerObj.GetComponent<PlayerInventory>();
        playerController = playerObj.GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerStats = gameManager.GetComponent<PlayerStats>();
        lootScript = gameManager.GetComponent<GameLoot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerPickUp")
        {
            StartCoroutine(lootScript.LootEffect(loot_type));
            Destroy(this.gameObject);
        }
    }
}
