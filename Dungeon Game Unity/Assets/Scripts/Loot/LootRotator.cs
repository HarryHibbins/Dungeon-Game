using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootRotator : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up * (100 * Time.deltaTime));
    }
}
