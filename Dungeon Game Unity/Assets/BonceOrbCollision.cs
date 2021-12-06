using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonceOrbCollision : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            playerHealth.Damage(1);
            Debug.Log("PLAYER HIT");
        }
    }
}
