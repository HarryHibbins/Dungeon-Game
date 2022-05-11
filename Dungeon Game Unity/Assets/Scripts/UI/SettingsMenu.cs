using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider lightingSlider;
    public Light directionalLight;
    public Text lightingValueText;

    public Slider volumeSlider;
    public Text volumeValueText;

    private void Start()
    {
        directionalLight.intensity = PlayerPrefs.GetFloat("Lighting");
        lightingSlider.value = directionalLight.intensity;
        volumeSlider.value = AudioListener.volume * 5;
    }

    private void Update()
    {
        directionalLight.intensity = lightingSlider.value;
        lightingValueText.text = lightingSlider.value.ToString();

        AudioListener.volume = volumeSlider.value / 5;
        volumeValueText.text = volumeSlider.value.ToString();

        
    }


    public void OnChanged()
    {
        PlayerPrefs.SetFloat("Lighting", lightingSlider.value);
        directionalLight.intensity = PlayerPrefs.GetFloat("Lighting");
    }
}
