using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public GameObject spawnerBox;
    public int enemyCount;
    private float xPos;
    private float yPos;
    private float zPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount<5)
        {
            xPos = spawnerBox.transform.position.x;
            yPos = spawnerBox.transform.position.y;
            zPos = spawnerBox.transform.position.z;
            Instantiate(enemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(3f);
            enemyCount += 1;
        }
    }

}
