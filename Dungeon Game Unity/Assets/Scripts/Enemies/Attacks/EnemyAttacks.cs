using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public enum AttackType
    {
        orb,
        threeorb,
        melee
    }

    public AttackType attackType;
    
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange, attackRange;
    
    private PlayerHealth playerHealth;
    
    public Animator anim;

    public Orb orb; 
    private bool inMeleeAttack;

    private bool inSwordHitBox;
    public GameObject objectForMeleeAnim;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        if (attackType == AttackType.melee)
        {
            anim = objectForMeleeAnim.GetComponent<Animator>();
        }
        else
        {
            anim = GetComponent<Animator>();
        }
        

    }

    public void Attack()
    {
        switch (attackType)
        {
            case AttackType.orb:
                anim.SetTrigger("Shoot1");
                foreach (Transform child in transform)
                {
                    if (child.name == ("Fire Point"))
                    {
                        Orb newOrb = Instantiate(orb, child.position, child.rotation) ;
                        newOrb.transform.parent = child.transform;
                        FindObjectOfType<AudioManager>().Play("Enemyattack");
                    }
                }
                break;
            case AttackType.threeorb:
                anim.SetTrigger("Shoot3");
                foreach (Transform child in transform)
                {
                    if (child.name == ("Fire Point"))
                    {
                        Orb newOrb = Instantiate(orb, child.position, child.rotation) ;
                        newOrb.transform.parent = child.transform;
                        FindObjectOfType<AudioManager>().Play("Enemyattack");
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
         
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sword_Attack"))
            {
                inMeleeAttack = true;
                Debug.Log("IN MELEE ATTACK");
            }
            else
            {
                inMeleeAttack = false;

            }

            if (inMeleeAttack && inSwordHitBox)
            {
                FindObjectOfType<AudioManager>().Play("Enemymelee");
                inSwordHitBox = false;
                inMeleeAttack = false;
                Debug.Log("Player hit by sword");
                playerHealth.Damage(1);
                FindObjectOfType<AudioManager>().Play("Playerdamage");
            }   
        }
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inSwordHitBox = true;
            Debug.Log("IN SWORD HIT BOX");
            Debug.Log("ANIM STATE: "+ anim.GetCurrentAnimatorStateInfo(0).IsName("Sword_Attack"));
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
