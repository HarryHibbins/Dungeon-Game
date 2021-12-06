using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLightChanger : MonoBehaviour
{
    public Light chestLight;

    private float every = 2f;
    private float colourstep;
    Color[] colours = new Color[6];
    int i;
    Color lerpedColour = Color.white;

    private void Start()
    {
        colours[0] = Color.white;
        colours[1] = Color.green;
        colours[2] = Color.blue;
        colours[3] = Color.magenta;
        colours[4] = Color.yellow;
        colours[5] = Color.white;
    }

    private void Update()
    {
        if (colourstep < every)
        {
            lerpedColour = Color.Lerp(colours[i], colours[i + 1], colourstep);
            chestLight.color = lerpedColour;
            colourstep += 0.0025f;
        }
        else
        {
            colourstep = 0;
            if (i < (colours.Length - 2))
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }
}
