using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    private Rigidbody rb;

    public EnemyAttacks enemyAttack;

    private EnemyController EnemyController;
    
    public Vector3 walkPoint;
    private bool walkPointSet;
    private float walkPointRange;

    private float timeBetweenAttacks;
    private bool alreadyAttacked;

    private Animator anim;
    private float sightRange, attackRange;
    public bool canSee;
    public bool playerInSightRange, playerInAttackRange;

    private float velocity;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        timeBetweenAttacks = enemyAttack.timeBetweenAttacks;
        walkPointRange = enemyAttack.walkPointRange;
        sightRange = enemyAttack.sightRange;
        attackRange = enemyAttack.attackRange;
        EnemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        velocity = agent.velocity.magnitude / agent.speed;
        if (EnemyController.alive)
        {
            if (velocity > 0.1)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }


            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (/*!playerInSightRange && !playerInAttackRange || playerInSightRange &&*/ !canSee)
            {
                //Debug.Log("Patroling");
                Patroling();
            }


            if (playerInSightRange && !playerInAttackRange && canSee)
            {
                //Debug.Log("ChasePlayer");
                ChasePlayer();
            }

            if (playerInSightRange && playerInAttackRange && canSee)
            {
                Debug.DrawRay(transform.position + new Vector3(0, 0.8f, 0),
                    player.transform.position - transform.position);
                RaycastHit hit;
                //Plus 0.8 for the height orb
                if (Physics.Raycast(transform.position + new Vector3(0, 0.8f, 0),
                        player.transform.position - transform.position, out hit, 20) &&
                    enemyAttack.attackType == EnemyAttacks.AttackType.orb || enemyAttack.attackType == EnemyAttacks.AttackType.threeorb)
                {
                    if (hit.transform.tag == "Player")
                    {
                        AttackPlayer();
                    }
                    else
                    {
                        agent.SetDestination(player.position);
                    }
                }
                else if (enemyAttack.attackType == EnemyAttacks.AttackType.melee)
                {
                    AttackPlayer();

                }


                

            }
            
            
        }
        
    }
    private void Patroling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }
        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

        
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        FindObjectOfType<AudioManager>().Play("Enemysteps");
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (enemyAttack.attackType == EnemyAttacks.AttackType.orb || enemyAttack.attackType == EnemyAttacks.AttackType.threeorb)
        {
            transform.LookAt(player);
            
        }

        if(!alreadyAttacked)
        {
            alreadyAttacked = true;
            enemyAttack.Attack();
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
