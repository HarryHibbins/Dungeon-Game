using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelLoader : MonoBehaviour
{
    public GameObject startRoom;
    private GameObject player;
    private CameraPosition camposscript;

    private Transform startRoomCamPos;
    private GameObject cameraObj;
    private Transform cameraTransform;

    public GameObject loadingScreen;
    public Text descendingText;
    public Text flavourText;

    [SerializeField]
    private string[] flavourTextArray;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = cameraObj.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(LoadingScreen());
        }
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

        GameObject StartRoom = Instantiate(startRoom, new Vector3(0, 0, 0), Quaternion.identity);
        player.transform.position = StartRoom.transform.position;

        foreach (Transform child in StartRoom.transform)
        {
            if (child.tag == "Camera Position")
            {
                startRoomCamPos = child;
            }
        }

        cameraTransform.position = startRoomCamPos.position;
        camposscript = StartRoom.GetComponentInChildren<CameraPosition>();
        camposscript.inRoom = true;
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
