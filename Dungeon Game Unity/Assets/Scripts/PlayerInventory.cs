using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public ArrowTypes.Arrows equippedArrow;

    public int maxNormalArrows;
    public int normalArrowCount;
    
    public int maxFireArrows;
    public int fireArrowCount;
    
    public int maxIceArrows;
    public  int iceArrowCount;

    public int maxExplosiveArrows;
    public  int explosiveArrowCount;

    public int maxSpeedArrows;
    public int speedArrowCount;


    int arrowCheck;
    int maxArrowCheck;
    void Start()
    {
        normalArrowCount = 20;
        fireArrowCount = 5;
        iceArrowCount = 5;
        explosiveArrowCount = 5;
        speedArrowCount = 10;


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
                return maxArrowCheck = maxNormalArrows;
            }
            case ArrowTypes.Arrows.Fire:
            {
                return maxArrowCheck = maxFireArrows;
            }
            case ArrowTypes.Arrows.Ice:
            {
                return maxArrowCheck = maxIceArrows;
            }
            case ArrowTypes.Arrows.Explosive:
            {
                return maxArrowCheck = maxExplosiveArrows;
            }
            case ArrowTypes.Arrows.Speed:
            {
                return maxArrowCheck = maxSpeedArrows;
            }
        }

        return maxArrowCheck;


    }
}
