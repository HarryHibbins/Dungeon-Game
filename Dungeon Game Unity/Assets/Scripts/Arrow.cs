using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float drawBackMultiplier; 
    
    public float actualspeed;
    public float actualDamage;
    private float arrowTypeDamageBonus;
    private float arrowTypeSpeedBonus;
    public float gravity;
    private bool hasLanded = false;
    private int canBePickedUp = 0;
    
    private GameObject bowObj;
    Bow bow;


    [SerializeField] float arrowDespawnTimer;
    public ArrowTypes.Arrows selectedArrow;
    
    private GameObject playerObj;
    private PlayerInventory playerInventory;

    private GameObject gameManager;
    private PlayerStats playerStats;
    private bool hasBeanFried;

    public bool move;

    private void Awake()
    {
        move = false;
        canBePickedUp = UnityEngine.Random.Range(0, 2);
        Debug.Log(canBePickedUp);

        bowObj = GameObject.FindWithTag("Bow");
        bow = bowObj.GetComponent<Bow>();
        
        playerObj = GameObject.FindWithTag("Player");
        playerInventory = playerObj.GetComponent<PlayerInventory>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerStats = gameManager.GetComponent<PlayerStats>();
        
        //The selected arrow is determined through the player inventory
        selectedArrow = playerInventory.equippedArrow;
        

        switch (selectedArrow)
        {
            case ArrowTypes.Arrows.Normal:
            {
                arrowTypeDamageBonus = playerStats.ArrowDamage_Normal;
                arrowTypeSpeedBonus = playerStats.ArrowSpeed_Normal;
                break;
            }
            case ArrowTypes.Arrows.Fire:
            {
                arrowTypeDamageBonus = playerStats.ArrowDamage_Fire;
                arrowTypeSpeedBonus = playerStats.ArrowSpeed_Fire;
                break;
            }
            case ArrowTypes.Arrows.Ice:
            {
                arrowTypeDamageBonus = playerStats.ArrowDamage_Ice;
                arrowTypeSpeedBonus = playerStats.ArrowSpeed_Ice;
                break;
            }
            case ArrowTypes.Arrows.Explosive:
            {
                arrowTypeDamageBonus = playerStats.ArrowDamage_Explosive;
                //Maybe go slower?
                arrowTypeSpeedBonus = playerStats.ArrowSpeed_Explosive;
                break;
            }
            case ArrowTypes.Arrows.Speed:
            {
                arrowTypeDamageBonus = playerStats.ArrowDamage_Speed;
                arrowTypeSpeedBonus = playerStats.ArrowSpeed_Speed;
                break;
            }
        }
        
   


    }

    public void ApplyDrawBackMultiplier()
    {
        //Modifiers applied depending on arrow type
        drawBackMultiplier = bow.drawBack;

        actualDamage = (playerStats.ArrowDamage_Base * drawBackMultiplier) + arrowTypeDamageBonus;
        actualspeed = (playerStats.ArrowSpeed_Base * drawBackMultiplier) + arrowTypeSpeedBonus;

    }

    void Update()
    {

        if (move)
        {
            
            //Apply Forward Translation
            transform.Translate((Vector3.forward * actualspeed) * Time.deltaTime);
            //Apply Downward Translation for gravity
            transform.Translate((Vector3.down * gravity) * Time.deltaTime);
            hasBeanFried = true;

        }
        
        arrowDespawnTimer -= Time.deltaTime;
        if(arrowDespawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }





    private void OnTriggerEnter(Collider other)
    {
        //If enemy is hit by the arrow round the damage value from the arrow worked out with the multiplier 
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment") && hasBeanFried)
        {
            Debug.Log("HIT Environment");
            move = false;
            hasLanded = true;
        }
        //If the arrow has hit the environment, and the player walks over it, 50% chance to pick it up, destroy gameobject. 
        if (other.gameObject.tag == "PlayerPickUp" && hasLanded )
        {
            if (canBePickedUp == 1)
            {
                switch (selectedArrow)
                {
                    case ArrowTypes.Arrows.Normal:
                        {
                            playerInventory.normalArrowCount++;
                            break;
                        }
                    case ArrowTypes.Arrows.Fire:
                        {
                            playerInventory.fireArrowCount++;
                            break;
                        }
                    case ArrowTypes.Arrows.Ice:
                        {
                            playerInventory.iceArrowCount++;
                            break;
                        }
                    case ArrowTypes.Arrows.Explosive:
                        {
                            //Probably shouldn't be able to pick up shot explosive arrows
                            break;
                        }
                    case ArrowTypes.Arrows.Speed:
                        {
                            playerInventory.speedArrowCount++;
                            break;
                        }
                }
            }
            Debug.Log("Pick up Arrow");
            Destroy(this.gameObject);
        }
    }
}
