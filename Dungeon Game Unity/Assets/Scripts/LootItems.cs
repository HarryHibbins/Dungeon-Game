using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItems : MonoBehaviour
{
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
}
