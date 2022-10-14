using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Prestart, Start, End_Defeat, End_Victory, Pause };
public class PlayerStat
{
    private int health;
    private int money;

    public PlayerStat(int health, int money)
    {
        this.health = health;
        this.money = money;
    }

    public int Lives { get => health; set => health = value; }
    public int Money { get => money; set => money = value; }


}
public class PlayerStatsConverter : JsonConverter<PlayerStat> { }
public class GameObjectConverter : JsonConverter<SaveGame> { }
public class GameController : MonoBehaviour
{
    [SerializeField]
    int playerLives;
    [SerializeField]
    float playerMoney;
    float playerPoint;
    State state;
    public bool flag = false;
    public Dictionary<int, bool> HeroList;

    public int PlayerLives { get => playerLives; set => playerLives = value; }
    public float PlayerMoney { get => playerMoney; set => playerMoney = value; }
    public float PlayerPoint { get => playerPoint; set => playerPoint = value; }
    public State State { get => state; set => state = value; }

    public static GameController instance;
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
        HeroList = new Dictionary<int, bool>();
        State = State.Prestart;
        StartCoroutine(wait());
        SoundManagerDetail.PlaySound("GamePlaySound");


    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.6f);
        flag = true;
    }

    void ReadStatFromFile()
    {
        PlayerStatsConverter statsConverter = new();
        statsConverter.setCurrentDir(@"/PlayerStats.json");
        PlayerStat stats = statsConverter.getObjectFromJSON();
        PlayerLives = stats.Lives;
        PlayerMoney = stats.Money;
        PlayerPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerLives <= 0)
        {
            State = State.End_Defeat;
            Time.timeScale = 0;
        }
    }
    public void SaveData()
    {
        GameObjectConverter converter = new GameObjectConverter();
        PlayerPrefs.SetString("saveData", converter.createJSONText(SaveGame.Instance));
    }
    public static GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
    public void LoadData()
    {
        GameObjectConverter converter = new GameObjectConverter();
        SaveGame.Instance = new SaveGame();
        SaveGame.Instance = converter.getObjectfromText(PlayerPrefs.GetString("saveData"));

        foreach (TowerData data in SaveGame.Instance.ls)
        {
            TowerManager.instance.SetTower(data.towerPlacementIndex, data.id);
        }
        GameController.instance.PlayerLives = SaveGame.Instance.lives;
        GameController.instance.PlayerMoney = SaveGame.Instance.money;
        GameController.instance.PlayerPoint = SaveGame.Instance.playerPoint;
        GameController.instance.State = SaveGame.Instance.state;
        NewSpawnController.Instance.WaveIndex = SaveGame.Instance.stageIndex;
        NewSpawnController.Instance.NumOfEnemies = NewSpawnController.Instance.NumOfEnemies;
        StoryUIController.instance.UpdateGoldIndex();
        StoryUIController.instance.UpdateLivesIndex();
        StoryUIController.instance.UpdateWaveIndex();
        flag = true;
        //reset stage using stageindex
    }
    public void OpenSetting()
    {
        FindInActiveObjectByName("Setting Canvas").SetActive(true);
    }
}