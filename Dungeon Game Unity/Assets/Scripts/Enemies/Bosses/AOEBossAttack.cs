using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBossAttack : MonoBehaviour
{
    public GameObject ringEffect;
    private GameObject player;
    private PlayerHealth playerHealth;
    private BossTwoBehaviour bossScript;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        bossScript = this.transform.parent.GetComponent<BossTwoBehaviour>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartAOEAttack());
    }


    public IEnumerator StartAOEAttack()
    {
        ringEffect.SetActive(true);
        yield return new WaitForSeconds(5);

        RaycastHit hit;
        if (Physics.Raycast (transform.position, player.transform.position - transform.position, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                playerHealth.Damage(bossScript.attackTwoDamage);
                Debug.LogError("HIT PLAYER");
            }

        }
        ringEffect.SetActive(false);
        yield break;
    }
}
