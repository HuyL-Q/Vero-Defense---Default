using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerType
{
    public string towerType;
}

public class StringConverter : JsonConverter<List<TowerType>> { }
public class StoryUIController : MonoBehaviour
{
    GameObject currentTower;
    private float currentTimeScale;
    private int placementIndex;
    [SerializeField]
    GameObject speedUpGO;
    [SerializeField]
    GameObject livesGO;
    [SerializeField]
    GameObject goldGO;
    [SerializeField]
    GameObject waveGO;
    [SerializeField]
    GameObject buyTowerPanel;
    [SerializeField]
    GameObject upgradeAndSellPanel;
    [SerializeField]
    GameObject pausePanel;
    public static StoryUIController instance;
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
        UpdateLivesIndex();
        UpdateGoldIndex();
        UpdateWaveIndex();
    }
    public void SpeedUp()
    {
        switch (Time.timeScale)
        {
            case 1:
                Time.timeScale = 2;
                speedUpGO.transform.GetChild(0).gameObject.SetActive(false);
                speedUpGO.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                Time.timeScale = 5;
                speedUpGO.transform.GetChild(1).gameObject.SetActive(false);
                speedUpGO.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 5:
                Time.timeScale = 1;
                speedUpGO.transform.GetChild(2).gameObject.SetActive(false);
                speedUpGO.transform.GetChild(0).gameObject.SetActive(true);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateWaveIndex()
    {
        waveGO.GetComponentInChildren<Text>().text = NewSpawnController.Instance.WaveIndex.ToString();
    }

    public void UpdateLivesIndex()
    {
        livesGO.GetComponentInChildren<Text>().text = GameController.instance.PlayerLives.ToString();
    }

    public void UpdateGoldIndex()
    {
        goldGO.GetComponentInChildren<Text>().text = GameController.instance.PlayerMoney.ToString();
    }

    public void OpenBuyTowerPanel(Transform towerPlacement, int towerPlacementIndex)
    {
        CloseUpgrade_SellPanel();
        CloseBuyTower();
        StartCoroutine(CheckTowerPrice());
        SoundManagerDetail.PlaySound("OpenTowerMenu");
        buyTowerPanel.SetActive(true);
        buyTowerPanel.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        placementIndex = towerPlacementIndex;
    }

    IEnumerator CheckTowerPrice()
    {
        while (true)
        {
            yield return new WaitUntil(() => BuyTower.flag);
            var ls = BuyTower.ButtonList;
            foreach (GameObject archerBuyTowerButton in ls)
            {
                archerBuyTowerButton.GetComponent<Button>().interactable = true;
                if (TowerManager.instance.ArcherPrice > GameController.instance.PlayerMoney||
                    (archerBuyTowerButton.name.Contains("hero") && GameController.instance.HeroList[int.Parse(archerBuyTowerButton.name.Split("_")[2])]))
                {
                    archerBuyTowerButton.GetComponent<Button>().interactable = false;
                }
            }
            yield return null;
        }
    }

    public void CloseBuyTower()
    {
        buyTowerPanel.transform.DOKill();
        buyTowerPanel.SetActive(false);
    }

    public void OpenUpgrade_SellPanel(Transform targetPos, GameObject tower)
    {
        CloseUpgrade_SellPanel();
        CloseBuyTower();
        SetUpgradeAndSellPrice(tower);
        upgradeAndSellPanel.SetActive(true);
        upgradeAndSellPanel.transform.position = new(targetPos.position.x, targetPos.position.y, upgradeAndSellPanel.transform.position.z);
        upgradeAndSellPanel.transform.localScale = new(0, 0, 0);
        upgradeAndSellPanel.transform.DOScale(1, .5f).SetEase(Ease.OutBack);
        currentTower = tower;
        placementIndex = tower.GetComponent<ATower>().PlacementIndex;
        OpenAttackRange(currentTower);
    }
    List<int> priceToUpgrades = new();
    float priceToSell;
    public void SetUpgradeAndSellPrice(GameObject tower)
    {
        priceToUpgrades.Clear();
        int count = tower.GetComponent<ATower>().PriceToUpgrade.Count;
        if (count > 1)
        {
            upgradeAndSellPanel.transform.GetChild(1).gameObject.SetActive(false);
            priceToUpgrades.Add(tower.GetComponent<ATower>().PriceToUpgrade[0]);
            priceToUpgrades.Add(tower.GetComponent<ATower>().PriceToUpgrade[1]);
            upgradeAndSellPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = priceToUpgrades[0].ToString();
            upgradeAndSellPanel.transform.GetChild(2).gameObject.SetActive(true);
            upgradeAndSellPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = priceToUpgrades[1].ToString();
            upgradeAndSellPanel.transform.GetChild(3).gameObject.SetActive(true);
            StartCoroutine(CheckUpgradeTwoBranchPrice());
        }
        else if (count > 0)
        {
            priceToUpgrades.Add(tower.GetComponent<ATower>().PriceToUpgrade[0]);
            upgradeAndSellPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = priceToUpgrades[0].ToString();
            upgradeAndSellPanel.transform.GetChild(1).gameObject.SetActive(true);
            upgradeAndSellPanel.transform.GetChild(2).gameObject.SetActive(false);
            upgradeAndSellPanel.transform.GetChild(3).gameObject.SetActive(false);
            StartCoroutine(CheckUpgradeOneBranchPrice());
        }
        else
        {
            upgradeAndSellPanel.transform.GetChild(1).gameObject.SetActive(false);
            upgradeAndSellPanel.transform.GetChild(2).gameObject.SetActive(false);
            upgradeAndSellPanel.transform.GetChild(3).gameObject.SetActive(false);
        }
        priceToSell = tower.GetComponent<ATower>().Price / 2;
        upgradeAndSellPanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = priceToSell.ToString();
    }
    IEnumerator CheckUpgradeTwoBranchPrice()
    {
        while (true)
        {
            upgradeAndSellPanel.transform.GetChild(2).GetComponent<Button>().interactable = true;
            upgradeAndSellPanel.transform.GetChild(3).GetComponent<Button>().interactable = true;
            if (GameController.instance.PlayerMoney < priceToUpgrades[0])
            {
                upgradeAndSellPanel.transform.GetChild(2).GetComponent<Button>().interactable = false;
            }
            if (GameController.instance.PlayerMoney < priceToUpgrades[1])
            {
                upgradeAndSellPanel.transform.GetChild(3).GetComponent<Button>().interactable = false;
            }
            yield return null;
        }
    }
    IEnumerator CheckUpgradeOneBranchPrice()
    {
        while (true)
        {
            upgradeAndSellPanel.transform.GetChild(1).GetComponent<Button>().interactable = true;
            if (GameController.instance.PlayerMoney < priceToUpgrades[0])
            {
                upgradeAndSellPanel.transform.GetChild(1).GetComponent<Button>().interactable = false;
            }
            yield return null;
        }
    }
    public void SellButton()
    {
        SoundManagerDetail.PlaySound("SellTower");
        CloseUpgrade_SellPanel();
        TowerManager.instance.TowerPlacementParent.transform.GetChild(placementIndex).gameObject.GetComponent<CircleCollider2D>().enabled = true;
        if(currentTower.GetComponent<ATower>().ID.Contains("hero"))
            GameController.instance.HeroList[int.Parse(currentTower.GetComponent<ATower>().ID.Split('_')[2])] = false;
        Destroy(currentTower);
        GameController.instance.PlayerMoney += priceToSell;
        UpdateGoldIndex();
    }
    public void UpgradeButton()
    {
        SoundManagerDetail.PlaySound("TowerUpgrade");
        CloseUpgrade_SellPanel();
        GameController.instance.PlayerMoney -= priceToUpgrades[0];
        UpdateGoldIndex();
        TowerManager.instance.UpgradeTower(currentTower, 0);
    }
    public void UpgradeButton(int prefix)
    {
        SoundManagerDetail.PlaySound("TowerUpgrade");
        CloseUpgrade_SellPanel();
        GameController.instance.PlayerMoney -= priceToUpgrades[prefix - 1];
        UpdateGoldIndex();
        TowerManager.instance.UpgradeTower(currentTower, prefix);
    }
    public void CloseUpgrade_SellPanel()
    {
        CloseAttackRange();
        StopAllCoroutines();
        upgradeAndSellPanel.transform.DOKill();
        upgradeAndSellPanel.SetActive(false);
    }

    public void OpenAttackRange(GameObject tower)
    {
        tower.GetComponent<ATower>().RangeIndicator.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void CloseAttackRange()
    {
        if (currentTower != null)
        {
            currentTower.GetComponent<ATower>().RangeIndicator.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void BuildTower(string index)
    {
        StopCoroutine(CheckTowerPrice());
        CloseBuyTower();
        TowerManager.instance.SetTower(placementIndex, index);      
    }

    public void PauseButton()
    {
        currentTimeScale = Time.timeScale;
        Time.timeScale = 0;
        pausePanel.transform.GetChild(0).localScale = new(0, 0, 0);
        pausePanel.SetActive(true);
        pausePanel.transform.GetChild(0).DOScale(1, .5f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void ResumeButton()
    {
        pausePanel.transform.GetChild(0).DOScale(0, .5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            pausePanel.SetActive(false);
            Time.timeScale = currentTimeScale;
        });
    }

    public void ReplayButton()
    {
        pausePanel.transform.GetChild(0).DOScale(0, .5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    public void MainMenuButton()
    {
        pausePanel.transform.GetChild(0).DOScale(0, .5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            MenuManager.MenuSwitch(MenuName.Main);
        });
    }
    public void GameOverReplay()
    {
        GameObject.Find("UI").transform.GetChild(2).transform.GetChild(0).DOScale(0, .5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            GameObject.Find("UI").transform.GetChild(2).gameObject.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        Time.timeScale = 1;
    }
    public void GameOverMainMenu()
    {
        GameObject.Find("UI").transform.GetChild(2).gameObject.SetActive(false);
        MenuManager.MenuSwitch(MenuName.Main);
    }
}