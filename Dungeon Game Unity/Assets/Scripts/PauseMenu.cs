using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool inPauseMenu = false;
    public bool inRelicMenu = false;

    public GameObject RelicUIPrefab;
    public GameObject ContentArea;
    public GameObject pausePanel;
    public GameObject relicPanel;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (inPauseMenu || inRelicMenu)
        {
            playerController.enabled = false;
        }
        else
        {
            playerController.enabled = true;
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

    public void AddToRelicUI(LootItems loot)
    {
        GameObject Relic = Instantiate(RelicUIPrefab, ContentArea.transform);

        Relic.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = loot.loot_sprite;
    }
}
