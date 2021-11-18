using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public ArrowTypes.Arrows equippedArrow;

    public int normalArrowCount;
    public int fireArrowCount;
    public  int iceArrowCount;
    public  int explosiveArrowCount; 
    public int speedArrowCount;
    void Start()
    {
        normalArrowCount = 20;
        fireArrowCount = 5;
        iceArrowCount = 5;
        explosiveArrowCount = 5;
        speedArrowCount = 10;



    
    }

}
