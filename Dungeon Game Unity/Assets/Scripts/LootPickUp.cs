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

    [SerializeField]
    private LootItems.Loot loot_type;

    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerInventory = playerObj.GetComponent<PlayerInventory>();
        playerController = playerObj.GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerStats = gameManager.GetComponent<PlayerStats>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerPickUp")
        {
            switch (loot_type)
            {
                case LootItems.Loot.QuiverNormal:
                    {
                        playerInventory.normalArrowCount = playerStats.playerinventory_maxNormalArrows;
                        break;
                    }
                case LootItems.Loot.QuiverFire:
                    {
                        playerInventory.fireArrowCount = playerStats.playerinventory_maxFireArrows;
                        break;
                    }
                case LootItems.Loot.QuiverIce:
                    {
                        playerInventory.iceArrowCount = playerStats.playerinventory_maxIceArrows;
                        break;
                    }
                case LootItems.Loot.QuiverExplosive:
                    {
                        playerInventory.explosiveArrowCount = playerStats.playerinventory_maxExplosiveArrows;
                        break;
                    }
                case LootItems.Loot.QuiverSpeed:
                    {
                        playerInventory.speedArrowCount = playerStats.playerinventory_maxSpeedArrows;
                        break;
                    }
                case LootItems.Loot.PlayerBaseSpeedRelic:
                    {
                        playerStats.playermovement_BaseSpeed += 2;
                        break;
                    }
                case LootItems.Loot.PlayerDrawSpeedRelic:
                    {
                        playerStats.playermovement_DrawSpeed += 2;
                        break;
                    }
                case LootItems.Loot.NoMovementPenaltyRelic:
                    {
                        // Might need to do this in PlayerController so it updates if speed ever gets increased again.
                        playerStats.playermovement_DrawSpeed = playerStats.playermovement_BaseSpeed;
                        break;
                    }
            }
        }
        Destroy(this.gameObject);
    }
}
