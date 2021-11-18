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

    public float bleedDamage = 2;
    public float burnDamage = 5;
    public int bleedTime = 5;
    public int burnTime = 5;
    public int slowTime = 5;

    public float enemySpeed;
    public float currentSpeed;
    private float halfSpeed;

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
            hit = true;
            DamageDealt = Mathf.Round(arrow.actualDamage);
            
            //Stop the arrow 
            arrow.actualspeed = 0;
            arrow.gravity = 0;
            arrow.GetComponent<Transform>().parent = transform;

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
                    time = bleedTime;
                    break;
                }
            case ArrowTypes.Effects.Burn:
                {
                    time = burnTime;
                    break;
                }
            case ArrowTypes.Effects.Slow:
                {
                    time = slowTime;
                    break;
                }
        }
        while (progress != time)
        {
            switch (effect)
            {
                case ArrowTypes.Effects.Bleed:
                    {
                        health -= bleedDamage;
                        break;
                    }
                case ArrowTypes.Effects.Burn:
                    {
                        health -= burnDamage;
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
