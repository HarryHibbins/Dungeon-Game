using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    private GameLoot gl_script;
    public LootItems.Loot LootName;

    void Start()
    {
        gl_script = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerPickUp")
        {
            if (gl_script.getLootByName(LootName).loot_type == LootItems.LootType.Relic)
            {
                gl_script.getLootByName(LootName).isCollected = true;
            }
            gl_script.StartCoroutine(gl_script.LootEffect(LootName));
            Destroy(this.gameObject);
        }
    }
}
