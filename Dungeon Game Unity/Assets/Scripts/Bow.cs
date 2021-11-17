using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerController player;
    [SerializeField] private bool draw;
    private bool fire;
    private GameObject firePointObj;
    private Transform firePoint;

    public Arrow arrow;

    public float drawBack;
    [SerializeField] private float maxDrawBack;



    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<PlayerController>();
        firePointObj = GameObject.FindWithTag("Fire Point");
        firePoint = firePointObj.transform;

        //Minimum drawback value so it doesnt slow down the arrow
        drawBack = 1;
    }

    void Update()
    {
        //Hold left click to draw bow
        if (Input.GetButtonDown("Fire1"))
        {
            draw = true;
        }
        //Release left click to fire arrow
        else if (Input.GetButtonUp("Fire1"))
        {
            draw = false;
            fire = true;
        }
        //Cap the draw back to a value that can be changed depending on bow
        if (draw && drawBack < maxDrawBack)
        {
            drawBack += Time.deltaTime;
            player.currentMoveSpeed = player.drawBowMoveSpeed;
        }

        if (fire)
        {
            Arrow newArrow = Instantiate(arrow, firePoint.position, firePoint.rotation) as Arrow;
            fire = false;
            drawBack = 1;
            player.currentMoveSpeed = player.normalMoveSpeed;

        }
    }
}
