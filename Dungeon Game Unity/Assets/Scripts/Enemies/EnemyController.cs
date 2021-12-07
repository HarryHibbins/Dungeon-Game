using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]public float health;
    private bool hit;
    private float DamageDealt;
    [SerializeField] ArrowTypes.Effects effect;
    private NavMeshAgent agent;
    private Animator anim;

    private float enemySpeed;
    private float halfSpeed;

    public bool alive;
    private PlayerStats playerStats;
    private GameLoot gameLoot;

    public GameObject firePS;
    public GameObject icePS;
    [SerializeField]
    private Transform effectSpawn;
    private GameObject ps;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        alive = true;

        foreach (Transform child in transform)
        {
            if (child.tag == "effectSpawn")
            {
                effectSpawn = child;
            }
        }
    }

    void Start()
    {
        enemySpeed = agent.speed;
        halfSpeed = agent.speed / 2;
    }

    void Update()
    {
        if (alive && hit)
        {
            FindObjectOfType<AudioManager>().Play("Enemydamage");
            //Deal damage to the enemy if hit
            //Debug.Log("Enemy hit: " + DamageDealt + "dmg");
            health -= DamageDealt;
            StartCoroutine(ApplyEffectDamage(effect));
            hit = false;
        }

        if (alive && health <= 0)
        {
            alive = false;
            
            anim.SetTrigger("Dead");

            StartCoroutine(OnCompleteAnimation());


        }
        IEnumerator OnCompleteAnimation()
        {
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If enemy is hit by the arrow round the damage value from the arrow worked out with the multiplier 
        if (alive && other.CompareTag("Arrow"))
        {
            //Get the arrow that hit the enemy
            Arrow arrow = other.GetComponent<Arrow>();
            //Stop the arrow 
            arrow.move = false;
            arrow.GetComponent<Transform>().parent = transform;

            //arrow.arrowDespawnTimer = 2.0f;
            Destroy(arrow.gameObject);

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
                        if (effect != ArrowTypes.Effects.Burn)
                        {
                            effect = ArrowTypes.Effects.Burn;
                        }
                        break;
                    }
                    case ArrowTypes.Arrows.Ice:
                    {
                        effect = ArrowTypes.Effects.Slow;
                        break;
                    }
                    case ArrowTypes.Arrows.Explosive:
                    {
                        arrow.SpawnExplosion();
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
                    ps = Instantiate(firePS, effectSpawn.position, Quaternion.Euler(-90, 0, 0));
                    ps.transform.parent = transform;
                    Debug.Log("Spawned Effect");
                    break;
                }
            case ArrowTypes.Effects.Slow:
                {
                    time = playerStats.ArrowEffects_SlowTime;
                    ps = Instantiate(icePS, effectSpawn.position, Quaternion.Euler(-90, 0, 0));
                    ps.transform.parent = transform;
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
                        agent.speed = halfSpeed;
                       
                        break;
                    }
            }
            
            yield return new WaitForSeconds(1);
            progress++;
        }
        Destroy(ps);
        agent.speed = enemySpeed;
    }
}
