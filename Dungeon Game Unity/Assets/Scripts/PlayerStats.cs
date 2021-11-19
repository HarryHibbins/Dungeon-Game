using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement Stats")]
    public float playermovement_BaseSpeed;
    public float playermovement_DrawSpeed;

    [Header("Arrow Stats")]
    public int playerinventory_BaseArrowCount;
    public int playerinventory_maxNormalArrows;
    public int playerinventory_maxFireArrows;
    public int playerinventory_maxIceArrows;
    public int playerinventory_maxExplosiveArrows;
    public int playerinventory_maxSpeedArrows;
    [Space(5)]
    public int arrowdamage_base;
    public int arrowdamage_normal;
    public int arrowdamage_fire;
    public int arrowdamage_ice;
    public int arrowdamage_explosive;
    public int arrowdamage_speed;
    [Space(5)]
    public int arrowspeed_base;
    public int arrowspeed_normal;
    public int arrowspeed_fire;
    public int arrowspeed_ice;
    public int arrowspeed_explosive;
    public int arrowspeed_speed;

    private void Start()
    {
        playerinventory_maxNormalArrows += playerinventory_BaseArrowCount;
        playerinventory_maxFireArrows += playerinventory_BaseArrowCount;
        playerinventory_maxIceArrows += playerinventory_BaseArrowCount;
        playerinventory_maxExplosiveArrows += playerinventory_BaseArrowCount;
        playerinventory_maxSpeedArrows += playerinventory_BaseArrowCount;
    }
}
