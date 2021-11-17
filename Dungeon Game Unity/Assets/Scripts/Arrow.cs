using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] private float baseDamage;
    [SerializeField] private float actualDamage;
    private GameObject bowObj;
    Bow bow;
    private float drawBackMultiplyer; 
    [SerializeField] float arrowDespawnTimer;
    public float damage;


    private void Awake()
    {
        bowObj = GameObject.FindWithTag("Bow");
        bow = bowObj.GetComponent<Bow>();
        drawBackMultiplyer = bow.drawBack;

        speed = 30;
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (speed * drawBackMultiplyer) * Time.deltaTime);
        arrowDespawnTimer -= Time.deltaTime;

        if(arrowDespawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //Hit
            actualDamage = baseDamage * drawBackMultiplyer;
            Destroy(gameObject);
        }
    }
}
