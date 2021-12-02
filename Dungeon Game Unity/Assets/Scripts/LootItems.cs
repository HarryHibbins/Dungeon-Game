using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItems
{
    [HideInInspector]
    public string element_name;

    public Loot loot_name;
    public GameObject loot_prefab;
    public Sprite loot_sprite;
    public LootRarity loot_rarity;
    public LootType loot_type;
    public string loot_description;
    public bool isActive;
    public bool isCollected;

    public enum LootRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Boss,
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
        ThornsRelic,                // Deals 10% damage to enemy when hit. 
        InfinityRelic,              // Chance to not consume ammo when firing.
        BiggerBagNormal,            // Increase max normal arrows
        BiggerBagFire,              // Increase max fire arrows
        BiggerBagIce,               // Increase max ice arrows
        BiggerBagExplosive,         // Increase max explosive arrows
        BiggerBagSpeed,             // Increase max speed arrows
        BiggerBagAll,               // Increase max arrows for all types
        CauterizeRelic,             // Increase damage dealt by burn
        PyromaniacRelic,            // Increase burn time
        BarbedTipRelic,             // Increase damage dealt by bleed
        DeepCutsRelic,              // Increase bleed time
        SubZeroRelic,               // Increase slow time
        SharperTipsRelic,           // Increase chance of applying bleed
        HealthPotion,               // Restore health to full
        DungeonFood,                // Restore a full heart
        AncientHelm,                // Chance to ignore damage
        WarbannerRelic,             // After standing still for 3 seconds, gain 50% damage
        RestorePotion,              // Gain health regen for 10 seconds (1 heart per 5 seconds)
        ArmourPiercingArrows,       // Gain 20% damage against bosses
        PreservedInsectRelic,       // Bosses start with 20% less health
        ExplorerRelic,              // Restore 1 heart for every 10 rooms visited
        AssassinRelic,              // Lose all burn and slow effect, gain 100% chance to bleed
        BetterBowRelic,             // Gain 10% chance to crit (double damage)
        LizardTailRelic,            // After taking lethal damage, heal to 50% hp (once per run)
        LuckyDiceRelic,             // Increase drop luck
        AncientLamp,                // Gain a small permanent light source
        MysticFeather,              // All arrow types gain +5 damage
        CartographerRelic,          // Increase chance of finding a treasure room (have to implement)
    }

    public LootItems(Loot lootname, string lootdesc, GameObject loot_prefab, Sprite lootsprite, LootType loottype , LootRarity lootrarity)
    {
        element_name = lootname.ToString();
        loot_description = lootdesc;
        loot_name = lootname;
        loot_sprite = lootsprite;
        loot_rarity = lootrarity;
        loot_type = loottype;
    }

    public LootItems(Loot lootname, string lootdesc, LootType loottype, LootRarity lootrarity)
    {
        element_name = lootname.ToString();
        loot_description = lootdesc;
        loot_name = lootname;
        loot_rarity = lootrarity;
        loot_type = loottype;
    }

}

