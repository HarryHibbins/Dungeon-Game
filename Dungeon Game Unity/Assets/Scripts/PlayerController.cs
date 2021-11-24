using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
     
    public float currentMoveSpeed;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    
    private GameObject camObj;
    private Camera cam;

   
    private PlayerInventory playerInventory;
    private GameObject bowObj;
    private Bow bow;

    private GameObject gameManager;
    private PlayerStats playerStats;

    private PlayerHealth playerHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camObj = GameObject.FindWithTag("MainCamera");
        cam = camObj.GetComponent<Camera>();

        playerInventory = GetComponent<PlayerInventory>();
        playerHealth = GetComponent<PlayerHealth>();

        bowObj = GameObject.FindWithTag("Bow");
        bow = bowObj.GetComponent<Bow>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerStats = gameManager.GetComponent<PlayerStats>();

        currentMoveSpeed = playerStats.PM_BaseSpeed;
    }
    
    void Update()
    {
        //Set movement values
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * currentMoveSpeed;

        
       
        
        
        //Create raycast for players aim
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        
        if (groundPlane.Raycast(cameraRay, out rayLength) && !playerHealth.dead)
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.green);

            transform.LookAt(new Vector3(pointToLook.x,transform.position.y,pointToLook.z));
        }

        if (Input.GetButtonDown("Change Ammo") && !bow.draw && !playerHealth.dead)
        {
            changeAmmo();
        }
    }
    private void FixedUpdate()
    {
        if (!playerHealth.dead)
        {
            rb.velocity = moveVelocity;
        }
    }

    //Cycle through ammo types
    private void changeAmmo()
    {
        if (!playerHealth.dead)
        {
            if (playerInventory.equippedArrow != ArrowTypes.Arrows.Speed)
            {
                playerInventory.equippedArrow += 1;
            }
            else
            {
                playerInventory.equippedArrow = 0;
            }
        }
        
      
    }
}
