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
            gl_script.StartCoroutine(gl_script.LootEffect(LootName));
            Destroy(this.gameObject);
        }
    }
}
