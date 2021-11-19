using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItems : MonoBehaviour
{
    public static List<LootItems> lootList = new List<LootItems>();

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
        PlayerBaseSpeedRelic,       // Adds 2 to base player movement speed
        PlayerDrawSpeedRelic,       // Adds 2 to bow draw movement speed
        NoMovementPenaltyRelic,     // Removes speed penalty for drawing bow
    }

    public LootItems(Loot lootname, Sprite lootsprite, LootType loottype ,LootRarity lootrarity)
    {
        loot_name = lootname;
        loot_sprite = lootsprite;
        loot_rarity = lootrarity;
        loot_type = loottype;

        lootList.Add(this);
    }

    public LootItems(Loot lootname, LootType loottype, LootRarity lootrarity)
    {
        loot_name = lootname;
        loot_rarity = lootrarity;
        loot_type = loottype;

        lootList.Add(this);
    }

}

