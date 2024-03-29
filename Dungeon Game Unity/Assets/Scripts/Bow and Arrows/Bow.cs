using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerController playerController;
    private PlayerInventory playerInventory;
    [SerializeField]
    private Animator playerAnimator;
    private GameObject gameManager;
    private PlayerStats playerStats;
    private GameLoot gameLoot;
    private PauseMenu pauseMenu;
    private AudioSource audioSrc;

    public bool draw;
    private bool fire;
    
    private GameObject drawPointObj;
    private Transform drawPoint;
    
    private GameObject firePointObj;
    private Transform firePoint;      
    
    public Arrow arrow;

    public float drawBack;
    [SerializeField] private float maxDrawBack;


    private int ammoCheck;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        playerObj = GameObject.FindGameObjectWithTag("Player");

        drawPointObj = GameObject.FindWithTag("Draw Point");
        firePointObj = GameObject.FindWithTag("Fire Point");

        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
        pauseMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>();
        
    }

    void Start()
    {
        drawPoint = drawPointObj.transform;
        firePoint = firePointObj.transform;
        //Minimum drawback value so it doesnt slow down the arrow
        drawBack = 1;
        audioSrc = GetComponent<AudioSource>();
    }

    [System.Obsolete]
    void Update()
    {
        //Hold left click to draw bow
        if (Input.GetButtonDown("Fire1") && !pauseMenu.isPaused)
        {
            if (playerInventory.getSelectedArrowAmmo() >= 1)
            {
                draw = true;
                //Create arrow
                Arrow newArrow = Instantiate(arrow, drawPoint.position, drawPoint.rotation) ;
                newArrow.transform.parent = drawPoint;
                audioSrc.Play();
            }
        }
        //Release left click to fire arrow
        else if (Input.GetButtonUp("Fire1") && draw)
        {
            FindObjectOfType<AudioManager>().Play("Bowfire");
            draw = false;
            fire = true;
            audioSrc.Stop();
        }
        //Cap the draw back to a value that can be changed depending on bow
        if (draw && drawBack < maxDrawBack)
        {
            drawBack += Time.deltaTime;
            playerController.currentMoveSpeed = playerStats.PM_DrawSpeed;
            playerAnimator.SetBool("Aiming", true);
        }

        //Debug.DrawLine(firePoint.position,new Vector3(playerController.mousePos.x, 0.5f,playerController.mousePos.z) );
        if (fire)
        {
            foreach(Transform child in drawPoint)
            {
                if (child.gameObject.tag == "Arrow")
                {
                    child.LookAt(new Vector3(playerController.mousePos.x, 0.5f,playerController.mousePos.z));
                    child.position = firePoint.position;
                    child.GetComponent<Arrow>().move = true;
                    child.GetComponent<Arrow>().ApplyDrawBackMultiplier();
                    child.transform.parent = null;

                }
            }
            //Reset Values ready for next shot
            fire = false;
            drawBack = 1;
            playerController.currentMoveSpeed = playerStats.PM_BaseSpeed;
            playerAnimator.SetBool("Aiming", false);
            int rand = UnityEngine.Random.Range(0, 6);

            if ((!gameLoot.getLootByName(LootItems.Loot.InfinityRelic).isActive) ||
                (gameLoot.getLootByName(LootItems.Loot.InfinityRelic).isActive && rand == 0))
            {
                //Minus the correct ammo count
                switch (playerInventory.equippedArrow)
                {
                    case ArrowTypes.Arrows.Normal:
                        {
                            playerInventory.normalArrowCount--;
                            break;
                        }
                    case ArrowTypes.Arrows.Fire:
                        {
                            playerInventory.fireArrowCount--;
                            break;
                        }
                    case ArrowTypes.Arrows.Ice:
                        {
                            playerInventory.iceArrowCount--;
                            break;
                        }
                    case ArrowTypes.Arrows.Explosive:
                        {
                            playerInventory.explosiveArrowCount--;
                            break;
                        }
                    case ArrowTypes.Arrows.Speed:
                        {
                            playerInventory.speedArrowCount--;
                            break;
                        }
                }
            }
            
            
            
            

        }
    }

   
}
