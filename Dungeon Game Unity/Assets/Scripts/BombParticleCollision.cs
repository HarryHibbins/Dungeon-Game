using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombParticleCollision : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;

    private ParticleSystem ps;
    private List<ParticleCollisionEvent> collisionEvents;
    private Vector3 position;

    private BossTwoBehaviour bossScript;

    // Start is called before the first frame update
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        bossScript = this.transform.parent.GetComponent<BossTwoBehaviour>();
    }


    private void OnParticleCollision(GameObject other)
    {
        
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while (i < numCollisionEvents)
        {
            position = collisionEvents[i].intersection;
            i++;
        }

        if (other.layer == LayerMask.NameToLayer("Environment"))
        {
            OnExplosion(position);
        }
    }

    void OnExplosion(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 4f);
        foreach (Collider c in colliders)
        {
            if (c.tag == "Player")
            {
                playerHealth.Damage(bossScript.attackOneDamage);
                Debug.Log("Player HIT");
            }
        }
    }
}
