using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameLoot : MonoBehaviour
{
    public GameObject lootPrefab;
    public List<LootItems> lootList;

    private PlayerStats playerStats;
    private GameObject player;
    private PlayerInventory playerInventory;

    public Sprite PBSR_Sprite;
    public Sprite PDSR_Sprite;
    public Sprite NMPR_Sprite;

    private LootItems QuiverNormal;
    private LootItems QuiverFire;
    private LootItems QuiverIce;
    private LootItems QuiverExplosive;
    private LootItems QuiverSpeed;
    private LootItems PlayerBaseSpeedRelic;
    private LootItems PlayerDrawSpeedRelic;
    private LootItems NoMovementPenaltyRelic;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>();

        QuiverNormal = new LootItems(LootItems.Loot.QuiverNormal, LootItems.LootType.Consumable, LootItems.LootRarity.Common);
        QuiverFire = new LootItems(LootItems.Loot.QuiverFire, LootItems.LootType.Consumable, LootItems.LootRarity.Common);
        QuiverIce = new LootItems(LootItems.Loot.QuiverIce, LootItems.LootType.Consumable, LootItems.LootRarity.Common);
        QuiverExplosive = new LootItems(LootItems.Loot.QuiverExplosive, LootItems.LootType.Consumable, LootItems.LootRarity.Common);
        QuiverSpeed = new LootItems(LootItems.Loot.QuiverSpeed, LootItems.LootType.Consumable, LootItems.LootRarity.Common);
        PlayerBaseSpeedRelic = new LootItems(LootItems.Loot.PlayerBaseSpeedRelic, PBSR_Sprite, LootItems.LootType.Relic, LootItems.LootRarity.Uncommon);
        PlayerDrawSpeedRelic = new LootItems(LootItems.Loot.PlayerDrawSpeedRelic, PDSR_Sprite, LootItems.LootType.Relic, LootItems.LootRarity.Uncommon);
        NoMovementPenaltyRelic = new LootItems(LootItems.Loot.NoMovementPenaltyRelic, NMPR_Sprite, LootItems.LootType.Relic, LootItems.LootRarity.Epic);

        lootList = new List<LootItems>(LootItems.lootList);
    }

    LootItems getLootByRarity(LootItems.LootRarity rarity)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_rarity == rarity)
            {
                tempList.Add(item);
            }
        }

        int rand = UnityEngine.Random.Range(0, tempList.Count);
        return tempList[rand];
    }

    LootItems getLootByType(LootItems.LootType type)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_type == type)
            {
                tempList.Add(item);
            }
        }

        int rand = UnityEngine.Random.Range(0, tempList.Count);
        return tempList[rand];
    }

    void SpawnLoot(Vector3 position, LootItems.Loot name)
    {
        GameObject loot = Instantiate(lootPrefab, position, Quaternion.identity);
        LootPickUp script = loot.GetComponent<LootPickUp>();
        script.loot_type = name;
    }

    void RemoveFromPool(LootItems.Loot name)
    {
        List<LootItems> tempList = new List<LootItems>(lootList);
        for (int i = 0; i < lootList.Count; i++)
        {
            if (lootList[i].loot_name == name && lootList[i].loot_type == LootItems.LootType.Relic)
            {
                lootList.RemoveAt(i);
            }
        }
    }

    // Using a Coroutine incase we want an effect that is time limited - Use 'yield return new WaitForSeconds()"
    public IEnumerator LootEffect(LootItems.Loot loot)
    {
        RemoveFromPool(loot);
        if (loot == LootItems.Loot.QuiverNormal)
        {
            playerInventory.normalArrowCount = playerStats.playerinventory_maxNormalArrows;
        }
        else if (loot == LootItems.Loot.QuiverFire)
        {
            playerInventory.fireArrowCount = playerStats.playerinventory_maxFireArrows;
        }
        else if (loot == LootItems.Loot.QuiverIce)
        {
            playerInventory.iceArrowCount = playerStats.playerinventory_maxIceArrows;
        }
        else if (loot == LootItems.Loot.QuiverExplosive)
        {
            playerInventory.explosiveArrowCount = playerStats.playerinventory_maxExplosiveArrows;
        }
        else if (loot == LootItems.Loot.QuiverSpeed)
        {
            playerInventory.speedArrowCount = playerStats.playerinventory_maxSpeedArrows;
        }
        else if (loot == LootItems.Loot.PlayerBaseSpeedRelic)
        {
            playerStats.playermovement_BaseSpeed += 2;
        }
        else if (loot == LootItems.Loot.PlayerDrawSpeedRelic)
        {
            playerStats.playermovement_DrawSpeed += 2;
        }
        else if (loot == LootItems.Loot.NoMovementPenaltyRelic)
        {
            // Might need to do this in PlayerController so it updates if speed ever gets increased again.
            playerStats.playermovement_DrawSpeed = playerStats.playermovement_BaseSpeed;
        }
        yield break;
    }
}
