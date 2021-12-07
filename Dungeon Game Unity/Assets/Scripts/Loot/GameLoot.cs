using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameLoot : MonoBehaviour
{
    //public GameObject lootPrefab;

    private GameObject player;
    private PlayerStats playerStats;
    private PlayerInventory playerInventory;
    private PlayerController playerController;
    private HeartsUI heartsUI;
    private PlayerHealth playerHealth;

    private GameObject helmet;
    private GameObject visor;
    private GameObject chestPlate;
    private Material[] armourMat; 
  
    public Material[] armourMats;
   
    
    public List<LootItems> lootList;
    [Space(3)]
    [Header("NEW LOOT OBJECT")]
    public LootItems.Loot lootname;
    [Multiline]
    public string lootdesc;
    public GameObject lootprefab;
    public Sprite lootsprite;
    public LootItems.LootType loottype;
    public LootItems.LootRarity lootrarity;

    [HideInInspector]
    public int explorerRelicRooms = 0;
    private int LootCount = 0;
    private float warbanner_timer = 0f;
    private float warbanner_goal = 3f;
    [HideInInspector]
    public bool warbannerDamage = false;
    //[SerializeField] [HideInInspector]
    private List<ParticleSystem> warbannerPSList;
    private GameObject warbannerps;
    private bool isPSPlaying = false;

    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>();
        heartsUI = GameObject.FindGameObjectWithTag("HeartsUI").GetComponent<HeartsUI>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        warbannerPSList = new List<ParticleSystem>();
        helmet = GameObject.FindWithTag("Helmet");
        visor = GameObject.FindWithTag("Visor");
        chestPlate = GameObject.FindWithTag("ChestPlate");
        armourMat = helmet.GetComponent<Renderer>().materials;
      }

    private void Start()
    {
        warbannerps = player.transform.Find("WarbannerEffect").gameObject;
        foreach (Transform child in warbannerps.transform)
        {
            warbannerPSList.Add(child.GetComponent<ParticleSystem>());
        }
        helmet.SetActive(false);
        visor.SetActive(false);
        
    }

    private void Update()
    {
        UpdateRelics();

        if (Input.GetKeyDown(KeyCode.M))
        {
            //SpawnLoot(new Vector3(0, 1, 0), getLootByRarityToSpawn(LootItems.LootRarity.Rare));
            //SpawnLoot(new Vector3(2, 1, 0), getLootByRarityToSpawn(LootItems.LootRarity.Epic));
            //SpawnLoot(new Vector3(0, 1, 0), getLootByTypeToSpawn(LootItems.LootType.Upgrade));
            //SpawnLoot(new Vector3(0, 1, 0), getLootByNameToSpawn(LootItems.Loot.AncientHelm));
            SpawnLoot(new Vector3(1, 1, 0), getLootByNameToSpawn(LootItems.Loot.CommonArmour));
            SpawnLoot(new Vector3(2, 1, 0), getLootByNameToSpawn(LootItems.Loot.UncommonArmour));
            SpawnLoot(new Vector3(3, 1, 0), getLootByNameToSpawn(LootItems.Loot.RareArmour));
            SpawnLoot(new Vector3(4, 1, 0), getLootByNameToSpawn(LootItems.Loot.EpicArmour));
            SpawnLoot(new Vector3(5, 1, 0), getLootByNameToSpawn(LootItems.Loot.LegendaryArmour));
           
        }
    }

    public LootItems.LootRarity RandomRarity()
    {
        int rand = UnityEngine.Random.Range(0, 101);
        LootItems.LootRarity rarity = LootItems.LootRarity.Common;
        rand += playerStats.AdditionalDropLuck;

        if (rand <= 40)
        {
            return LootItems.LootRarity.Common;
        }
        if (rand > 40 && rand <= 70)
        {
            return LootItems.LootRarity.Uncommon;
        }
        if (rand > 70 && rand <= 85)
        {
            return LootItems.LootRarity.Rare;
        }
        if (rand > 85 && rand <= 95)
        {
            return LootItems.LootRarity.Epic;
        }
        if (rand > 95 && rand <= 100)
        {
            return LootItems.LootRarity.Legendary;
        }

        return rarity;
    }

    public void NewLootItem(LootItems.Loot lootname, string lootdesc, GameObject lootprefab, Sprite lootsprite, LootItems.LootType loottype, LootItems.LootRarity lootrarity)
    {
        LootItems temp = new LootItems(lootname, lootdesc, lootprefab, lootsprite, loottype, lootrarity);
        lootList.Add(temp);
    }

    public List<LootItems> getLootByHighestRarityToSpawn(int loot_count)
    {
        LootCount = loot_count;
        List<LootItems> LootRarityList = new List<LootItems>();
        List<LootItems> BossList = new List<LootItems>();
        List<LootItems> LegendaryList = new List<LootItems>();
        List<LootItems> EpicList = new List<LootItems>();
        List<LootItems> RareList = new List<LootItems>();
        List<LootItems> UncommonList = new List<LootItems>();
        List<LootItems> CommonList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_rarity == LootItems.LootRarity.Boss && !item.isCollected)
            {
                BossList.Add(item);
            }
            if (item.loot_rarity == LootItems.LootRarity.Legendary && !item.isCollected)
            {
                LegendaryList.Add(item);
            }
            if (item.loot_rarity == LootItems.LootRarity.Epic && !item.isCollected)
            {
                EpicList.Add(item);
            }
            if (item.loot_rarity == LootItems.LootRarity.Rare && !item.isCollected)
            {
                RareList.Add(item);
            }
            if (item.loot_rarity == LootItems.LootRarity.Uncommon && !item.isCollected)
            {
                UncommonList.Add(item);
            }
            if (item.loot_rarity == LootItems.LootRarity.Common && !item.isCollected)
            {
                CommonList.Add(item);
            }
        }

        LootRarityList.AddRange(PopulateRarityList(BossList));
        LootRarityList.AddRange(PopulateRarityList(LegendaryList));
        LootRarityList.AddRange(PopulateRarityList(EpicList));
        LootRarityList.AddRange(PopulateRarityList(RareList));
        LootRarityList.AddRange(PopulateRarityList(UncommonList));
        LootRarityList.AddRange(PopulateRarityList(CommonList));
        
        if (LootRarityList != null)
        {
            return LootRarityList;
        }
        else
        {
            return null;
        }
    }

    List<LootItems> PopulateRarityList(List<LootItems> raritylist)
    {
        List<LootItems> templist = new List<LootItems>();
        List<LootItems> newraritylist = raritylist;

        if (newraritylist.Count >= LootCount)
        {
            for (int i = 0; i < LootCount; i++)
            {
                int rand = UnityEngine.Random.Range(0, newraritylist.Count);
                templist.Add(newraritylist[rand]);
                newraritylist.RemoveAt(rand);
            }

        }
        else
        {
            for (int i = 0; i < newraritylist.Count; i++)
            {
                templist.Add(newraritylist[i]);
                LootCount--;
            }
        }
        return templist;
    }

    public LootItems getLootByName (LootItems.Loot lootname)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_name == lootname)
            {
                tempList.Add(item);
            }
        }
        if (tempList.Count != 0)
        {
            return tempList[0];
        }
        else
        {
            Debug.Log("No Items called " + lootname);
            return null;
        }
        
    }

    public LootItems getLootByNameToSpawn(LootItems.Loot lootname)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_name == lootname && !item.isCollected)
            {
                tempList.Add(item);
            }
        }
        if (tempList.Count != 0)
        {
            return tempList[0];
        }
        else
        {
            Debug.Log("No Items called " + lootname);
            return null;
        }

    }

    public LootItems getLootByRarity(LootItems.LootRarity rarity)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_rarity == rarity)
            {
                tempList.Add(item);
            }
        }
        int rand = UnityEngine.Random.Range(0, tempList.Count);

        if (tempList.Count != 0)
        {
            return tempList[rand];
        }
        else
        {
            Debug.Log("No Items with rarity " + rarity);
            return null;
        }
        
    }

    public LootItems getLootByRarityToSpawn(LootItems.LootRarity rarity)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_rarity == rarity && !item.isCollected)
            {
                tempList.Add(item);
            }
        }
        int rand = UnityEngine.Random.Range(0, tempList.Count);

        if (tempList.Count != 0)
        {
            return tempList[rand];
        }
        else
        {
            Debug.Log("No Items with rarity " + rarity);
            return null;
        }

    }

    public LootItems getLootByType(LootItems.LootType type)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_type == type)
            {
                tempList.Add(item);
            }
        }
        int rand = UnityEngine.Random.Range(0, tempList.Count);

        if (tempList.Count != 0)
        {
            return tempList[rand];
        }
        else
        {
            Debug.Log("No Items by type " + type);
            return null;
        }
    }

    public LootItems getLootByTypeToSpawn(LootItems.LootType type)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_type == type && !item.isCollected)
            {
                tempList.Add(item);
            }
        }
        int rand = UnityEngine.Random.Range(0, tempList.Count);

        if (tempList.Count != 0)
        {
            return tempList[rand];
        }
        else
        {
            Debug.Log("No Items by type " + type);
            return null;
        }
    }

    public LootItems getLootByTypeAndRarity(LootItems.LootType type, LootItems.LootRarity rarity)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_type == type && item.loot_rarity == rarity)
            {
                tempList.Add(item);
            }
        }

        int rand = UnityEngine.Random.Range(0, tempList.Count);

        if (tempList.Count != 0)
        {
            return tempList[rand];
        }
        else
        {
            Debug.Log("No Items with type " + type + " and rarity " + rarity);
            return null;
        }
    }

    public LootItems getLootByTypeAndRarityToSpawn(LootItems.LootType type, LootItems.LootRarity rarity)
    {
        List<LootItems> tempList = new List<LootItems>();
        foreach (LootItems item in lootList)
        {
            if (item.loot_type == type && item.loot_rarity == rarity && !item.isCollected)
            {
                tempList.Add(item);
            }
        }

        int rand = UnityEngine.Random.Range(0, tempList.Count);

        if (tempList.Count != 0)
        {
            return tempList[rand];
        }
        else
        {
            Debug.Log("No Items with type " + type + " and rarity " + rarity);
            return null;
        }
    }


    public void SpawnLoot(Vector3 position, LootItems name)
    {
        lootprefab = name.loot_prefab;
        if (name.loot_name == LootItems.Loot.AncientHelm)
        {
            lootprefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            lootprefab.transform.localScale = new Vector3(1, 1, 1);
        }

        
        GameObject loot = Instantiate(lootprefab, position, Quaternion.identity);
        loot.AddComponent<LootPickUp>();
        loot.AddComponent<LootRotator>();
        loot.AddComponent<BoxCollider>().isTrigger = true;
        loot.tag = "Loot";

        switch (name.loot_rarity)
        {
            case LootItems.LootRarity.Common:
                {
                    loot.AddComponent<Light>().color = Color.white;
                    break;
                }
            case LootItems.LootRarity.Uncommon:
                {
                    loot.AddComponent<Light>().color = Color.green;
                    break;
                }
            case LootItems.LootRarity.Rare:
                {
                    loot.AddComponent<Light>().color = Color.blue;
                    break;
                }
            case LootItems.LootRarity.Epic:
                {
                    loot.AddComponent<Light>().color = Color.magenta;
                    break;
                }
            case LootItems.LootRarity.Legendary:
                {
                    loot.AddComponent<Light>().color = Color.yellow;
                    break;
                }
        }

        LootPickUp script = loot.GetComponent<LootPickUp>();
        script.LootName = name.loot_name;
    }

    // Using a Coroutine incase we want an effect that is time limited - Use 'yield return new WaitForSeconds()"
    // Using if ststements instead of Switch due to yield/break issues.
    public IEnumerator LootEffect(LootItems.Loot loot)
    {
        if (loot == LootItems.Loot.QuiverNormal)
        {
            playerInventory.normalArrowCount = playerStats.PI_MaxNormalArrows;
        }
        else if (loot == LootItems.Loot.QuiverFire)
        {
            playerInventory.fireArrowCount = playerStats.PI_MaxFireArrows;
        }
        else if (loot == LootItems.Loot.QuiverIce)
        {
            playerInventory.iceArrowCount = playerStats.PI_MaxIceArrows;
        }
        else if (loot == LootItems.Loot.QuiverExplosive)
        {
            playerInventory.explosiveArrowCount = playerStats.PI_MaxExplosiveArrows;
        }
        else if (loot == LootItems.Loot.QuiverSpeed)
        {
            playerInventory.speedArrowCount = playerStats.PI_MaxSpeedArrows;
        }
        else if (loot == LootItems.Loot.MaxAmmo)
        {
            playerInventory.MaxAmmo();
        }
        else if (loot == LootItems.Loot.PlayerBaseSpeedRelic)
        {
            playerStats.PM_BaseSpeed += 1;
        }
        else if (loot == LootItems.Loot.PlayerDrawSpeedRelic)
        {
            playerStats.PM_DrawSpeed += 1;
        }
        else if (loot == LootItems.Loot.NoMovementPenaltyRelic)
        {
            getLootByName(loot).isActive = true;
        }
        else if (loot == LootItems.Loot.TankRelic)
        {
            heartsUI.UpdateHearts(playerStats.playerHearts);
            playerStats.PM_BaseSpeed /= 2;
            playerStats.PM_DrawSpeed /= 2;
        }
        else if (loot == LootItems.Loot.ScoutRelic)
        {
            heartsUI.UpdateHearts(-(playerStats.playerHearts / 2));
            playerStats.PM_BaseSpeed *= 2;
            playerStats.PM_DrawSpeed *= 2;
        }
        else if (loot == LootItems.Loot.ThornsRelic)
        {
            getLootByName(loot).isActive = true;
        }
        else if (loot == LootItems.Loot.InfinityRelic)
        {
            getLootByName(loot).isActive = true;
        }
        else if (loot == LootItems.Loot.BiggerBagNormal)
        {
            playerStats.PI_MaxNormalArrows += 5;
        }
        else if (loot == LootItems.Loot.BiggerBagFire)
        {
            playerStats.PI_MaxFireArrows += 5;
        }
        else if (loot == LootItems.Loot.BiggerBagIce)
        {
            playerStats.PI_MaxIceArrows += 5;
        }
        else if (loot == LootItems.Loot.BiggerBagExplosive)
        {
            playerStats.PI_MaxExplosiveArrows += 5;
        }
        else if (loot == LootItems.Loot.BiggerBagSpeed)
        {
            playerStats.PI_MaxSpeedArrows += 5;
        }
        else if (loot == LootItems.Loot.BiggerBagAll)
        {
            playerStats.updateAllMaxArrows(10);
        }
        else if (loot == LootItems.Loot.CauterizeRelic)
        {
            playerStats.ArrowEffects_BurnDamage += 5;
        }
        else if (loot == LootItems.Loot.PyromaniacRelic)
        {
            playerStats.ArrowEffects_BurnTime += 5;
        }
        else if (loot == LootItems.Loot.BarbedTipRelic)
        {
            playerStats.ArrowEffects_BleedDamage += 5;
        }
        else if (loot == LootItems.Loot.DeepCutsRelic)
        {
            playerStats.ArrowEffects_BleedTime += 5;
        }
        else if (loot == LootItems.Loot.SubZeroRelic)
        {
            playerStats.ArrowEffects_SlowTime += 5;
        }
        else if (loot == LootItems.Loot.SharperTipsRelic)
        {
            playerStats.ArrowEffects_BleedChance -= 1;
        }
        else if (loot == LootItems.Loot.HealthPotion)
        {
            playerHealth.Heal(4);
        }
        else if (loot == LootItems.Loot.DungeonFood)
        {
            playerHealth.Heal(1);
        }
        else if (loot == LootItems.Loot.AncientHelm)
        {
            //Apply chance to ignore damage
        }
        else if (loot == LootItems.Loot.CommonArmour)
        {
            helmet.SetActive(true);
            armourMat[0] = armourMats[0];
            
            helmet.GetComponent<MeshRenderer>().materials = armourMat;
            visor.GetComponent<MeshRenderer>().materials = armourMat;
            chestPlate.GetComponent<MeshRenderer>().materials = armourMat;
        }
        else if (loot == LootItems.Loot.UncommonArmour)
        {
            visor.SetActive(true);
            armourMat[0] = armourMats[1];
           
            helmet.GetComponent<MeshRenderer>().materials = armourMat;
            visor.GetComponent<MeshRenderer>().materials = armourMat;
            chestPlate.GetComponent<MeshRenderer>().materials = armourMat;
        }
        else if (loot == LootItems.Loot.RareArmour)
        {
            armourMat[0] = armourMats[2];
            helmet.GetComponent<MeshRenderer>().materials = armourMat;
            visor.GetComponent<MeshRenderer>().materials = armourMat;
            chestPlate.GetComponent<MeshRenderer>().materials = armourMat;
        }
        else if (loot == LootItems.Loot.EpicArmour)
        {
            armourMat[0] = armourMats[3];
            helmet.GetComponent<MeshRenderer>().materials = armourMat;
            visor.GetComponent<MeshRenderer>().materials = armourMat;
            chestPlate.GetComponent<MeshRenderer>().materials = armourMat;
        }
        else if (loot == LootItems.Loot.LegendaryArmour)
        {
            armourMat[0] = armourMats[4];
            helmet.GetComponent<MeshRenderer>().materials = armourMat;
            visor.GetComponent<MeshRenderer>().materials = armourMat;
            chestPlate.GetComponent<MeshRenderer>().materials = armourMat;
        }
        else if (loot == LootItems.Loot.WarbannerRelic)
        {
            getLootByName(loot).isActive = true;
            
        }
        else if (loot == LootItems.Loot.RestorePotion)
        {
            StartCoroutine(RestorePotion());
        }
        else if (loot == LootItems.Loot.ArmourPiercingArrows)
        {
            getLootByName(loot).isActive = true;
        }
        else if (loot == LootItems.Loot.PreservedInsectRelic)
        {
            getLootByName(loot).isActive = true;
        }
        else if (loot == LootItems.Loot.ExplorerRelic)
        {
            getLootByName(loot).isActive = true;
        }
        else if (loot == LootItems.Loot.AssassinRelic)
        {
            playerStats.ArrowEffects_BurnDamage = 0;
            playerStats.ArrowEffects_BurnTime = 0;
            playerStats.ArrowEffects_SlowTime = 0;

            playerStats.ArrowEffects_BleedChance = 1;
        }
        else if (loot == LootItems.Loot.BetterBowRelic)
        {
            playerStats.ArrowDamage_CritChance += 10;
        }
        else if (loot == LootItems.Loot.LizardTailRelic)
        {
            getLootByName(loot).isActive = true;
        }
        else if (loot == LootItems.Loot.LuckyDiceRelic)
        {
            playerStats.AdditionalDropLuck += 20;
        }
        else if (loot == LootItems.Loot.AncientLamp)
        {
            player.transform.Find("AncientLampLight").gameObject.SetActive(true);
        }
        else if (loot == LootItems.Loot.MysticFeather)
        {
            playerStats.ArrowDamage_Base += 5;
        }
        else if (loot == LootItems.Loot.CartographerRelic)
        {
            playerStats.TreasureRoomChance += 20;
        }
        else if (loot == LootItems.Loot.HestiasAmulet)
        {
            playerStats.Torch_MaxTimer += 30;
        }
        else if (loot == LootItems.Loot.AncientWood)
        {
            playerStats.Torch_MaxTimer += 3;
        }
        else if (loot == LootItems.Loot.PrometheusChains)
        {
            playerStats.Torch_MaxIntensity += 10;
        }
        else if (loot == LootItems.Loot.LampOil)
        {
            playerStats.Torch_MaxIntensity += 3;
        }
        yield break;
    }

    public void UpdateRelics()
    {
        if (getLootByName(LootItems.Loot.NoMovementPenaltyRelic).isActive)
        {
            playerStats.PM_DrawSpeed = playerStats.PM_BaseSpeed;
        }
        if (getLootByName(LootItems.Loot.ThornsRelic).isActive)
        {
            //Do thorns damage once enemies are setup.
        }
        if (getLootByName(LootItems.Loot.WarbannerRelic).isActive)
        {
            if (!playerController.isMovingForSound)
            {
                if (warbanner_timer < warbanner_goal)
                {
                    warbannerDamage = false;
                    foreach (ParticleSystem ps in warbannerPSList)
                    {
                        ps.Stop();
                        isPSPlaying = false;
                    }
                    warbanner_timer += Time.deltaTime;
                    warbannerps.transform.parent = player.transform;
                }
                else if (warbanner_timer >= warbanner_goal)
                {
                    warbannerDamage = true;
                    foreach (ParticleSystem ps in warbannerPSList)
                    {
                        if (!isPSPlaying)
                        {
                            ps.Clear();
                            if (ps.name.Contains("Radius"))
                            {
                                ParticleSystem.MainModule psmain = ps.main;
                                psmain.startLifetime = 20;
                            }
                            ps.Play();
                            ParticleSystem.ColorOverLifetimeModule pscol = ps.colorOverLifetime;
                            pscol.enabled = false;
                        }
                    }
                    isPSPlaying = true;
                    warbannerps.transform.parent = null;
                }
            }
            else
            {
                warbannerDamage = false;
                foreach (ParticleSystem ps in warbannerPSList)
                {
                    if (ps.name.Contains("Radius"))
                    {
                        ps.Clear();
                    }
                    ps.Stop();
                    isPSPlaying = false;
                    ParticleSystem.ColorOverLifetimeModule pscol = ps.colorOverLifetime;
                    pscol.enabled = true;
                }
                warbanner_timer = 0;
                warbannerps.transform.parent = player.transform;
            }
        }
        if (getLootByName(LootItems.Loot.ExplorerRelic).isActive)
        {
            if (explorerRelicRooms == 10)
            {
                playerHealth.Heal(1);
                explorerRelicRooms = 0;
            }
        }
        if (getLootByName(LootItems.Loot.LizardTailRelic).isActive)
        {
            //do thing
        }
    }

    IEnumerator RestorePotion()
    {
        yield return new WaitForSeconds(5);
        playerHealth.Heal(2);
        yield return new WaitForSeconds(5);
        playerHealth.Heal(2);
        yield break;
    }
}
