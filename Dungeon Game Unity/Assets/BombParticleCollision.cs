using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombParticleCollision : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Transform position = this.transform;
        if (other.layer == LayerMask.NameToLayer("Environment"))
        {
            OnExplosion(position);
        }
    }

    void OnExplosion(Transform pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos.position, 4f);
        foreach (Collider c in colliders)
        {
            if (c.tag == "Player")
            {
                playerHealth.Damage(1);
                Debug.Log("Player HIT");
            }
        }
    }
}
