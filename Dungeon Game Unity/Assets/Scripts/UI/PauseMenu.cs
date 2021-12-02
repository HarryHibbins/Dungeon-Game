using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public bool inPauseMenu = false;
    public bool inRelicMenu = false;
    public bool inSettingsMenu = false;

    private bool fadeStart = false;

    public GameObject RelicUIPrefab;
    public GameObject ContentArea;
    public GameObject pausePanel;
    public GameObject relicPanel;
    public GameObject settingsPanel;
    public GameObject startPanel;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        fadeStart = true;
        startPanel.SetActive(true);
    }

    private void Update()
    {
        if (fadeStart)
        {
            Color panelcol = startPanel.GetComponent<Image>().color;
            panelcol.a -= (0.2f * Time.deltaTime);
            startPanel.GetComponent<Image>().color = panelcol;
            if (panelcol.a < 0)
            {
                startPanel.SetActive(false);
                fadeStart = false;
            }
        }

        if (inPauseMenu || inRelicMenu || inSettingsMenu)
        {
            playerController.enabled = false;
            isPaused = true;
        }
        else
        {
            playerController.enabled = true;
            isPaused = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inPauseMenu)
            {
                ResumeGame();
            }
            else if (inRelicMenu)
            {
                CloseRelicMenu();
            }
            else if (inSettingsMenu)
            {
                CloseSettingsMenu();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        inPauseMenu = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        inPauseMenu = false;
        Time.timeScale = 1;
    }

    public void OpenRelicMenu()
    {
        inPauseMenu = false;
        inRelicMenu = true;
        pausePanel.SetActive(false);
        relicPanel.SetActive(true);
    }

    public void CloseRelicMenu()
    {
        inRelicMenu = false;
        inPauseMenu = true;
        relicPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void OpenSettingsMenu()
    {
        inPauseMenu = false;
        inSettingsMenu = true;
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        inPauseMenu = true;
        inSettingsMenu = false;
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void AddToRelicUI(LootItems loot)
    {
        GameObject Relic = Instantiate(RelicUIPrefab, ContentArea.transform);

        Relic.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = loot.loot_sprite;
    }
}
