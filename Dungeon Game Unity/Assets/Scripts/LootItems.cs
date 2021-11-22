using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItems
{
    [HideInInspector]
    public string element_name;

    public Loot loot_name;
    public Sprite loot_sprite;
    public LootRarity loot_rarity;
    public LootType loot_type;
    public bool isActive;

    public enum LootRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
    }

    public enum LootType
    {
        Consumable,
        Relic,
    }

    public enum Loot
    {
        QuiverNormal,               // Refills Normal Ammo
        QuiverFire,                 // Refills Fire Ammo
        QuiverIce,                  // Refills Ice Ammo
        QuiverExplosive,            // Refills Explosive Ammo
        QuiverSpeed,                // Refills Speed Ammo
        MaxAmmo,                    // Refills all Ammo
        PlayerBaseSpeedRelic,       // Adds 2 to base player movement speed
        PlayerDrawSpeedRelic,       // Adds 2 to bow draw movement speed
        NoMovementPenaltyRelic,     // Removes speed penalty for drawing bow
        TankRelic,                  // Double Health, Half Speed
        ScoutRelic,                 // Half Health, Double Speed
    }

    public LootItems(Loot lootname, Sprite lootsprite, LootType loottype ,LootRarity lootrarity)
    {
        element_name = lootname.ToString();
        loot_name = lootname;
        loot_sprite = lootsprite;
        loot_rarity = lootrarity;
        loot_type = loottype;
    }

    public LootItems(Loot lootname, LootType loottype, LootRarity lootrarity)
    {
        element_name = lootname.ToString();
        loot_name = lootname;
        loot_rarity = lootrarity;
        loot_type = loottype;
    }

}

