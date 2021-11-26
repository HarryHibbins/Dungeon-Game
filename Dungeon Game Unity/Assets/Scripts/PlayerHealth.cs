using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public const int maxFragmentAmount = 4;
    public event EventHandler onDamaged;
    public event EventHandler onHeal;
    public event EventHandler onDead;

    public bool dead;
    private List<Heart> heartList = new List<Heart>();

    [SerializeField] private int startHeartAmount;

    private void Start()
    {
        //heartList = new List<Heart>();
        
        for (int i = 0; i < startHeartAmount; i++)
        {
            Heart newHeart = new Heart(4);
            heartList.Add(newHeart);
        }
    }

    public void Damage(int damageAmount)
    {
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
            if (onDead != null) onDead(this, EventArgs.Empty);
            Debug.Log("Dead");
            dead = true;

        }
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
