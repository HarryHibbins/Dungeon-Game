using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerStats playerStats;
    private GameLoot gameLoot;

    public const int maxFragmentAmount = 4;
    public event EventHandler onDamaged;
    public event EventHandler onHeal;
    public event EventHandler onDead;

    public bool dead;
    private List<Heart> heartList = new List<Heart>();
    private GameObject player;
    private Animator anim;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerStats>();
        player = GameObject.FindWithTag("Player");
        anim = player.GetComponent<Animator>();
        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
        
        for (int i = 0; i < playerStats.playerHearts; i++)
        {
            Heart newHeart = new Heart(4);
            heartList.Add(newHeart);
        }
    }


    public void UpdateHearts (int amount)
    {
        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                Heart newHeart = new Heart(4);
                heartList.Add(newHeart);
            }
        }
        else if (amount < 0)
        {
            int temp = amount * -1;
            for (int i = 0; i < temp; i++)
            {
                Heart newHeart = new Heart(4);
                heartList.RemoveAt(0);
            }
        }
        playerStats.playerHearts += amount;
    }

    public void Damage(int damageAmount)
    {
        if (gameLoot.getLootByName(LootItems.Loot.AncientHelm).isActive)
        {
            int rand = UnityEngine.Random.Range(0, 101);
            if (rand <= 20)
            {
                damageAmount = 0;
            }
        }

        for (int i = heartList.Count-1; i >= 0; i--)
        {
            Heart heart = heartList[i];//Current Heart

            if (damageAmount > heart.getFragments())//If the incoming damage is higher than fragments in current heart
            {
                //Damage heart and go to next one
                damageAmount -= heart.getFragments();
                heart.Damage(heart.getFragments());
            }

            else
            {
                //Just damage this heart
                heart.Damage(damageAmount);
                break;
            }
        }
        
        if (onDamaged != null) onDamaged(this, EventArgs.Empty);

        if (isDead())
        {
            if (gameLoot.getLootByName(LootItems.Loot.LizardTailRelic).isActive)
            {
                Heal(playerStats.playerHearts * 4);
                gameLoot.getLootByName(LootItems.Loot.LizardTailRelic).isActive = false;
            }
            else
            {
                if (onDead != null) onDead(this, EventArgs.Empty);
                Debug.Log("Dead");
                anim.SetTrigger("Dead");
                dead = true;
            }
        }
    }

    public int GetMaxHealth()
    {
        return heartList.Count * 4;
    }

    public void Heal(int healAmount)
    {
        for (int i = 0; i < heartList.Count; i++)
        {
            Heart heart = heartList[i];
            int missingFragments = maxFragmentAmount - heart.getFragments();
            if (healAmount > missingFragments)
            {
                healAmount -= missingFragments;
                heart.Heal(missingFragments);
            }
            else
            {
                heart.Heal(healAmount);
                break;
            }
        }
        if (onHeal != null) onHeal(this, EventArgs.Empty);

    }

    public bool isDead()
    {
        return heartList[0].getFragments() == 0;
    }
    
    
    public List<Heart> getHeartList()
    {
        return heartList;
    }
    

    public class Heart
    {
        private int fragments;
        
        public Heart(int fragments)
        {
            this.fragments = fragments;
        }

        public int getFragments()
        {
            return fragments;
        }
        
        public void setFragments(int fragments)
        {
            this.fragments = fragments;
        }

        public void Damage(int damageAmount)
        {
            if (damageAmount > fragments)
            {
                fragments = 0;
            }
            else
            {
                fragments -= damageAmount;
            }
        }

        public void Heal(int healAmount)
        {
            if (fragments + healAmount > maxFragmentAmount)
            {
                fragments = maxFragmentAmount;
            }
            else
            {
                fragments += healAmount;
            }
        }
    }
    
    private void Update()
    { 
        //Testing damage and heal
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Damage(1);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Heal(1);
        }
        
    }

  
}
