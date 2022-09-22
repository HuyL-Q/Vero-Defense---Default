using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Tower
{
    public string id;
    public int attack;
    public float attackSpeed;
    public float range;
    public string[] Special;
    public int Cost;
    public int width;
    public int height;
    public string bulletID;
}

public class TowerPriceConverter : JsonConverter<List<Tower>> { }
public class TowerManager : MonoBehaviour
{
    [SerializeField]
    GameObject towerPlacementParent;
    [SerializeField]
    GameObject btnBuyArcher;
    [SerializeField]
    GameObject btnBuyHero1;
    [SerializeField]
    GameObject btnBuyHero2;
    [SerializeField]
    Transform towerParent;
    int archerPrice;
    ArcherTowerFactory archerTowerFactory;
    HeroTowerFactory heroTowerFactory;
    public static TowerManager instance;
    public int ArcherPrice { get => archerPrice; set => archerPrice = value; }
    public int HeroPrice { get => archerPrice; set => archerPrice = value; }
    public GameObject TowerPlacementParent { get => towerPlacementParent; set => towerPlacementParent = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        archerTowerFactory = GetComponent<ArcherTowerFactory>();
        heroTowerFactory = GetComponent<HeroTowerFactory>();
        StartCoroutine(FetchTowerPrice());
    }

    IEnumerator FetchTowerPrice()
    {
        TowerPriceConverter converter = new();
        converter.setCurrentDir(@"/TowerStat.json");
        List<Tower> towers = converter.getObjectFromJSON();
        foreach (Tower tower in towers)
        {
            if (tower.id.Contains("archer_1"))
            {
                ArcherPrice = tower.Cost;
                btnBuyArcher.transform.GetChild(0).GetComponent<Text>().text = ArcherPrice.ToString();
            }
            if (tower.id.Contains("hero_1"))
            {
                HeroPrice = tower.Cost;
                btnBuyHero1.transform.GetChild(0).GetComponent<Text>().text = HeroPrice.ToString();
            }

            if (tower.id.Contains("hero_2"))
            {
                HeroPrice = tower.Cost;
                btnBuyHero2.transform.GetChild(0).GetComponent<Text>().text = HeroPrice.ToString();
            }
        }
        yield return null;
    }

    //public void SetTower(int placementIndex, int index)
    //{
    //    Transform towerPlace = TowerPlacementParent.transform.GetChild(placementIndex);
    //    Vector3 pos = towerPlace.position;
    //    pos.y += .25f;
    //    switch(index){
    //        case 0:
    //            archerTowerFactory.GetComponent<ArcherTowerFactory>().CreateTower(towerParent, pos, placementIndex);
    //            break;
    //        case 1:
    //            heroTowerFactory.GetComponent<HeroTowerFactory>().CreateTower(towerParent, pos, placementIndex, index);
    //            break;
    //        case 2:
    //            heroTowerFactory.GetComponent<HeroTowerFactory>().CreateTower(towerParent, pos, placementIndex, index+1);
    //            break;
    //    }
        
    //    towerPlace.gameObject.SetActive(false);
    //    //GameController.instance.PlayerMoney -= ArcherPrice;
    //    //StoryUIController.instance.UpdateGoldIndex();
    //}
    public void SetTower(int placementIndex, string id)
    {
        Transform towerPlace = TowerPlacementParent.transform.GetChild(placementIndex);
        Vector3 pos = towerPlace.position;
        string[] ar = id.Split("_");
        switch (ar[1]) 
        {
            case "archer":
                archerTowerFactory.GetComponent<ArcherTowerFactory>().CreateTower(towerParent, pos, placementIndex, id);
                break;
            case "hero":
                archerTowerFactory.GetComponent<HeroTowerFactory>().CreateTower(towerParent, pos, placementIndex, id);
                break;
        }
        towerPlace.gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    public void UpgradeTower(GameObject tower, int prefix)
    {
        Transform towerLevelSprite = tower.transform.GetChild(0);
        for (int i = 0; i < towerLevelSprite.childCount; i++)
        {
            if (towerLevelSprite.GetChild(i).gameObject.activeSelf)
            {
                towerLevelSprite.GetChild(i).gameObject.SetActive(false);
                towerLevelSprite.GetChild(i + 1).gameObject.SetActive(true);
                string[] idSplit = tower.GetComponent<ATower>().ID.Split("_");
                string nextID = idSplit[0] + "_" + idSplit[1] + "_" + (int.Parse(idSplit[2]) + 1);
                if (prefix > 0)
                {
                    towerLevelSprite.GetChild(i + 1).GetChild(prefix - 1).gameObject.SetActive(true);
                    nextID += "_" + prefix;
                }

                StartCoroutine(tower.GetComponent<ATower>().SetTower(nextID));
                break;
            }
        }
    }
}