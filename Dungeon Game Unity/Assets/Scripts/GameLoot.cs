using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameLoot : MonoBehaviour
{
    //public GameObject lootPrefab;

    private PlayerStats playerStats;
    private GameObject player;
    private PlayerInventory playerInventory;
    private HeartsUI heartsUI;
    private PlayerHealth playerHealth;

    public List<LootItems> lootList;
    [Space(3)]
    [Header("NEW LOOT OBJECT")]
    public LootItems.Loot lootname;
    [Multiline]
    public string lootdesc;
    public GameObject lootprefab;
    public Sprite lootsprite;
    public LootItems.LootType loottype;
    public LootItems.LootRarity lootrarity;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>();
        heartsUI = GameObject.FindGameObjectWithTag("HeartsUI").GetComponent<HeartsUI>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        UpdateRelics();

        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnLoot(new Vector3(0, 1, 0), getLootByName(LootItems.Loot.QuiverExplosive));
            SpawnLoot(new Vector3(2, 1, 0), getLootByName(LootItems.Loot.MaxAmmo));
        }
    }

    public void NewLootItem(LootItems.Loot lootname, string lootdesc, GameObject lootprefab, Sprite lootsprite, LootItems.LootType loottype, LootItems.LootRarity lootrarity)
    {
        LootItems temp = new LootItems(lootname, lootdesc, lootprefab, lootsprite, loottype, lootrarity);
        lootList.Add(temp);
    }

    public LootItems getLootByName (LootItems.Loot lootname)
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

    public LootItems getLootByRarity(LootItems.LootRarity rarity)
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

    public LootItems getLootByType(LootItems.LootType type)
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

    public LootItems getLootByTypeAndRarity(LootItems.LootType type, LootItems.LootRarity rarity)
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

    public void SpawnLoot(Vector3 position, LootItems name)
    {
        lootprefab = name.loot_prefab;
        lootprefab.transform.localScale = new Vector3(1, 1, 1);
        GameObject loot = Instantiate(lootprefab, position, Quaternion.identity);
        loot.AddComponent<LootPickUp>();
        loot.AddComponent<LootRotator>();
        loot.AddComponent<BoxCollider>().isTrigger = true;
        LootPickUp script = loot.GetComponent<LootPickUp>();
        script.LootName = name.loot_name;
    }

    public void RemoveFromPool(LootItems.Loot name)
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
            heartsUI.UpdateHearts(playerStats.playerHearts);
            playerStats.PM_BaseSpeed /= 2;
            playerStats.PM_DrawSpeed /= 2;
        }
        else if (loot == LootItems.Loot.ScoutRelic)
        {
            heartsUI.UpdateHearts(-(playerStats.playerHearts / 2));
            playerStats.PM_BaseSpeed *= 2;
            playerStats.PM_DrawSpeed *= 2;
        }
        else if (loot == LootItems.Loot.ThornsRelic)
        {
            getLootByName(LootItems.Loot.ThornsRelic).isActive = true;
        }
        else if (loot == LootItems.Loot.InfinityRelic)
        {
            getLootByName(LootItems.Loot.InfinityRelic).isActive = true;
        }
        else if (loot == LootItems.Loot.BiggerBagNormal)
        {
            playerStats.PI_MaxNormalArrows += 10;
        }
        else if (loot == LootItems.Loot.BiggerBagFire)
        {
            playerStats.PI_MaxFireArrows += 10;
        }
        else if (loot == LootItems.Loot.BiggerBagIce)
        {
            playerStats.PI_MaxIceArrows += 10;
        }
        else if (loot == LootItems.Loot.BiggerBagExplosive)
        {
            playerStats.PI_MaxExplosiveArrows += 10;
        }
        else if (loot == LootItems.Loot.BiggerBagSpeed)
        {
            playerStats.PI_MaxSpeedArrows += 10;
        }
        else if (loot == LootItems.Loot.BiggerBagAll)
        {
            playerStats.updateAllMaxArrows(10);
        }
        else if (loot == LootItems.Loot.CauterizeRelic)
        {
            playerStats.ArrowEffects_BurnDamage += 5;
        }
        else if (loot == LootItems.Loot.PyromaniacRelic)
        {
            playerStats.ArrowEffects_BurnTime += 5;
        }
        else if (loot == LootItems.Loot.BarbedTipRelic)
        {
            playerStats.ArrowEffects_BleedDamage += 5;
        }
        else if (loot == LootItems.Loot.DeepCutsRelic)
        {
            playerStats.ArrowEffects_BleedTime += 5;
        }
        else if (loot == LootItems.Loot.SubZeroRelic)
        {
            playerStats.ArrowEffects_SlowTime += 5;
        }
        else if (loot == LootItems.Loot.SharperTipsRelic)
        {
            playerStats.ArrowEffects_BleedChance -= 1;
        }
        else if (loot == LootItems.Loot.HealthPotion)
        {
            playerHealth.Heal(playerHealth.GetMaxHealth());
        }
        else if (loot == LootItems.Loot.DungeonFood)
        {
            playerHealth.Heal(4);
        }
        else if (loot == LootItems.Loot.AncientHelm)
        {
            //Apply chance to ignore damage
        }
        yield break;
    }

    public void UpdateRelics()
    {
        if (getLootByName(LootItems.Loot.NoMovementPenaltyRelic).isActive)
        {
            playerStats.PM_DrawSpeed = playerStats.PM_BaseSpeed;
        }
        if (getLootByName(LootItems.Loot.ThornsRelic).isActive)
        {
            //Do thorns damage once enemies are setup.
        }
    }
}
