using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    [SerializeField] private Sprite heart4Sprite;
    [SerializeField] private Sprite heart3Sprite;
    [SerializeField] private Sprite heart2Sprite;
    [SerializeField] private Sprite heart1Sprite;
    [SerializeField] private Sprite heart0Sprite;

    
    private List<HeartImage> heartImageList;
    private PlayerHealth playerHealth;
    

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }


    void Start()
    {
        PlayerHealth playerHealth = new PlayerHealth(3); //amount of hearts
        setPlayerHearts(playerHealth);
        
        

    }

    public void setPlayerHearts(PlayerHealth playerHealth)
    {
        this.playerHealth = playerHealth;

        List<PlayerHealth.Heart> heartList = playerHealth.getHeartList();
        Vector2 heartAnchorPos = new Vector2(0, 0);
        for (int i = 0; i < heartList.Count; i++)
        {
            PlayerHealth.Heart heart = heartList[i];
            CreateHeart(heartAnchorPos).setHeartFragments(heart.getFragments());
            heartAnchorPos += new Vector2(75, 0);

        }

        playerHealth.onDamaged += playerHealth_onDamaged;
        playerHealth.onHeal  += playerHealth_onHeal;
        playerHealth.onDead += playerHealth_onDead;


    }

    private void playerHealth_onDamaged(object sender, System.EventArgs e)
    {

        refreshAllHearts();
    }

    private void playerHealth_onHeal(object sender, System.EventArgs e)
    {
        refreshAllHearts();
    }
    
    private void playerHealth_onDead(object sender, System.EventArgs e)
    {
        Debug.Log("Dead");
    }

    private void refreshAllHearts()
    {
        List<PlayerHealth.Heart> heartList = playerHealth.getHeartList();
        
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            PlayerHealth.Heart heart = heartList[i];
            
            heartImage.setHeartFragments(heart.getFragments());
        }
    }

   

    
    private HeartImage CreateHeart(Vector2 anchorPos)
    {
        GameObject heartObject = new GameObject("Heart", typeof(Image));
        
        heartObject.transform.parent = transform;
        heartObject.transform.localPosition = Vector3.zero;

        //Location and size
        heartObject.GetComponent<RectTransform>().anchoredPosition = anchorPos;
        heartObject.GetComponent<RectTransform>().sizeDelta = new Vector2(52, 46);
        
        //set Sprite
        Image heartImageUI = heartObject.GetComponent<Image>();
        heartImageUI.sprite = heart4Sprite;


        HeartImage heartImage = new HeartImage(this, heartImageUI);
        heartImageList.Add(heartImage);
        
        return heartImage;

    }

    public class HeartImage
    {
        
        private Image heartImage;
        private HeartsUI heartsUI;
        public HeartImage(HeartsUI heartsUI, Image heartImage)
        {
            this.heartsUI = heartsUI;
            this.heartImage = heartImage;
//            this.heartImage.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

        }

        public void setHeartFragments(int fragments)
        {
            switch (fragments)
            {
                case 0:
                    heartImage.sprite = heartsUI.heart0Sprite;
                    break;
                
                  case 1:
                    heartImage.sprite = heartsUI.heart1Sprite;
                    break;
                  
                case 2:
                    heartImage.sprite = heartsUI.heart2Sprite;
                    break;

                case 3:
                    heartImage.sprite = heartsUI.heart3Sprite;
                    break;

                case 4:
                    heartImage.sprite = heartsUI.heart4Sprite;
                    break;

            }
        }
    }
    
    
    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerHealth.Damage(1);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            playerHealth.Heal(3);
        }
    }
    



}
