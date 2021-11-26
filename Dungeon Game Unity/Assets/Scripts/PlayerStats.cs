using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Stats")]
    public float playerHealth;

    [Header("Movement Stats")]
    public float PM_BaseSpeed;
    public float PM_DrawSpeed;

    [Header("Arrow Stats")]
    public int PI_MaxNormalArrows;
    public int PI_MaxFireArrows; 
    public int PI_MaxIceArrows; 
    public int PI_MaxExplosiveArrows; 
    public int PI_MaxSpeedArrows;
    [Space(5)]
    public int ArrowDamage_Base; 
    public int ArrowDamage_Normal; 
    public int ArrowDamage_Fire; 
    public int ArrowDamage_Ice; 
    public int ArrowDamage_Explosive; 
    public int ArrowDamage_Speed; 
    [Space(5)]
    public int ArrowSpeed_Base; 
    public int ArrowSpeed_Normal; 
    public int ArrowSpeed_Fire; 
    public int ArrowSpeed_Ice; 
    public int ArrowSpeed_Explosive; 
    public int ArrowSpeed_Speed;

    [Header("Arrow Effects")]   
    public int ArrowEffects_BleedChance;        // 2 = 50%, 3 = 33% etc
    public float ArrowEffects_BleedDamage;
    public float ArrowEffects_BurnDamage;
    public int ArrowEffects_BleedTime;
    public int ArrowEffects_BurnTime;
    public int ArrowEffects_SlowTime;

    public void updateAllMaxArrows(int amount)
    {
        PI_MaxNormalArrows += amount;
        PI_MaxFireArrows += amount;
        PI_MaxIceArrows += amount;
        PI_MaxExplosiveArrows += amount;
        PI_MaxSpeedArrows += amount;
}
}
