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

    public List<LootItems> lootList;
    [Space(3)]
    [Header("NEW LOOT OBJECT")]
    public LootItems.Loot lootname;
    public Sprite lootsprite;
    public LootItems.LootType loottype;
    public LootItems.LootRarity lootrarity;
    

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

    LootItems getLootByName (LootItems.Loot lootname)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_name == lootname)
            {
                tempList.Add(item);
            }
        }
        return tempList[0];
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
        if (getLootByName(name).loot_type == LootItems.LootType.Relic)
        {
            List<LootItems> tempList = new List<LootItems>(lootList);
            for (int i = 0; i < lootList.Count; i++)
            {
                if (lootList[i].loot_name == name)
                {
                    lootList.RemoveAt(i);
                }
            }
        }
    }

    // Using a Coroutine incase we want an effect that is time limited - Use 'yield return new WaitForSeconds()"
    // Using if ststements instead of Switch due to yield/break issues.
    public IEnumerator LootEffect(LootItems.Loot loot)
    {
        RemoveFromPool(loot);
        if (loot == LootItems.Loot.QuiverNormal)
        {
            playerInventory.normalArrowCount = playerStats.PI_MaxNormalArrows;
        }
        else if (loot == LootItems.Loot.QuiverFire)
        {
            playerInventory.fireArrowCount = playerStats.PI_MaxFireArrows;
        }
        else if (loot == LootItems.Loot.QuiverIce)
        {
            playerInventory.iceArrowCount = playerStats.PI_MaxIceArrows;
        }
        else if (loot == LootItems.Loot.QuiverExplosive)
        {
            playerInventory.explosiveArrowCount = playerStats.PI_MaxExplosiveArrows;
        }
        else if (loot == LootItems.Loot.QuiverSpeed)
        {
            playerInventory.speedArrowCount = playerStats.PI_MaxSpeedArrows;
        }
        else if(loot == LootItems.Loot.MaxAmmo)
        {
            playerInventory.MaxAmmo();
        }
        else if (loot == LootItems.Loot.PlayerBaseSpeedRelic)
        {
            playerStats.PM_BaseSpeed += 2;
        }
        else if (loot == LootItems.Loot.PlayerDrawSpeedRelic)
        {
            playerStats.PM_DrawSpeed += 2;
        }
        else if (loot == LootItems.Loot.NoMovementPenaltyRelic)
        {
            getLootByName(LootItems.Loot.NoMovementPenaltyRelic).isActive = true;
        }
        else if (loot == LootItems.Loot.TankRelic)
        {
            playerStats.playerHealth *= 2;
            playerStats.PM_BaseSpeed /= 2;
            playerStats.PM_DrawSpeed /= 2;
        }
        else if (loot == LootItems.Loot.ScoutRelic)
        {
            playerStats.playerHealth /= 2;
            playerStats.PM_BaseSpeed *= 2;
            playerStats.PM_DrawSpeed *= 2;
        }
        yield break;
    }

    public void UpdateRelics()
    {
        if (getLootByName(LootItems.Loot.NoMovementPenaltyRelic).isActive)
        {
            playerStats.PM_DrawSpeed = playerStats.PM_BaseSpeed;
        }
    }
}
