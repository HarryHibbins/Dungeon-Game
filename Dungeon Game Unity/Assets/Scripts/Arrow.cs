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
        transform.Translate(Vector3.forward * actualspeed * Time.deltaTime);
        arrowDespawnTimer -= Time.deltaTime;

        if(arrowDespawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    

    
    

}
