using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    private GameLoot gl_script;
    private PauseMenu pm_script;
    public LootItems.Loot LootName;

    private void Awake()
    {
        gl_script = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
        pm_script = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerPickUp")
        {
            if (gl_script.getLootByName(LootName).loot_type == LootItems.LootType.Relic)
            {
                gl_script.getLootByName(LootName).isCollected = true;
                pm_script.AddToRelicUI(gl_script.getLootByName(LootName));
            }
            gl_script.StartCoroutine(gl_script.LootEffect(LootName));
            Destroy(this.gameObject);
        }
    }
}
