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

    private AudioSource audioSrc;
    public bool isMovingForSound = false;

    private PlayerInventory playerInventory;
    private GameObject bowObj;
    private Bow bow;

    private GameObject gameManager;
    private PlayerStats playerStats;

    private PlayerHealth playerHealth;
    [SerializeField] private ParticleSystem ps;

    public int CameraPos;



    public Vector3 mousePos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        playerInventory = GetComponent<PlayerInventory>();
        playerHealth = GetComponent<PlayerHealth>();

        bow = GameObject.FindGameObjectWithTag("Bow").GetComponent<Bow>();

        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();

        audioSrc = GetComponent<AudioSource>();

    }

    void Start()
    {
        currentMoveSpeed = playerStats.PM_BaseSpeed;
    }
    
    void Update()
    {
        //Set movement values
        if (CameraPos == 0)
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        }

        else if (CameraPos == 1)
        {
            moveInput = new Vector3(-Input.GetAxisRaw("Vertical"), 0f, Input.GetAxisRaw("Horizontal")).normalized;

        }
        else if (CameraPos == 2)
        {
            moveInput = new Vector3(-Input.GetAxisRaw("Horizontal"), 0f, -Input.GetAxisRaw("Vertical")).normalized;
        }
        else if (CameraPos == 3)
        {
            moveInput = new Vector3(Input.GetAxisRaw("Vertical"), 0f, -Input.GetAxisRaw("Horizontal")).normalized;

        }

        moveVelocity = moveInput * currentMoveSpeed;

        if (moveInput.x != 0 || moveInput.z != 0)
        {
            anim.SetBool("isWalking", true);
            isMovingForSound = true;
        }

        else
        {
            anim.SetBool("isWalking", false);
            isMovingForSound = false;
        }

        if (isMovingForSound)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
        }
        

        else
            audioSrc.Stop();

        if (playerInventory.getSelectedArrowAmmo() >= 1)
        {
            ps.gameObject.SetActive(true);
        }
        else
        {
            ps.gameObject.SetActive(false);
        }


        //Create raycast for players aim
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        
        if (groundPlane.Raycast(cameraRay, out rayLength) && !playerHealth.dead)
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.green);

            mousePos = new Vector3(pointToLook.x,transform.position.y,pointToLook.z);
            transform.LookAt(mousePos);
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
            
            foreach (Transform child in transform)
            {
                if (child.name == "Current Arrow PS")
                {
                    ps = child.GetComponent<ParticleSystem>();
                }
            }


             switch (playerInventory.equippedArrow)
             {
                 case ArrowTypes.Arrows.Normal:
                 {


                     var col = ps.colorOverLifetime;
                     col.enabled = true;
                     col.color = new Color(255, 255, 255, 100);


                     break;
                 }
                 case ArrowTypes.Arrows.Fire:
                 {


                     var col = ps.colorOverLifetime;
                     col.enabled = true;
                     col.color = new Color(255, 0, 0, 100);

                     break;
                 }
                 case ArrowTypes.Arrows.Ice:
                 {


                     var col = ps.colorOverLifetime;
                     col.enabled = true;
                     col.color = new Color(0, 255, 255, 100);


                     break;
                 }
                 case ArrowTypes.Arrows.Explosive:
                 {


                     var col = ps.colorOverLifetime;
                     col.enabled = true;
                     col.color = new Color(255, 255, 0, 100);

                     break;
                 }
                 case ArrowTypes.Arrows.Speed:
                 {


                     var col = ps.colorOverLifetime;
                     col.enabled = true;
                     col.color = new Color(0, 255, 0, 100);



                     break;
                 }
             }
        }
    }
}
