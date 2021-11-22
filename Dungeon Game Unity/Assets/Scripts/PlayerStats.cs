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
    public int PI_BaseArrowCount;
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

    private void Start()
    {
        PI_MaxNormalArrows += PI_BaseArrowCount;
        PI_MaxFireArrows += PI_BaseArrowCount;
        PI_MaxIceArrows += PI_BaseArrowCount;
        PI_MaxExplosiveArrows += PI_BaseArrowCount;
        PI_MaxSpeedArrows += PI_BaseArrowCount;
    }
}
