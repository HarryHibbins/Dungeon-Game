using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Orb : MonoBehaviour
{

    [SerializeField] private float chargeTime;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float projectileSpeed;

    private PlayerHealth playerHealth;

    private Transform firePoint;

    private bool charged;
    void Awake()
    {

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        StartCoroutine(stopCharging());
    }

    void Update()
    {
        if (!charged)
        {
            transform.localScale += new Vector3(chargeSpeed, chargeSpeed, chargeSpeed) * Time.deltaTime;

        }
        else
        {
            transform.Translate((Vector3.forward * projectileSpeed) * Time.deltaTime);
        }
        

    }

    IEnumerator stopCharging()
    {
        yield return new WaitForSeconds(chargeTime);
        charged = true;
        transform.parent = null;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.Damage(1);
            Debug.Log("Player hit by Orb");
            Destroy(gameObject);

            
        } 
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment") || other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
