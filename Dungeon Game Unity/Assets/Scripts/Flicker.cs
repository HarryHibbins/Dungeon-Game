using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    // Start is called before the first frame update
    public new Light light;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    public int smoothing = 5;
    
    Queue<float> smoothQueue;
    float lastSum = 0;
    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }
    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        if (light == null)
        {
            light = GetComponent<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (light == null)
            return;

        // pop off an item if too big
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        // Calculate new smoothed average
        light.intensity = lastSum / (float) smoothQueue.Count;
    }
}
