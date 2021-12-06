using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public enum AttackType
    {
        orb,
    }

    public AttackType attackType;
    
    public float walkPointRange;
    public float timeBetweenAttacks;
    public float sightRange, attackRange;


    public Orb orb;



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
        }
    }

}
