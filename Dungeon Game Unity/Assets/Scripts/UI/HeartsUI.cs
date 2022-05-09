using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
    private GameObject playerObj;
    [SerializeField] private PlayerHealth playerHealth;
    
    

    private void Start()
    {
        heartImageList = new List<HeartImage>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerObj.GetComponent<PlayerHealth>();
        setPlayerHearts();
    }



    public void UpdateHearts (int amount)
    {
        playerHealth.UpdateHearts(amount);

        GameObject[] hearts = GameObject.FindGameObjectsWithTag("Heart");
        foreach (GameObject heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        setPlayerHearts();
    }    

    public void setPlayerHearts( )
    {
        
        List<PlayerHealth.Heart> heartList = playerHealth.getHeartList();
        Vector2 heartAnchorPos = new Vector2(0, 0);
        for (int i = 0; i < heartList.Count; i++)
        {
            PlayerHealth.Heart heart = heartList[i];
            CreateHeart(heartAnchorPos).setHeartFragments(heart.getFragments());
            //heartAnchorPos += new Vector2(75, 0);
            heartAnchorPos += new Vector2(40, 0);
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
        heartObject.tag = "Heart";

        //Location and size
        heartObject.GetComponent<RectTransform>().anchoredPosition = anchorPos;
        //heartObject.GetComponent<RectTransform>().sizeDelta = new Vector2(52, 46);
        heartObject.GetComponent<RectTransform>().sizeDelta = new Vector2(33, 30);
        
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
            //this.heartImage.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
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
}
