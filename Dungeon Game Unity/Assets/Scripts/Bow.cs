using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerController playerController;
    private PlayerInventory playerInventory;
    private GameObject gameManager;
    private PlayerStats playerStats;

    public bool draw;
    private bool fire;
    
    private GameObject firePointObj;
    private Transform firePoint;

    public Arrow arrow;

    public float drawBack;
    [SerializeField] private float maxDrawBack;

   
    private int ammoCheck;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        firePointObj = GameObject.FindWithTag("Fire Point");
        firePoint = firePointObj.transform;
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();

        //Minimum drawback value so it doesnt slow down the arrow
        drawBack = 1;
    }

    void Update()
    {
        //Hold left click to draw bow
        if (Input.GetButtonDown("Fire1"))
        {
            if (playerInventory.getSelectedArrowAmmo() >= 1)
            {
                draw = true;
                //Create arrow
                Arrow newArrow = Instantiate(arrow, firePoint.position, firePoint.rotation) ;
                newArrow.transform.parent = firePoint;
                Debug.Log("Drawing " + playerInventory.equippedArrow + " arrow "+  playerInventory.getSelectedArrowAmmo() + " In Quiver");

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
            playerController.currentMoveSpeed = playerStats.PM_DrawSpeed;
        }

        if (fire)
        {
            foreach(Transform child in firePoint)
            {
                if (child.gameObject.tag == "Arrow")
                {
                    child.GetComponent<Arrow>().move = true;
                    child.GetComponent<Arrow>().ApplyDrawBackMultiplier();
                    child.transform.parent = null;

                }
            }
            //Reset Values ready for next shot
            fire = false;
            drawBack = 1;
            playerController.currentMoveSpeed = playerStats.PM_BaseSpeed;

            //Minus the correct ammo count
            switch (playerInventory.equippedArrow)
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
            
            
            

        }
    }

   
}
