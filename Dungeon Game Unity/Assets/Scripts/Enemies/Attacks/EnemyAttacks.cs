using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public enum AttackType
    {
        singleOrb,
        tripleOrb,
        Ice,
        Explosive,
        Speed,
    }

    public AttackType attackType;
    
    public float walkPointRange;
    public float timeBetweenAttacks;

    private void Start()
    {
        
    }

    public void Attack()
    {
        switch (attackType)
        {
            case AttackType.singleOrb:
                //Debug.Log("Attack");
                break;
        }
    }

}
