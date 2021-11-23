using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField]
    private Animator anim;
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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        camObj = GameObject.FindWithTag("MainCamera");
        cam = camObj.GetComponent<Camera>();
        
        playerInventory = GetComponent<PlayerInventory>();
        
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

        if (moveInput.x != 0 || moveInput.z != 0)
        {
            anim.SetBool("isWalking", true);
        }

        else
        {
            anim.SetBool("isWalking", false);
        }
       
        
        
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
