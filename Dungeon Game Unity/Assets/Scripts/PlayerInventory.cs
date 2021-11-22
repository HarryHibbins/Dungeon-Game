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
        normalArrowCount = playerStats.playerinventory_maxNormalArrows;
        fireArrowCount = playerStats.playerinventory_maxFireArrows;
        iceArrowCount = playerStats.playerinventory_maxIceArrows;
        explosiveArrowCount = playerStats.playerinventory_maxExplosiveArrows;
        speedArrowCount = playerStats.playerinventory_maxSpeedArrows;
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
                return maxArrowCheck = playerStats.playerinventory_maxNormalArrows;
            }
            case ArrowTypes.Arrows.Fire:
            {
                return maxArrowCheck = playerStats.playerinventory_maxFireArrows;
            }
            case ArrowTypes.Arrows.Ice:
            {
                return maxArrowCheck = playerStats.playerinventory_maxIceArrows;
            }
            case ArrowTypes.Arrows.Explosive:
            {
                return maxArrowCheck = playerStats.playerinventory_maxExplosiveArrows;
            }
            case ArrowTypes.Arrows.Speed:
            {
                return maxArrowCheck = playerStats.playerinventory_maxSpeedArrows;
            }
        }

        return maxArrowCheck;


    }
}
