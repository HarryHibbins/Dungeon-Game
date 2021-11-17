using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private float drawBackMultiplier; 
    
    [SerializeField] float baseSpeed;
    public float actualspeed;
    [SerializeField] private float baseDamage;
    public float actualDamage;
    public float gravity;
    
    private GameObject bowObj;
    Bow bow;


    [SerializeField] float arrowDespawnTimer;


    private void Awake()
    {
        bowObj = GameObject.FindWithTag("Bow");
        bow = bowObj.GetComponent<Bow>();
        drawBackMultiplier = bow.drawBack;
        actualDamage = baseDamage * drawBackMultiplier;
        actualspeed = baseSpeed * drawBackMultiplier;


    }

    void Update()
    {
        //Apply Forward Translation
        transform.Translate((Vector3.forward * actualspeed) * Time.deltaTime);
        //Apply Downward Translation for gravity
        transform.Translate((Vector3.down * gravity) * Time.deltaTime);
        arrowDespawnTimer -= Time.deltaTime;

        if(arrowDespawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If enemy is hit by the arrow round the damage value from the arrow worked out with the multiplier 
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Debug.Log("HIT Environment");
            actualspeed = 0;
            gravity = 0;
        }
    }
}
