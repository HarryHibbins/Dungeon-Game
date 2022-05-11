using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject controlsPanel;
    public GameObject settingsPanel;
    private bool fadeout = false;
    private bool inControlsMenu = false;
    private bool inSettingsMenu = false;

    public List<GameObject> menuItems;

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("Trapdoor");
        fadePanel.SetActive(true);
        fadeout = true;
    }

    public void OpenControls()
    {
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(true);
        foreach (var item in menuItems)
        {
            item.SetActive(false);
        }
        inControlsMenu = true;
    }

    public void OpenSettings()
    {
        controlsPanel.SetActive(false);
        settingsPanel.SetActive(true);
        foreach (var item in menuItems)
        {
            item.SetActive(false);
        }
        inSettingsMenu = true;
    }

    private void Awake()
    {
        fadeout = false;
        inControlsMenu = false;
        inSettingsMenu = false;
        fadePanel.SetActive(false);
        Color panelcol = fadePanel.GetComponent<Image>().color;
        panelcol.a = 0;
        fadePanel.GetComponent<Image>().color = panelcol;
    }

    private void Update()
    {
        if (fadeout)
        {
            FindObjectOfType<AudioManager>().Play("Background");
            Color panelcol = fadePanel.GetComponent<Image>().color;
            panelcol.a += (Time.deltaTime);
            fadePanel.GetComponent<Image>().color = panelcol;
            if (panelcol.a > 1.5f)
            {
                fadeout = false;
                SceneManager.LoadScene(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inControlsMenu)
            {
                controlsPanel.SetActive(false);
                foreach (var item in menuItems)
                {
                    item.SetActive(true);
                }
                inControlsMenu = false;
            }
            if (inSettingsMenu)
            {
                settingsPanel.SetActive(false);
                foreach (var item in menuItems)
                {
                    item.SetActive(true);
                }
                inSettingsMenu = false;
            }
        }
    }
}
