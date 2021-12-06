using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEBossAttack : MonoBehaviour
{
    public GameObject ringEffect;
    private GameObject player;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(StartAOEAttack());
            //Debug.DrawRay(transform.position, player.transform.position - transform.position);
        }
    }

    public IEnumerator StartAOEAttack()
    {
        ringEffect.SetActive(true);
        yield return new WaitForSeconds(5);

        RaycastHit hit;
        if (Physics.Raycast (transform.position, player.transform.position - transform.position, out hit, 10))
        {
            if (hit.transform.tag == "Player")
            {
                playerHealth.Damage(4);
                Debug.LogError("HIT PLAYER");
            }

        }
        ringEffect.SetActive(false);
        yield break;
    }
}
