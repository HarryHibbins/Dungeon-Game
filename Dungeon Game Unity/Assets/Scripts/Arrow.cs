using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private float drawBackMultiplier; 
    
    [SerializeField] float baseSpeed;
    public float actualspeed;
    [SerializeField] private float baseDamage;
    public float actualDamage;
    private float arrowTypeDamageBonus;
    private float arrowTypeSpeedBonus;
    public float gravity;
    
    private GameObject bowObj;
    Bow bow;


    [SerializeField] float arrowDespawnTimer;


    private ArrowTypes.Arrows selectedArrow;
  

    private void Awake()
    {
        bowObj = GameObject.FindWithTag("Bow");
        bow = bowObj.GetComponent<Bow>();
        drawBackMultiplier = bow.drawBack;

        
        //The selected arrow is determined through the bow
        selectedArrow = bow.currentArrow;

        switch (selectedArrow)
        {
            case ArrowTypes.Arrows.Normal:
            {
                arrowTypeDamageBonus = 0;
                break;
            }
            case ArrowTypes.Arrows.Fire:
            {
                arrowTypeDamageBonus = 5;

                break;
            }
            case ArrowTypes.Arrows.Ice:
            {
                arrowTypeDamageBonus = 5;

                break;
            }
            case ArrowTypes.Arrows.Explosive:
            {
                arrowTypeDamageBonus = 5;
                //Maybe go slower?
                arrowTypeSpeedBonus = -5;


                break;
            }
            case ArrowTypes.Arrows.Speed:
            {
                arrowTypeDamageBonus = 0;
                arrowTypeSpeedBonus = 10;

                break;
            }
        }
        
        //Modifiers applied depending on arrow type
        actualDamage = (baseDamage * drawBackMultiplier) + arrowTypeDamageBonus;
        actualspeed = (baseSpeed * drawBackMultiplier) + arrowTypeSpeedBonus;


    }

    void Update()
    {
        //Apply Forward Translation
        transform.Translate((Vector3.forward * actualspeed) * Time.deltaTime);
        //Apply Downward Translation for gravity
        transform.Translate((Vector3.down * gravity) * Time.deltaTime);
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
            actualspeed = 0;
            gravity = 0;
        }
    }
}
