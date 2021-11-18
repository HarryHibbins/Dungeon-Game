using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsRemainingUI : MonoBehaviour
{
    public Text ammoCountText;
    private PlayerInventory playerInventory;
    private GameObject playerObj;
    
    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerInventory = playerObj.GetComponent<PlayerInventory>();

    }

    void Update()
    {
        ammoCountText.text = playerInventory.getSelectedArrowAmmo().ToString() + "/"+ playerInventory.getMaxSelectedArrowAmmo();
    }
}
