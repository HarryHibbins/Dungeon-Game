using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public enum AttackType
    {
        orb,
        melee
    }

    public AttackType attackType;
    
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange, attackRange;
    
    private PlayerHealth playerHealth;
    
    private Animator anim;

    public Orb orb; 
    private bool inMeleeAttack;

    private bool inSwordHitBox;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        switch (attackType)
        {
            case AttackType.orb:
                foreach (Transform child in transform)
                {
                    if (child.name == ("Fire Point"))
                    {
                        Orb newOrb = Instantiate(orb, child.position, child.rotation) ;
                        newOrb.transform.parent = child.transform;
                    }
                }
                break;
            case AttackType.melee:
                anim.SetTrigger("Sword Attack");



                break;
        }
    }
    

    private void Update()
    {
        if (attackType == AttackType.melee)
        {
         
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                inMeleeAttack = true;
            }
            else
            {
                inMeleeAttack = false;

            }

            if (inMeleeAttack && inSwordHitBox)
            {
                inSwordHitBox = false;
                inMeleeAttack = false;
                Debug.Log("Player hit by sword");
                playerHealth.Damage(1);
            }   
        }
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inSwordHitBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inSwordHitBox = false;
        }
    }
}
