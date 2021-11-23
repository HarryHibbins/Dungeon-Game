using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private GameObject gameManager;
    private PlayerStats playerStats;

    public ArrowTypes.Arrows equippedArrow;

    public int normalArrowCount;
    public int fireArrowCount;
    public  int iceArrowCount;
    public  int explosiveArrowCount;
    public int speedArrowCount;

    public bool holdingTorch = false;

    int arrowCheck;
    int maxArrowCheck;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerStats = gameManager.GetComponent<PlayerStats>();

        MaxAmmo();
    }

    public void MaxAmmo()
    {
        normalArrowCount = playerStats.PI_MaxNormalArrows;
        fireArrowCount = playerStats.PI_MaxFireArrows;
        iceArrowCount = playerStats.PI_MaxIceArrows;
        explosiveArrowCount = playerStats.PI_MaxExplosiveArrows;
        speedArrowCount = playerStats.PI_MaxSpeedArrows;
    }

    //Checks to see if there is ammo left of the selected type
    public int getSelectedArrowAmmo()
    {

        switch (equippedArrow)
        {
            case ArrowTypes.Arrows.Normal:
            {
                return arrowCheck = normalArrowCount;
            }
            case ArrowTypes.Arrows.Fire:
            {
                return arrowCheck = fireArrowCount;
            }
            case ArrowTypes.Arrows.Ice:
            {
                return arrowCheck = iceArrowCount;
            }
            case ArrowTypes.Arrows.Explosive:
            {
                return arrowCheck = explosiveArrowCount;
            }
            case ArrowTypes.Arrows.Speed:
            {
                return arrowCheck = speedArrowCount;
            }
        }

        return arrowCheck;


    }
    
    public int getMaxSelectedArrowAmmo()
    {

        switch (equippedArrow)
        {
            case ArrowTypes.Arrows.Normal:
            {
                return maxArrowCheck = playerStats.PI_MaxNormalArrows;
            }
            case ArrowTypes.Arrows.Fire:
            {
                return maxArrowCheck = playerStats.PI_MaxFireArrows;
            }
            case ArrowTypes.Arrows.Ice:
            {
                return maxArrowCheck = playerStats.PI_MaxIceArrows;
            }
            case ArrowTypes.Arrows.Explosive:
            {
                return maxArrowCheck = playerStats.PI_MaxExplosiveArrows;
            }
            case ArrowTypes.Arrows.Speed:
            {
                return maxArrowCheck = playerStats.PI_MaxSpeedArrows;
            }
        }

        return maxArrowCheck;


    }
}
