using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject controlsPanel;
    private bool fadeout = false;
    private bool inControlsMenu = false;

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("Trapdoor");
        Debug.Log("Play");
        fadePanel.SetActive(true);
        fadeout = true;
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
        inControlsMenu = true;
    }

    private void Start()
    {
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
                inControlsMenu = false;
            }
            
        }
    }
}
