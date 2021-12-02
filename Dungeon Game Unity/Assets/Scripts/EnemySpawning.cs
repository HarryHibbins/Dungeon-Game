using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    private int numOfEnemies;

    private EnemyTypes enemyTypes;

    [SerializeField] private int minEnemiesInRoom = 2;
    
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
              
                if ( randomValue <= spawnChance ) {
                   Instantiate(getEnemyType(), child.position, child.rotation);
                   numOfEnemies++;

                }
                else {
                    //minimum of 2 enemies per room
                    if (numOfEnemies < minEnemiesInRoom)
                    {
                        Instantiate(getEnemyType(), child.position, child.rotation);

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
            //Spawn fast enemy
            enemyToSpawn = enemyTypes.enemies[1];
            Debug.Log("Spawn Fast enemy");
        }
        else if (randomValue > 2 && randomValue <= 4)
        {
            //Spawn tank enemy
            enemyToSpawn = enemyTypes.enemies[2];
            Debug.Log("Spawn Tank enemy");

        }
        /*if (Random.Range(0, enemyTypes.enemies.Length) == 1)
        {
            enemyToSpawn = enemyTypes.enemies[1];
        }*/
        else
        {
            //Spawn normal enemy
            enemyToSpawn = enemyTypes.enemies[0];
            Debug.Log("Spawn Normal enemy");

            
        }
        
        

        return enemyToSpawn;



    }

}
