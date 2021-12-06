using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Orb : MonoBehaviour
{

    [SerializeField] private float startSize;
    [SerializeField] private float fullSize;
    [SerializeField] private float chargeTime;

    private bool atFullSize;
    void Awake()
    {
        transform.localScale = new Vector3(startSize, startSize, startSize);
    }

    void Update()
    {
        if (!atFullSize)
        {
            
        }
    }
}
