using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameLoot : MonoBehaviour
{
    public GameObject lootPrefab;

    private PlayerStats playerStats;
    private GameObject player;
    private PlayerInventory playerInventory;

    public Sprite PBSR_Sprite;
    public Sprite PDSR_Sprite;
    public Sprite NMPR_Sprite;

    public List<LootItems> lootList;
    public List<LootItems> lootList2;
    [Space(3)]
    [Header("NEW LOOT OBJECT")]
    public LootItems.Loot lootname;
    public Sprite lootsprite;
    public LootItems.LootType loottype;
    public LootItems.LootRarity lootrarity;

    private bool NoMovementPenaltyRelic = false;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        UpdateRelics();
    }

    public void NewLootItem(LootItems.Loot lootname, Sprite lootsprite, LootItems.LootType loottype, LootItems.LootRarity lootrarity)
    {
        LootItems temp = new LootItems(lootname, lootsprite, loottype, lootrarity);
        lootList.Add(temp);
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

    LootItems getLootByTypeAndRarity(LootItems.LootType type, LootItems.LootRarity rarity)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_type == type && item.loot_rarity == rarity)
            {
                tempList.Add(item);
            }
        }

        int rand = UnityEngine.Random.Range(0, tempList.Count);
        return tempList[rand];
    }

    void SpawnLoot(Vector3 position, LootItems name)
    {
        GameObject loot = Instantiate(lootPrefab, position, Quaternion.identity);
        LootPickUp script = loot.GetComponent<LootPickUp>();
        script.LootName = name.loot_name;
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
        else if(loot == LootItems.Loot.MaxAmmo)
        {
            playerInventory.MaxAmmo();
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
            NoMovementPenaltyRelic = true;
        }
        yield break;
    }

    public void UpdateRelics()
    {
        if (NoMovementPenaltyRelic)
        {
            playerStats.playermovement_DrawSpeed = playerStats.playermovement_BaseSpeed;
        }
    }
}
