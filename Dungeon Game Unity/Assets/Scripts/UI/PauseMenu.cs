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
    public bool inControlsMenu = false;

    private bool fadeStart = false;

    public GameObject RelicUIPrefab;
    public GameObject ContentArea;
    public GameObject pausePanel;
    public GameObject relicPanel;
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    public GameObject startPanel;
    public Text relicDesc;

    private PlayerController playerController;

    public List<GameObject> relicList;
    private GameObject upgradeLoot;

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

        if (inPauseMenu || inRelicMenu || inSettingsMenu || inControlsMenu)
        {
            startPanel.SetActive(false);
            playerController.enabled = false;
            isPaused = true;
        }
        else
        {
            startPanel.SetActive(true);
            playerController.enabled = true;
            isPaused = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().Play("UIbutton");
            if (inPauseMenu)
            {
                ResumeGame();
            }
            else if (inRelicMenu)
            {
                CloseRelicMenu();
                PauseGame();
            }
            else if (inSettingsMenu)
            {
                CloseSettingsMenu();
            }
            else if (inControlsMenu)
            {
                CloseControlsMenu();
            }
            else
            {
                PauseGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !inPauseMenu && !inSettingsMenu && !inControlsMenu) 
        {
            if (!inRelicMenu)
            {
                OpenRelicMenu();
            }
            else
            {
                CloseRelicMenu();
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
        FindObjectOfType<AudioManager>().Play("UIbutton");
        pausePanel.SetActive(false);
        inPauseMenu = false;
        Time.timeScale = 1;
    }

    public void OpenRelicMenu()
    {
        FindObjectOfType<AudioManager>().Play("UIbutton");
        inPauseMenu = false;
        inRelicMenu = true;
        pausePanel.SetActive(false);
        relicPanel.SetActive(true);
    }

    public void CloseRelicMenu()
    {
        FindObjectOfType<AudioManager>().Play("UIbutton");
        inRelicMenu = false;
        inPauseMenu = false;
        relicPanel.SetActive(false);
        //pausePanel.SetActive(true);
    }

    public void OpenSettingsMenu()
    {
        FindObjectOfType<AudioManager>().Play("UIbutton");
        inPauseMenu = false;
        inSettingsMenu = true;
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        FindObjectOfType<AudioManager>().Play("UIbutton");
        inPauseMenu = true;
        inSettingsMenu = false;
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void AddToRelicUI(LootItems loot)
    {
        bool isUnique = true;

        if (loot.loot_type == LootItems.LootType.Relic)
        {
            GameObject Relic = Instantiate(RelicUIPrefab, ContentArea.transform);
            Relic.GetComponent<RelicDescHolder>().relicName = loot.loot_name.ToString();
            Relic.GetComponent<RelicDescHolder>().relicDesc = loot.loot_description;
            Relic.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = loot.loot_sprite;
            relicList.Add(Relic);
        }
        else if (loot.loot_type == LootItems.LootType.Upgrade)
        {
            Debug.Log(loot.loot_name.ToString());
            foreach (GameObject relic in relicList)
            {
                if (loot.loot_name.ToString() == relic.GetComponent<RelicDescHolder>().relicName)
                {
                    isUnique = false;
                    upgradeLoot = relic;
                }
            }
            if (isUnique)
            {
                GameObject Relic = Instantiate(RelicUIPrefab, ContentArea.transform);
                Relic.GetComponent<RelicDescHolder>().relicName = loot.loot_name.ToString();
                Relic.GetComponent<RelicDescHolder>().relicDesc = loot.loot_description;
                Relic.GetComponent<RelicDescHolder>().upgradeCount++;
                Relic.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = loot.loot_sprite;
                relicList.Add(Relic);
            }
            else if (!isUnique)
            {
                upgradeLoot.GetComponent<RelicDescHolder>().upgradeCount++;
            }
        }

        

        
    }

    public void OpenControlsMenu()
    {
        FindObjectOfType<AudioManager>().Play("UIbutton");
        inPauseMenu = false;
        inControlsMenu = true;
        pausePanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void CloseControlsMenu()
    {
        FindObjectOfType<AudioManager>().Play("UIbutton");
        inPauseMenu = true;
        inControlsMenu = false;
        pausePanel.SetActive(true);
        controlsPanel.SetActive(false);
    }
}
