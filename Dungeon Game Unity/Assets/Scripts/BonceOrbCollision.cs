using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonceOrbCollision : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private BossOneBehaviour bossScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        bossScript = this.transform.parent.GetComponent<BossOneBehaviour>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Playerdamage");
            playerHealth.Damage(bossScript.attackOneDamage);
            Debug.Log("PLAYER HIT");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            FindObjectOfType<AudioManager>().Play("Ballbounce");
        }
    }

}
