using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float health;
    private bool hit;
    private float DamageDealt;
    [SerializeField] ArrowTypes.Effects effect;

    public float enemySpeed;
    public float currentSpeed;
    private float halfSpeed;
    
    

    private PlayerStats playerStats;
    private GameLoot gameLoot;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
        
    }

    void Start()
    {
        currentSpeed = enemySpeed;
        halfSpeed = enemySpeed / 2;
    }

    void Update()
    {
        halfSpeed = enemySpeed / 2;
        if (hit)
        {
            //Deal damage to the enemy if hit
            Debug.Log("Enemy hit: " + DamageDealt + "dmg");
            health -= DamageDealt;
            StartCoroutine(ApplyEffectDamage(effect));
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
            arrow.arrowDespawnTimer = 2.0f;

            int crit_rand = UnityEngine.Random.Range(playerStats.ArrowDamage_CritChance, 101);

            if (this.tag == "Boss" && gameLoot.getLootByName(LootItems.Loot.ArmourPiercingArrows).isActive)
            {
                DamageDealt = Mathf.Round(arrow.actualDamage) + Mathf.Round(arrow.actualDamage / 0.2f);
            }
            else
            {
                DamageDealt = Mathf.Round(arrow.actualDamage);
            }
            if (crit_rand <= 100)
            {
                DamageDealt *= 2;
            }
            if (gameLoot.warbannerDamage)
            {
                DamageDealt *= 1.5f;
            }
            
            //Stop the arrow 
            arrow.move = false;
            arrow.GetComponent<Transform>().parent = transform;
            int rand = UnityEngine.Random.Range(1, (playerStats.ArrowEffects_BleedChance + 1));

            //Apply Effect
            switch (arrow.selectedArrow)
            {
                case ArrowTypes.Arrows.Normal:
                    {
                        effect = ArrowTypes.Effects.NONE;
                        break;
                    }
                case ArrowTypes.Arrows.Fire:
                    {
                        effect = ArrowTypes.Effects.Burn;
                        break;
                    }
                case ArrowTypes.Arrows.Ice:
                    {
                        effect = ArrowTypes.Effects.Slow;
                        break;
                    }
                case ArrowTypes.Arrows.Explosive:
                    {
                        break;
                    }
                case ArrowTypes.Arrows.Speed:
                    {
                        break;
                    }
            }
            if (rand == 1)
            {
                effect = ArrowTypes.Effects.Bleed;
            }
            hit = true;
        }
    }

    IEnumerator ApplyEffectDamage(ArrowTypes.Effects effect)
    {
        int progress = 0;
        float time = 0;
        switch (effect)
        {
            case ArrowTypes.Effects.NONE:
                {
                    time = 0;
                    break;
                }
            case ArrowTypes.Effects.Bleed:
                {
                    time = playerStats.ArrowEffects_BleedTime;
                    break;
                }
            case ArrowTypes.Effects.Burn:
                {
                    time = playerStats.ArrowEffects_BurnTime;
                    break;
                }
            case ArrowTypes.Effects.Slow:
                {
                    time = playerStats.ArrowEffects_SlowTime;
                    break;
                }
        }
        while (progress != time)
        {
            switch (effect)
            {
                case ArrowTypes.Effects.Bleed:
                    {
                        health -= playerStats.ArrowEffects_BleedDamage;
                        break;
                    }
                case ArrowTypes.Effects.Burn:
                    {
                        health -= playerStats.ArrowEffects_BurnDamage;
                        break;
                    }
                case ArrowTypes.Effects.Slow:
                    {
                        currentSpeed = halfSpeed;
                        break;
                    }
            }

            yield return new WaitForSeconds(1);
            progress++;
        }
        currentSpeed = enemySpeed;
    }
}