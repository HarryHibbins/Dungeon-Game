using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    private int numOfEnemies;

    private EnemyTypes enemyTypes;

    [SerializeField] private int minEnemiesInRoom = 1;
    [SerializeField] private int maxEnemiesInRoom = 3;
    
    //Chance of spawning enemies per spawn point between 0 and 1
    [SerializeField] private float spawnChance = 0.5f;

    private void Awake()
    {
        enemyTypes = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyTypes>();
    }

    void Start()
    {
        foreach (Transform child in transform)
        {
            
            if (child.tag == "Enemy Spawn Point")
            {
                float randomValue = Random.value;
              
                if ( randomValue <= spawnChance && numOfEnemies < maxEnemiesInRoom) 
                {
                   GameObject newenemy = Instantiate(getEnemyType(), child.position, child.rotation);
                   numOfEnemies++;
                    newenemy.transform.parent = this.transform.parent;
                }
                else {
                    //minimum of 2 enemies per room
                    if (numOfEnemies < minEnemiesInRoom)
                    {
                        GameObject newenemy = Instantiate(getEnemyType(), child.position, child.rotation);
                        newenemy.transform.parent = this.transform.parent;
                        numOfEnemies++;
                    }
                }
            }

        }  
    }

    GameObject getEnemyType()
    {
        GameObject enemyToSpawn;


        float randomValue = Random.Range(1, 10);
              
        if ( randomValue <= 2 ) 
        {
            //Spawn Triple Orb enemy
            enemyToSpawn = enemyTypes.enemies[1];
        }
        else if (randomValue > 2 && randomValue <= 5)
        {
            //Spawn Warrior enemy
            enemyToSpawn = enemyTypes.enemies[2];

        }
        else
        {
            //Spawn Single Orb enemy
            enemyToSpawn = enemyTypes.enemies[0];
            //Debug.Log("Spawn Normal enemy");

            
        }
        
        

        return enemyToSpawn;



    }

}
