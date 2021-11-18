using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerController playerController;
    private PlayerInventory playerInventory;
    
    private bool draw;
    private bool fire;
    
    private GameObject firePointObj;
    private Transform firePoint;

    public Arrow arrow;

    public float drawBack;
    [SerializeField] private float maxDrawBack;

    //Controls the type of arrow that the bow will fire
    //This gets updated by playerInvetory when the bow is drawn
    [HideInInspector] public ArrowTypes.Arrows currentArrow;
    
    
    private bool arrowSelected = false;
    private int ammoCheck;

    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerController = playerObj.GetComponent<PlayerController>();
        playerInventory = playerObj.GetComponent<PlayerInventory>();
        firePointObj = GameObject.FindWithTag("Fire Point");
        firePoint = firePointObj.transform;
        

        //Minimum drawback value so it doesnt slow down the arrow
        drawBack = 1;
    }

    void Update()
    {
        //Hold left click to draw bow
        if (Input.GetButtonDown("Fire1"))
        {
            if (getSelectedArrowAmmo() >= 1)
            {
                draw = true;
                Debug.Log("Drawing " + currentArrow + " arrow "+  getSelectedArrowAmmo() + " In Quiver");

            }
        }
        //Release left click to fire arrow
        else if (Input.GetButtonUp("Fire1") && draw)
        {
            draw = false;
            fire = true;
        }
        //Cap the draw back to a value that can be changed depending on bow
        if (draw && drawBack < maxDrawBack)
        {
            drawBack += Time.deltaTime;
            playerController.currentMoveSpeed = playerController.drawBowMoveSpeed;
        }

        if (fire)
        {
            //Create arrow
            Arrow newArrow = Instantiate(arrow, firePoint.position, firePoint.rotation) ;
            
            //Reset Values ready for next shot
            fire = false;
            drawBack = 1;
            playerController.currentMoveSpeed = playerController.normalMoveSpeed;

            //Minus the correct ammo count
            switch (currentArrow)
            {
                case ArrowTypes.Arrows.Normal:
                {
                    playerInventory.normalArrowCount--;
                    break;
                }
                case ArrowTypes.Arrows.Fire:
                {
                    playerInventory.fireArrowCount--;
                    break;
                }
                case ArrowTypes.Arrows.Ice:
                {
                    playerInventory.iceArrowCount--;
                    break;
                }
                case ArrowTypes.Arrows.Explosive:
                {
                    playerInventory.explosiveArrowCount--;
                    break;
                }
                case ArrowTypes.Arrows.Speed:
                {
                    playerInventory.speedArrowCount--;
                    break;
                }
            }
            
            
            
            arrowSelected = false;

        }
    }

    //Checks to see if there is ammo left of the selected type
    int getSelectedArrowAmmo()
    {

        currentArrow = playerInventory.equippedArrow;

        switch (currentArrow)
        {
            case ArrowTypes.Arrows.Normal:
            {
                return ammoCheck = playerInventory.normalArrowCount;
            }
            case ArrowTypes.Arrows.Fire:
            {
                return ammoCheck = playerInventory.fireArrowCount;
            }
            case ArrowTypes.Arrows.Ice:
            {
                return ammoCheck = playerInventory.iceArrowCount;
            }
            case ArrowTypes.Arrows.Explosive:
            {
                return ammoCheck = playerInventory.explosiveArrowCount;
            }
            case ArrowTypes.Arrows.Speed:
            {
                return ammoCheck = playerInventory.speedArrowCount;
            }
        }

        return ammoCheck;


    }
}
