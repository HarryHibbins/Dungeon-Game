using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public event EventHandler onDamaged;
    private List<Heart> heartList;
    
    public PlayerHealth(int heartAmount)
    {
        heartList = new List<Heart>();
        
        for (int i = 0; i < heartAmount; i++)
        {
            Heart newHeart = new Heart(4);
            heartList.Add(newHeart);
        }

    }

    public void Damage(int damageAmount)
    {
        for (int i = heartList.Count-1; i < 0; i--)
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
    }

  
}
