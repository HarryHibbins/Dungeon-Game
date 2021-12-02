using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject fadePanel;
    private bool fadeout = false;

    public void PlayGame()
    {
        Debug.Log("Play");
        fadePanel.SetActive(true);
        fadeout = true;
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
            Color panelcol = fadePanel.GetComponent<Image>().color;
            panelcol.a += (Time.deltaTime);
            fadePanel.GetComponent<Image>().color = panelcol;
            if (panelcol.a > 1.5f)
            {
                fadeout = false;
                SceneManager.LoadScene(1);
            }
        }
    }
}
