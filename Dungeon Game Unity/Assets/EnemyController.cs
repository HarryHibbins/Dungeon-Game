using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float health;
    private bool hit;
    private float DamageDealt;
    void Start()
    {
        
    }

    void Update()
    {
        if (hit)
        {
            //Deal damage to the enemy if hit
            Debug.Log("Enemy hit: " + DamageDealt + "dmg");
            health -= DamageDealt;
            hit = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If enemy is hit by the arrow round the damage value from the arrow worked out with the multiplier 
        if (other.CompareTag("Arrow"))
        {
            //Get the arrow that hit the enemy
            Arrow arrow = other.GetComponent<Arrow>();
            hit = true;
            DamageDealt = Mathf.Round(arrow.actualDamage);
            //Stop the arrow 
            arrow.actualspeed = 0;
            arrow.gravity = 0;
            arrow.GetComponent<Transform>().parent = transform; 


        }
    }
    
    
    

}
