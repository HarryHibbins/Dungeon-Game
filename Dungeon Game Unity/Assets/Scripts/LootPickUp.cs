using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerInventory playerInventory;
    private PlayerController playerController;

    [SerializeField]
    private LootItems.Loot loot_type;

    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerInventory = playerObj.GetComponent<PlayerInventory>();
        playerController = playerObj.GetComponent<PlayerController>();
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
                        playerInventory.normalArrowCount = playerInventory.maxNormalArrows;
                        break;
                    }
                case LootItems.Loot.QuiverFire:
                    {
                        playerInventory.fireArrowCount = playerInventory.maxFireArrows;
                        break;
                    }
                case LootItems.Loot.QuiverIce:
                    {
                        playerInventory.iceArrowCount = playerInventory.maxIceArrows;
                        break;
                    }
                case LootItems.Loot.QuiverExplosive:
                    {
                        playerInventory.explosiveArrowCount = playerInventory.maxExplosiveArrows;
                        break;
                    }
                case LootItems.Loot.QuiverSpeed:
                    {
                        playerInventory.speedArrowCount = playerInventory.maxSpeedArrows;
                        break;
                    }
                case LootItems.Loot.PlayerBaseSpeedRelic:
                    {
                        playerController.normalMoveSpeed += 2;
                        break;
                    }
                case LootItems.Loot.PlayerDrawSpeedRelic:
                    {
                        playerController.drawBowMoveSpeed += 2;
                        break;
                    }
                case LootItems.Loot.NoMovementPenaltyRelic:
                    {
                        // Might need to do this in PlayerController so it updates if speed ever gets increased again.
                        playerController.drawBowMoveSpeed = playerController.normalMoveSpeed;
                        break;
                    }
            }
        }
        Destroy(this.gameObject);
    }
}
