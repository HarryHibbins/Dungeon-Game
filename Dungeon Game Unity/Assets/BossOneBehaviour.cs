using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneBehaviour : MonoBehaviour
{
    public GameObject attackOne;
    public int attackOneDamage;
    public GameObject attackTwo;
    public int attackTwoDamage;

    private EnemyController enemyController;

    private float maxHealth;
    private bool isAttacking = false;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    private void Start()
    {
        maxHealth = enemyController.health;
        attackOne.SetActive(false);
        attackTwo.SetActive(false);
    }

    private void Update()
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackPattern());
        }
    }

    public IEnumerator AttackPattern()
    {
        isAttacking = true;
        attackOne.SetActive(true);
        yield return new WaitForSeconds(10);
        attackTwo.SetActive(true);
        yield return new WaitForSeconds(10);
        attackOne.SetActive(false);
        attackTwo.SetActive(false);
        isAttacking = false;
        yield break;
    }
}
