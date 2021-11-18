using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public float currentMoveSpeed;
    public float drawBowMoveSpeed;
    public float normalMoveSpeed;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    
    private GameObject camObj;
    private Camera cam;

    private PlayerInventory playerInventory;
    private GameObject bowObj;
    private Bow bow;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camObj = GameObject.FindWithTag("MainCamera");
        cam = camObj.GetComponent<Camera>();

        currentMoveSpeed = normalMoveSpeed;
        playerInventory = GetComponent<PlayerInventory>();

        bowObj = GameObject.FindWithTag("Bow");
        bow = bowObj.GetComponent<Bow>();



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

        
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.green);

            transform.LookAt(new Vector3(pointToLook.x,transform.position.y,pointToLook.z));
        }

        if (Input.GetButtonDown("Change Ammo") && !bow.draw)
        {
            changeAmmo();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }

    //Cycle through ammo types
    private void changeAmmo()
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
