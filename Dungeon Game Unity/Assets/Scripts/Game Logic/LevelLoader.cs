using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class LevelLoader : MonoBehaviour
{
    public GameObject startRoom;
    public RoomTemplates templates;
    private GameObject player;
    private GameObject entryRoomCam;
    private CameraPosition camposscript;
    private PauseMenu pauseMenu;

    private Transform startRoomCamPos;
    private Transform cameraTransform;

    public GameObject loadingScreen;
    public Text descendingText;
    public Text flavourText;

    [SerializeField]
    private string[] flavourTextArray;

    private GameLoot gameLoot;
    public Image BossChoiceOne;
    public Image BossChoiceTwo;
    public Image BossChoiceThree;

    public GameObject bossScreenPanel;
    private LootItems choice_one;
    private LootItems choice_two;
    private LootItems choice_three;

    private Transform camTrans;

    private void Awake()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameLoot = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameLoot>();
        pauseMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenu>();
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

    }

    private void Start()
    {
        camTrans = cameraTransform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(LoadingScreen());
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ShowBossLoot();
        }
    }

    public void ShowBossLoot()
    {
        bossScreenPanel.SetActive(true);

        AssignBossLoot();

        BossChoiceOne.sprite = choice_one.loot_sprite;
        BossChoiceOne.SetNativeSize();
        Vector2 currentsizeone = BossChoiceOne.rectTransform.sizeDelta;
        BossChoiceOne.rectTransform.sizeDelta = currentsizeone / 2;

        BossChoiceTwo.sprite = choice_two.loot_sprite;
        BossChoiceTwo.SetNativeSize();
        Vector2 currentsizetwo = BossChoiceTwo.rectTransform.sizeDelta;
        BossChoiceTwo.rectTransform.sizeDelta = currentsizetwo / 2;

        BossChoiceThree.sprite = choice_three.loot_sprite;
        BossChoiceThree.SetNativeSize();
        Vector2 currentsizethree = BossChoiceThree.rectTransform.sizeDelta;
        BossChoiceThree.rectTransform.sizeDelta = currentsizethree / 2;
    }

    void AssignBossLoot()
    {
        List<LootItems> loot = new List<LootItems>();
        loot = gameLoot.getLootByHighestRarityToSpawn(3);

        foreach (LootItems lootitem in loot)
        {
            Debug.Log(lootitem.loot_name);
        }

        choice_one = loot[0];
        choice_two = loot[1];
        choice_three = loot[2];
        Debug.Log("Loot One: " + choice_one.loot_name);
        Debug.Log("Loot Two: " + choice_two.loot_name);
        Debug.Log("Loot Three: " + choice_three.loot_name);
    }

    public void ChooseRelicOne()
    {
        choice_one.isActive = true;
        if (choice_one.loot_type == LootItems.LootType.Relic)
        {
            choice_one.isCollected = true;
        }
        pauseMenu.AddToRelicUI(choice_one);
        gameLoot.StartCoroutine(gameLoot.LootEffect(choice_one.loot_name));
        bossScreenPanel.SetActive(false);
        StartCoroutine(LoadingScreen());
    }

    public void ChooseRelicTwo()
    {
        choice_two.isActive = true;
        if (choice_two.loot_type == LootItems.LootType.Relic)
        {
            choice_two.isCollected = true;
        }
        pauseMenu.AddToRelicUI(choice_two);
        gameLoot.StartCoroutine(gameLoot.LootEffect(choice_two.loot_name));
        bossScreenPanel.SetActive(false);
        StartCoroutine(LoadingScreen());
    }

    public void ChooseRelicThree()
    {
        choice_three.isActive = true;
        if (choice_three.loot_type == LootItems.LootType.Relic)
        {
            choice_three.isCollected = true;
        }
        pauseMenu.AddToRelicUI(choice_three);
        gameLoot.StartCoroutine(gameLoot.LootEffect(choice_three.loot_name));
        bossScreenPanel.SetActive(false);
        StartCoroutine(LoadingScreen());
    }

    void LoadNewLevel()
    {
        GameObject[] rooms;
        rooms = GameObject.FindGameObjectsWithTag("RoomPrefab");
        GameObject[] loot;
        loot = GameObject.FindGameObjectsWithTag("Loot");
        GameObject[] bosses;
        bosses = GameObject.FindGameObjectsWithTag("Boss");

        foreach (GameObject room in rooms)
        {
            Destroy(room.gameObject);
        }
        foreach (GameObject lootobj in loot)
        {
            Destroy(lootobj.gameObject);
        }
        foreach (GameObject boss in bosses)
        {
            Destroy(boss.gameObject);
        }

        Destroy(templates.boss);
        templates.spawnedBoss = false;
        templates.waitTime = templates.startWaitTime;
        GameObject StartRoom = Instantiate(startRoom, new Vector3(0, 0, 0), Quaternion.identity);
        startRoom.GetComponentInChildren<RoomSpawner>().waitTime = 4;
        player.transform.position = StartRoom.transform.position;

        /*List<GameObject> newlist;
        foreach (Transform child in StartRoom.transform)
        {
            if (child.tag == "CameraPositions")
            {
                startRoomCamPos = child.GetChild(0).transform;
            }
        }*/


        
        camposscript = StartRoom.GetComponentInChildren<CameraPosition>();
        camposscript.inRoom = true;
        
        
        /*cameraTransform = camposscript
            .cameraPositions[player.GetComponent<PlayerController>().CameraPos];*/

        cameraTransform.position = new Vector3(2, 15, -12);
        Quaternion temp = Quaternion.Euler(54.5f,0,0);
        player.GetComponent<PlayerController>().CameraPos = 0;
        cameraTransform.rotation = temp;
    }

    IEnumerator LoadingScreen()
    {
        loadingScreen.SetActive(true);

        loadingScreen.GetComponent<CanvasRenderer>().SetAlpha(0);
        descendingText.GetComponent<CanvasRenderer>().SetAlpha(0);
        flavourText.GetComponent<CanvasRenderer>().SetAlpha(0);

        loadingScreen.GetComponent<Image>().CrossFadeAlpha(1, 1, false);
        descendingText.CrossFadeAlpha(1, 1, false);
        flavourText.CrossFadeAlpha(1, 1, false);


        int rand = UnityEngine.Random.Range(0, flavourTextArray.Length);
        flavourText.text = flavourTextArray[rand];

        if (flavourText.text.Contains("<r>"))
        {
            flavourText.text = flavourText.text.Replace("<r>", "");
            flavourText.color = Color.red;
        }
        else if (flavourText.text.Contains("<b>"))
        {
            flavourText.text = flavourText.text.Replace("<b>", "");
            flavourText.color = Color.blue;
        }
        else
        {
            flavourText.color = Color.white;
        }

        if (flavourText.text.Contains("<UN>"))
        {
            flavourText.text = flavourText.text.Replace("<UN>", Environment.UserName);
        }

        descendingText.text = "Descending";
        yield return new WaitForSeconds(1);
        LoadNewLevel();
        descendingText.text = "Descending.";
        yield return new WaitForSeconds(1);
        descendingText.text = "Descending..";
        yield return new WaitForSeconds(1);
        descendingText.text = "Descending...";

        loadingScreen.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
        descendingText.CrossFadeAlpha(0, 1, false);
        flavourText.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1);

        loadingScreen.SetActive(false);
        yield break;
    }
}
