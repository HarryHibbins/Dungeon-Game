using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float drawBackMultiplier; 
    
    [SerializeField] float baseSpeed;
    public float actualspeed;
    [SerializeField] private float baseDamage;
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
                arrowTypeDamageBonus = playerStats.arrowdamage_normal;
                arrowTypeSpeedBonus = playerStats.arrowspeed_normal;
                break;
            }
            case ArrowTypes.Arrows.Fire:
            {
                arrowTypeDamageBonus = playerStats.arrowdamage_fire;
                arrowTypeSpeedBonus = playerStats.arrowspeed_fire;
                break;
            }
            case ArrowTypes.Arrows.Ice:
            {
                arrowTypeDamageBonus = playerStats.arrowdamage_ice;
                arrowTypeSpeedBonus = playerStats.arrowspeed_ice;
                break;
            }
            case ArrowTypes.Arrows.Explosive:
            {
                arrowTypeDamageBonus = playerStats.arrowdamage_explosive;
                //Maybe go slower?
                arrowTypeSpeedBonus = playerStats.arrowspeed_explosive;
                break;
            }
            case ArrowTypes.Arrows.Speed:
            {
                arrowTypeDamageBonus = playerStats.arrowdamage_speed;
                arrowTypeSpeedBonus = playerStats.arrowspeed_speed;
                break;
            }
        }
        
   


    }

    public void ApplyDrawBackMultiplier()
    {
        //Modifiers applied depending on arrow type
        drawBackMultiplier = bow.drawBack;

        actualDamage = (baseDamage * drawBackMultiplier) + arrowTypeDamageBonus;
        actualspeed = (baseSpeed * drawBackMultiplier) + arrowTypeSpeedBonus;

    }

    void Update()
    {

        if (move)
        {
            
            //Apply Forward Translation
            transform.Translate((Vector3.forward * actualspeed) * Time.deltaTime);
            //Apply Downward Translation for gravity
            transform.Translate((Vector3.down * gravity) * Time.deltaTime);
  
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Debug.Log("HIT Environment");
            move = false;
            hasLanded = true;
        }
        //If the arrow has hit the environment, and the player walks over it, 50% chance to pick it up, destroy gameobject. 
        if (other.gameObject.tag == "PlayerPickUp" && hasLanded)
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
