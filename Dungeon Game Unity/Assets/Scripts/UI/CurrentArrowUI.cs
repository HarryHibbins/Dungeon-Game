using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrentArrowUI : MonoBehaviour
{
    public Text ammoType;
    private PlayerInventory playerInventory;
    private GameObject playerObj;
    
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerInventory = playerObj.GetComponent<PlayerInventory>();

    }

    void Update()
    {
        ammoType.text = playerInventory.equippedArrow.ToString();

    }
}
