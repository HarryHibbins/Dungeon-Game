using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private bool inRange = false;
    private bool opened = false;
    public AnimationClip animationClip;
    private Animation animation;
    public ParticleSystem ps;
    private GameLoot gameLoot;
    public GameObject lootpos;

    private void Awake()
    {
        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
        animation = GetComponent<Animation>();
    }

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetButtonDown("Interact") && !opened)
            {
                StartCoroutine(OpenLootChest());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }

    IEnumerator OpenLootChest()
    {
        LootItems loot = gameLoot.getLootByRarityToSpawn(gameLoot.RandomRarity());
        switch (loot.loot_rarity)
        {
            case LootItems.LootRarity.Common:
                ps.startColor = Color.white;
                break;
            case LootItems.LootRarity.Uncommon:
                ps.startColor = Color.green;
                break;
            case LootItems.LootRarity.Rare:
                ps.startColor = Color.blue;
                break;
            case LootItems.LootRarity.Epic:
                ps.startColor = Color.magenta;
                break;
            case LootItems.LootRarity.Legendary:
                ps.startColor = Color.yellow;
                break;
        }

        
        //Play chest animation
        animation.clip = animationClip;
        ps.Play();
        yield return new WaitForSeconds(0.5f);
        animation.Play();
        yield return new WaitForSeconds(1.8f);
        ps.Stop();
        gameLoot.SpawnLoot(lootpos.transform.position, loot);
        opened = true;
        yield break;
    }
}
