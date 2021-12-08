using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

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
    private LevelLoader levelLoader;

    public GameObject firePS;
    public GameObject icePS;
    public GameObject bleedPS;
    [SerializeField]
    private Transform effectSpawn;
    private GameObject ps;

    public bool onFire;
    public bool onIce;

    private bool showLoot = false;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
        levelLoader = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelLoader>();
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
            agent.speed = 0;

            StartCoroutine(OnCompleteAnimation());
        }

        if (this.tag == "Boss" && health <= 0 && !showLoot)
        {
            showLoot = true;
            levelLoader.ShowBossLoot();
        }

        IEnumerator OnCompleteAnimation()
        {
            yield return new WaitForSeconds(1);
            Vector3 pos = new Vector3(transform.position.x, 1.0f, transform.position.z);
            
            float randomValue = UnityEngine.Random.value;

           
            
            if (randomValue <= 0.75f)
            {
                gameLoot.SpawnLoot(pos,gameLoot.getLootByTypeToSpawn(LootItems.LootType.Consumable));
            }

            if (randomValue <= 0.25f)
            {
                pos.x += 1.0f;
                gameLoot.SpawnLoot(pos,gameLoot.getLootByRarityToSpawn(gameLoot.RandomRarity()));

            }
            
            Destroy(transform.parent.gameObject);
        }

        
    }

    IEnumerator CheckForEffect(ArrowTypes.Effects effect)
    {
        switch (effect)
        {
            case ArrowTypes.Effects.NONE:
            {
                break;
            }
            case ArrowTypes.Effects.Burn:
            {
                yield return new WaitForSeconds(playerStats.ArrowEffects_BurnTime);
                foreach (Transform child in transform)
                {
                    if (child.tag == "OnFire")
                    {
                        Destroy(child.gameObject);
                        onFire = false;

                    }
                }
                break;
            }
            case ArrowTypes.Effects.Slow:
            {
                yield return new WaitForSeconds(playerStats.ArrowEffects_SlowTime);
                foreach (Transform child in transform)
                {
                    if (child.tag == "OnIce")
                    {
                        Destroy(child.gameObject);
                        onIce = false;

                    }
                }
                break;
            }
        }
        yield break;
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
            
            ps = Instantiate(bleedPS, effectSpawn.position, Quaternion.Euler(0, 0, 0));
            ps.transform.parent = transform;
            
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
                    ps = Instantiate(bleedPS, effectSpawn.position, Quaternion.Euler(0, 0, 0));
                    ps.transform.parent = transform;
                    break;
                }
            case ArrowTypes.Effects.Bleed:
                {
                    time = playerStats.ArrowEffects_BleedTime;
                    ps = Instantiate(bleedPS, effectSpawn.position, Quaternion.Euler(0, 0, 0));
                    ps.transform.parent = transform;
                    
                    break;
                }
            case ArrowTypes.Effects.Burn:
                {
                    time = playerStats.ArrowEffects_BurnTime;
                    
                    if (!onFire)
                    {
                        ps = Instantiate(firePS, effectSpawn.position, Quaternion.Euler(-90, 0, 0));
                        ps.transform.parent = transform;
                        onFire = true;
                        StartCoroutine(CheckForEffect(ArrowTypes.Effects.Burn));
                    }
                    
                    
                    break;
                }
            case ArrowTypes.Effects.Slow:
                {
                    time = playerStats.ArrowEffects_SlowTime;
                    if (!onIce)
                    {
                        ps = Instantiate(icePS, effectSpawn.position, Quaternion.Euler(-90, 0, 0));
                        ps.transform.parent = transform;
                        onIce = true;
                        StartCoroutine(CheckForEffect(ArrowTypes.Effects.Slow));

                    }

                    
                    break;
                }
        }
        while (progress <= time)
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

        

        agent.speed = enemySpeed;
    }
}
